using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    internal class GameEvents
    {
        public static event Action<Enemy> EnemyKilled;
        public static event Action<Item> ItemCollected;
        public static event Action GameCompleted;
        public static event Action ShopReached;

        public static void OnEnemyKilled(Enemy enemy)
        {
            EnemyKilled?.Invoke(enemy);
        }

        public static void OnItemCollected(Item i) => ItemCollected?.Invoke(i);
        public static void OnGameCompleted() => GameCompleted?.Invoke();
        public static void OnShopReached() => ShopReached?.Invoke();

    }
}
