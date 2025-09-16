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
        private Actor entity;

        private Table questuiTable;
        private List<Quest> currentQuests = new List<Quest>();

        public Quest_UI()
        {
            RenderLayer = 999;

            Skin skin = Skin.CreateDefaultSkin();

            questuiTable = new Table();
            questuiTable.SetFillParent(true);

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
                string objective = quest.Objective;
                string description = quest.Description;
                string status = quest.IsCompleted ? "Completed" : "In Progress";

                questuiTable.Add(new Label($"[{status}] {questTitle}", skin)).Left();
                questuiTable.Row();
                questuiTable.Add(new Label($"- {objective}", skin)).Left();
                questuiTable.Row();
                questuiTable.Add(new Label($"{description}", skin)).Left();
                questuiTable.Row();
            }
        }
    }
}
