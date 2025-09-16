using Nez;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.QuestSsystem
{
    // make enum of types of objectives (general genres; collect, kill, explore)
    internal class QuestObjective //: Component, IUpdatable
    {
        // one requirement, can be enum
        public enum objectiveType
        {
            kill,
            travel,
            collect
        }

        // different requirements to complete each
    }
}
