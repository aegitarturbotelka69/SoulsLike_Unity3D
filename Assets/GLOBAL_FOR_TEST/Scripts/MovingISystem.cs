using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace GLOBAL_FOR_TESTING
{
    [BurstCompile]
    public partial struct MovingISystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            RefRW<RandomComponent> rnd = SystemAPI.GetSingletonRW<RandomComponent>();
            float deltaTime = SystemAPI.Time.DeltaTime;


            JobHandle handle =  new MoveJob
            {
                deltaTime = deltaTime
            }.ScheduleParallel(state.Dependency);

            handle.Complete();
            new ReachedPositionJob
            {
                randomComponent = rnd
            }.Run();
        }
    }

    [BurstCompile]
    public partial struct MoveJob: IJobEntity
    {
        public float deltaTime;

        public void Execute(MoveToPositionAspect moveAspect)
        {
            moveAspect.Move(deltaTime);
        }
    }

    [BurstCompile]
    public partial struct ReachedPositionJob: IJobEntity
    {
        [NativeDisableUnsafePtrRestriction] public RefRW<RandomComponent> randomComponent;

        public void Execute(MoveToPositionAspect moveAspect)
        {
            moveAspect.CheckReachedTargetPosition(randomComponent);
        }
    }
}