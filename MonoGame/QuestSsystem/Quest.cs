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
        public bool IsCompleted => objective.IsCompleted;
        public QuestObjective objective { get; set; }

        bool IsComplete()
        {
            return false;
        }
    }
}
