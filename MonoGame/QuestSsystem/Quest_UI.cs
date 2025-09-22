using Nez;
using Nez.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Nez.Textures;
using System.Linq;

namespace MonoGame.QuestSsystem
{
    // updates text on-screen depending on questmanager
    internal class Quest_UI : UICanvas
    {
        // show list of all quests, change position
        // follow event in manager script to update UI
        // display current quest window (different text per quest, include quest name & objective)
        // do same for completed quests
        //private Actor entity;

        private Table questuiTable;
        private List<Quest> currentQuests = new List<Quest>();

        public Quest_UI()
        {
            RenderLayer = 999;

            Skin skin = Skin.CreateDefaultSkin();

            questuiTable = new Table();
            questuiTable.SetFillParent(true);
            questuiTable.Right().Top(); // position

            Stage.AddElement(questuiTable);
        }

        // write for each quest UI object
        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
            RefreshTable();
        }

        // questmanager needs to use this to uopdate quest data displayed
        public void UpdateQuests(List<Quest> quests)
        {
            currentQuests = quests;
            RefreshTable();
        }

        private void RefreshTable()
        {
            questuiTable.Clear();

            Skin skin = Skin.CreateDefaultSkin();

            questuiTable.Add(new Label("Quests", skin)).Left();
            questuiTable.Row();

            foreach (var quest in currentQuests)
            {
                string questTitle = quest.Name;
                string description = quest.Description;
                string status = quest.IsCompleted ? "Completed" : "In Progress";

                string objective = quest.objective != null
                ? $"{quest.objective.Name} ({quest.objective.progress}/{quest.objective.targetAmount})" : "";

                Color statusColor = quest.IsCompleted ? Color.Green : Color.White;

                // status/title label
                var statusLabel = new Label($"[{status}] {questTitle}", skin);
                statusLabel.SetColor(statusColor);
                questuiTable.Add(statusLabel).Left();
                questuiTable.Row();

                // objective label
                var objectiveLabel = new Label($"- {objective}", skin);
                objectiveLabel.SetColor(statusColor);
                questuiTable.Add(objectiveLabel).Left();
                questuiTable.Row();

                // description label
                var descriptionLabel = new Label($"{description}", skin);
                descriptionLabel.SetColor(statusColor);
                questuiTable.Add(descriptionLabel).Left();
                questuiTable.Row();
            }
        }
    }
}
