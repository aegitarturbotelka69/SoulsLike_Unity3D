using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SLGame.Gameplay
{
    public class WeaponSteathedPosition : MonoBehaviour
    {
        [SerializeField] public static Transform Transform { get; private set; }
        public void Awake()
        {
            WeaponSteathedPosition.Transform = this.gameObject.transform;
        }
    }
}