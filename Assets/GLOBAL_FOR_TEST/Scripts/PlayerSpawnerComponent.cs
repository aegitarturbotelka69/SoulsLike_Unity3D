﻿using Unity.Entities;
using UnityEngine;

namespace GLOBAL_FOR_TESTING
{
    public struct PlayerSpawnerComponent: IComponentData
    {
        public Entity playerPrefab;
    }
}