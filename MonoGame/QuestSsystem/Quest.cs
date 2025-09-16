using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.QuestSsystem
{
    internal class Quest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public QuestObjective CurrentObjective { get; set; }
        public string Objective => CurrentObjective?.Name ?? "";

        void Update()
        {
            // update objectives
        }

        bool IsComplete()
        {
            return false;
        }

        void UpdateObjectives()
        {
            // re-load lists
            // if not completed, check objective requirement
        }
    }
}
