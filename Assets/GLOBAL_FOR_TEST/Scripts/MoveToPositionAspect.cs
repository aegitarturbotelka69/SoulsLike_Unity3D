using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace GLOBAL_FOR_TESTING
{
    public readonly partial struct MoveToPositionAspect: IAspect
    {
        private readonly TransformAspect transform;
        private readonly RefRO<EntityComponent> speed;
        private readonly RefRW<TargetPosition> position;

        public void Move(float deltaTime)
        {
            float3 direction = math.normalize(position.ValueRW.value - transform.Position);

            transform.Position += direction * deltaTime * speed.ValueRO.speed;
        }

        public void CheckReachedTargetPosition(RefRW<RandomComponent> random)
        {
            float reachedTargetDistance = 0.5f;

            if(math.distance(transform.Position, position.ValueRW.value) < reachedTargetDistance)
            {
                position.ValueRW.value = GetRandomPosition(random);
            }
        }

        private float3 GetRandomPosition(RefRW<RandomComponent> rnd)
        {
            return new float3(rnd.ValueRW.rndGenerator.NextFloat(0f, 15f), 0f, rnd.ValueRW.rndGenerator.NextFloat(0f, 15f));
        }
    }
}