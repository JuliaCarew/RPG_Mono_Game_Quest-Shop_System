using Nez;
using System;
using System.Collections.Generic;

namespace MonoGame.QuestSsystem
{
    internal class QuestSystem : Scene
    {
        public override void Initialize()
        {
            base.Initialize();

            var questEntity = CreateEntity("questEntity");

            questEntity.AddComponent(new QuestManager());
        }
    }
}
