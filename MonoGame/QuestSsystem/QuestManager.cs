using Nez;
using System;
using System.Collections.Generic;


namespace MonoGame.QuestSsystem
{
    // lists of all quests, active quests, completed quests
    // takes event from when quest objective is complete to move from active to complete list
    internal class QuestManager : Component
    {
        public List<Quest> currentQuests = new List<Quest>();
        public List<Quest> completedQuests = new List<Quest>();

        public override void OnAddedToEntity()
        {
            Debug.Log("QuestManager: OnOnAddedToEntity");

            base.OnAddedToEntity();

            // test quest
            currentQuests.Add(new Quest
            {
                Name = "Slayer",
                Description = "Improve in combat",
                CurrentObjective = new QuestObjective { Name = "Kill 1 enemy" },
                IsCompleted = false
            });

            // UI
            var questUI = Entity.AddComponent(new Quest_UI());
            questUI.UpdateQuests(currentQuests);
        }
    }
}
