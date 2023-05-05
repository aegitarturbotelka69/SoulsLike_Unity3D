using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace GLOBAL_FOR_TESTING
{
    public class TargetPositionAuthoring: MonoBehaviour
    {
        public float3 value;
    }

    public class TargetPositionBaker : Baker<TargetPositionAuthoring>
    {
        public override void Bake(TargetPositionAuthoring authoring)
        {
            AddComponent(new TargetPosition
            {
                value = authoring.value,
            });
        }
    }
}