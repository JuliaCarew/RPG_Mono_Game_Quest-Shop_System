using Nez;
using System;
using System.Collections.Generic;
using System.Linq;


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
                Name = "First Blood",
                Description = "Kill 3 enemies",
                objective = new QuestObjective(objectiveType.kill, "Kill 3 enemies", 3)
            });

            currentQuests.Add(new Quest
            {
                Name = "Collector",
                Description = "Pick up 5 items",
                objective = new QuestObjective(objectiveType.collect, "Collect 5 items", 5)
            });

            currentQuests.Add(new Quest
            {
                Name = "Champion",
                Description = "Complete the game",
                objective = new QuestObjective(objectiveType.complete, "Complete the game")
            });

            GameEvents.EnemyKilled += OnEnemyKilled;
            GameEvents.ItemCollected += OnItemCollected;
            GameEvents.GameCompleted += OnGameCompleted;

            // UI
            var questUI = Entity.AddComponent(new Quest_UI());
            questUI.UpdateQuests(currentQuests);
        }

        void OnEnemyKilled(Enemy e)
        {
            UpdateObjectives(objectiveType.kill);
        }

        void OnItemCollected(Item i)
        {
            UpdateObjectives(objectiveType.collect);
        }

        void OnGameCompleted()
        {
            UpdateObjectives(objectiveType.complete);
        }

        void UpdateObjectives(objectiveType type)
        {
            foreach (var quest in currentQuests.ToList())
            {
                if (quest.objective.type == type && !quest.objective.IsCompleted)
                {
                    quest.objective.Increment();

                    if (quest.objective.IsCompleted)
                    {
                        completedQuests.Add(quest);
                    }
                }
            }

            var questUI = Entity.GetComponent<Quest_UI>();
            if (questUI != null)
                questUI.UpdateQuests(currentQuests);
        }

        public override void OnRemovedFromEntity()
        {
            GameEvents.EnemyKilled -= OnEnemyKilled;
            GameEvents.ItemCollected -= OnItemCollected;
            GameEvents.GameCompleted -= OnGameCompleted;
            base.OnRemovedFromEntity();
        }
    }

  
}
