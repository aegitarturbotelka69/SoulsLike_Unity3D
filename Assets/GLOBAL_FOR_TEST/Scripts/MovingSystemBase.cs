using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor.Rendering;

namespace GLOBAL_FOR_TESTING
{
    public partial class MovingSystemBase : SystemBase
    {
        protected override void OnUpdate()
        {
            //foreach (MoveToPositionAspect moveAspect in SystemAPI.Query<MoveToPositionAspect>())
            //{
            //    moveAspect.Move(SystemAPI.Time.DeltaTime, SystemAPI.GetSingletonRW<RandomComponent>());
            //}

            //Entities.ForEach((TransformAspect aspect) =>
            //{
            //    aspect.Position += new float3(SystemAPI.Time.DeltaTime, 0f, 0f);
            //}).Schedule();
        }
    }
}