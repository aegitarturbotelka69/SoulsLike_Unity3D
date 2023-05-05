using UnityEngine;
using Unity.Entities;

namespace GLOBAL_FOR_TESTING
{
    public class PlayerTagAuthoring: MonoBehaviour
    {

    }

    public class PlayerTagBaker : Baker<PlayerTagAuthoring>
    {
        public override void Bake(PlayerTagAuthoring authoring)
        {
            AddComponent(new PlayerTag());
        }
    }
}