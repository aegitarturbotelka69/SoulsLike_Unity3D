using Unity.Entities;
using UnityEngine;

namespace GLOBAL_FOR_TESTING
{
    public class RandomAuthoring: MonoBehaviour
    {

    }

    public class RandomBaker: Baker<RandomAuthoring>
    {
        public override void Bake(RandomAuthoring randomAuthoring)
        {
            AddComponent(new RandomComponent
            {
                rndGenerator = new Unity.Mathematics.Random(1)
            });
        }
    }
}