using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.QuestSsystem
{
    internal class Quest
    {
        string name;
        public List<QuestObjective> objectives = new List<QuestObjective>();

        public QuestObjective newObjective;
        public QuestObjective completedObjective;

        // list objectives
        /*
        private QuestObjective 
        {
            name
            description
            isComplete
        }
        */

        void Update()
        {
            // update objectives
        }

        void AddObjective()
        {
            objectives.Add(newObjective);

        }

        bool IsComplete()
        {
            return false;
        }

        void RemoveObjective()
        {
            objectives.Remove(completedObjective);
        }

        void UpdateObjectives()
        {
            // re-load lists
            // if not completed, check objective requirement
        }
    }
}
