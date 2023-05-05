using Unity.Entities;
using UnityEngine;

namespace GLOBAL_FOR_TESTING
{
    public class EntityComponentAuthoring: MonoBehaviour
    {
        public float speed;
    }

    public class EntityComponentBaker : Baker<EntityComponentAuthoring>
    {
        public override void Bake(EntityComponentAuthoring authoring)
        {
            AddComponent(new EntityComponent
            {
                speed = authoring.speed
            });
        }
    }
}