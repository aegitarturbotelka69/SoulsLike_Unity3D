using Unity.Entities;

namespace SLGame.Modules
{
    [GenerateAuthoringComponent]
    public struct GameObjectToEntity : IComponentData
    {
        public Entity _gameObjectToEntity;
    }
}