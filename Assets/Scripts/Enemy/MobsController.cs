using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Rendering;
using SLGame.Modules;
using Unity.Mathematics;

namespace SLGame.Enemy
{

    public class MobsController : MonoBehaviour
    {
        private async void Awake()
        {
            Debug.LogWarning(UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset.GetType().FullName);
            AssetLoader loader = new AssetLoader("spiderPrefabAddressable");
            await loader.Load();
        }
    }

    public partial class EntitySpawnSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem bufferSystem;

        protected override void OnCreate()
        {
            bufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected async override void OnStartRunning()
        {
            await System.Threading.Tasks.Task.Delay(2000);

            var commandBuffer = bufferSystem.CreateCommandBuffer().AsParallelWriter();

            Entities.WithAll<GameObjectToEntity>().ForEach(
                (Entity entity, int entityInQueryIndex, ref GameObjectToEntity gameObject) =>
                {
                    for (int i = 0; i < 4; i++)
                    {
                        var tempEntity = commandBuffer.Instantiate(entityInQueryIndex, gameObject._gameObjectToEntity);
                    }
                }
            ).ScheduleParallel();

            Entities.WithAll<GameObjectToEntity>().ForEach(
                (ref GameObjectToEntity gameObject) =>
                {
                    EntityManager.SetComponentData<Translation>(gameObject._gameObjectToEntity, new Translation()
                    {
                        Value = new float3(3.5f, 0f, 11f)
                    });
                }
            ).WithoutBurst().Run();

            bufferSystem.AddJobHandleForProducer(Dependency);

            Debug.LogWarning("OnStartRunning");
        }

        protected override void OnUpdate()
        {
            //throw new System.NotImplementedException();
        }
    }
}