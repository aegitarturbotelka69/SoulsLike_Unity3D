using SLGame.ScriptableObjects;
using UnityEngine;

namespace SLGame.Gameplay
{
    public interface IWeaponEquiper
    {
        void EquipWeapon(object sender, WeaponArgs weapon);
    }
    public interface IWeaponSteather
    {
        void SteathWeapon();
    }
    public class Weapon : MonoBehaviour, IWeaponEquiper, IWeaponSteather
    {
        [SerializeField] private WeaponSO _weapon;

        [SerializeField] private WeaponType _type;
        public void EquipWeapon(object sender, WeaponArgs selectedWeapon)
        {
            if (_type == selectedWeapon.WeaponType)
            {
                // TODO: Possibly need to call Instanciate or something
                // !     Main logic to apply weapon to player's hands
            }
        }

        public void SteathWeapon()
        {
            throw new System.NotImplementedException();
        }
    }
}