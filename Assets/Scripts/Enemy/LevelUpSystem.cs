using Unity.Entities;
using UnityEngine;

namespace SLGame.Enemy
{
    public class LevelUpSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref StatsComponent stats) =>
            {
                stats.level += 1 * (uint)Time.DeltaTime;
            });
        }
    }
}