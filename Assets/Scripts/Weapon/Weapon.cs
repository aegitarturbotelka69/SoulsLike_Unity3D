using System;
using SLGame.Input;
using SLGame.Modules;
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
    [Serializable]
    public class Weapon : MonoBehaviour, IWeaponEquiper, IWeaponSteather
    {
        [Header("References:")]
        [SerializeField] private WeaponLogicAssembler _playerWeapon;

        [Header("Stats: ")]
        [SerializeField] private WeaponType _type;
        [SerializeField] private GameObject _weaponGameObject;
        [SerializeField] public WeaponSO WeaponSO;

        /// <summary>
        /// C'tor realization for Monobehaviour Weapon
        /// </summary>
        /// <param name="mainGameObject">Gameobject to add component</param>
        /// <param name="playerWeapon">"Player hands"</param>
        /// <param name="weaponType">WeaponType</param>
        /// <returns></returns>
        public static Weapon CreateComponent(GameObject mainGameObject, WeaponLogicAssembler playerWeapon, WeaponType weaponType)
        {
            Weapon weaponComponent = mainGameObject.AddComponent<Weapon>();
            weaponComponent._playerWeapon = playerWeapon;
            weaponComponent._type = weaponType;

            return weaponComponent;
        }

        public async void EquipWeapon(object sender, WeaponArgs selectedWeapon)
        {
            if (_type == selectedWeapon.WeaponType)
            {
                WeaponSO = selectedWeapon.Weapon;

                AssetLoader loader = new AssetLoader(WeaponSO.AdressablesPrefabPath.ToString());

                _weaponGameObject = await loader.Load();
                _weaponGameObject.transform.SetParent(_playerWeapon._steathedWeaponParentPosition);
                _weaponGameObject.transform.localPosition = WeaponSO.SteathedTransform.Position;
                _weaponGameObject.transform.localRotation = Quaternion.Euler(WeaponSO.SteathedTransform.Rotation);
            }
        }

        public void SteathWeapon()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get this weapon type
        /// </summary>
        /// <returns>WeaponType</returns>
        public WeaponType GetWeaponType() => _type;
    }
}