using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLGame.Gameplay
{
    public class WeaponArmedPosition : MonoBehaviour
    {
        public static Transform Transform { get; private set; }
        public void Awake()
        {
            WeaponArmedPosition.Transform = this.gameObject.transform;
        }
    }
}