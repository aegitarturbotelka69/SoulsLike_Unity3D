using System;
using SLGame.ScriptableObjects;

namespace SLGame.Gameplay
{
    public class WeaponArgs : EventArgs
    {
        public WeaponSO Weapon { get; set; }
        public WeaponType WeaponType { get; set; }
    }
}