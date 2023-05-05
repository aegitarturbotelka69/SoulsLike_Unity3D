using Unity.Entities;

namespace GLOBAL_FOR_TESTING { 
    public struct RandomComponent: IComponentData
    {
        public Unity.Mathematics.Random rndGenerator;
    }
}
