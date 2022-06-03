using System;
using UnityEngine;

namespace SLGame.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapons/Weapon", order = 0)]
    public class WeaponSO : ScriptableObject
    {
        /// <summary>
        /// unique weapon identifier
        /// </summary>
        public uint WeaponID;

        /// <summary>
        /// Weapon name displayed in UI and settings
        /// </summary>
        public string Name;

        /// <summary>
        /// Weapon Type (enum)
        /// </summary>
        public WeaponType Type;

        /// <summary>
        /// actual weapon object that will be spawned
        /// </summary>
        public GameObject Prefab;

        [Serializable]
        public class Transform
        {
            public Vector3 Position;
            public Vector3 Rotation;
        }

        /// <summary>
        /// Weapon position and rotation at player's hands
        /// </summary>
        public Transform ArmedTransform;

        /// <summary>
        /// Weapon position and rotation behinds player's back
        /// </summary>
        public Transform SteathedTransform;

        [Space(10)] public Sprite Image;
    }
}
