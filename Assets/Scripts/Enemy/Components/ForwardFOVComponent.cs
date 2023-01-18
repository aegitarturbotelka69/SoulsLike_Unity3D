using System;
using UnityEngine;
using UnityEngine.AI;

namespace SLGame.Enemy
{
    [Serializable]
    public struct ForwardFOVComponent
    {
        /// <summary>
        /// Radius of view
        /// </summary>
        public float Radius;

        /// <summary>
        /// View angle
        /// </summary>
        public float Angle;

        /// <summary>
        /// What kind of entities we are search?
        /// </summary>
        public LayerMask TargetMask;

        /// <summary>
        /// All obstructions
        /// </summary>
        public LayerMask ObstructionMask;
    }
}