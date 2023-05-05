using Unity.Entities;
using UnityEngine;

namespace GLOBAL_FOR_TESTING
{
    public partial class PlayerSpawnerSystem: SystemBase
    {
        protected override void OnUpdate()
        {
            var player = EntityManager.CreateEntityQuery(typeof(PlayerTag));

            var spawnerComponent = SystemAPI.GetSingleton<PlayerSpawnerComponent>();
            var rndGenerator = SystemAPI.GetSingletonRW<RandomComponent>();

            var commandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

            if(player.CalculateEntityCount() < 5)
            {
                var entity=  commandBuffer.Instantiate(spawnerComponent.playerPrefab);
                commandBuffer.SetComponent(entity, new EntityComponent
                {
                    speed = rndGenerator.ValueRW.rndGenerator.NextFloat(1f, 8f)
                });
            }
        }
    }
}  
