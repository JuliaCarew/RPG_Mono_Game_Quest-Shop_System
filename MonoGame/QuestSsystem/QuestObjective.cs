using Nez;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.QuestSsystem
{
    // make enum of types of objectives (general genres; collect, kill, explore)
    public enum objectiveType
    {
        kill,
        travel,
        collect,
        complete,
        none
    }
    internal class QuestObjective 
    {
        public string Name; 
        public objectiveType type; 
        public int targetAmount; // how many needed 
        public int progress; // how many done so far
        public bool IsCompleted => progress >= targetAmount;

        public QuestObjective(objectiveType Type, string name, int TargetAmount = 1)
        {
            type = Type;
            Name = name;
            targetAmount = TargetAmount;
            progress = 0;
        }

        public void Increment()
        {
            progress++;
        }
    }
}
