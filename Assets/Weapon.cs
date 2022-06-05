using System;
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
        [SerializeField] private PlayerWeapon _playerWeapon;
        [SerializeField] private WeaponSO _weaponScriptableObject;
        [SerializeField] private WeaponType _type;
        [SerializeField] private GameObject _weaponGameObject;

        /// <summary>
        /// C'tor realization for Monobehaviour Weapon
        /// </summary>
        /// <param name="mainGameObject">Gameobject to add component</param>
        /// <param name="playerWeapon">"Player hands"</param>
        /// <param name="weaponType">WeaponType</param>
        /// <returns></returns>
        public static Weapon CreateComponent(GameObject mainGameObject, PlayerWeapon playerWeapon, WeaponType weaponType)
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
                _weaponScriptableObject = selectedWeapon.Weapon;

                AssetLoader loader = new AssetLoader();
                _weaponGameObject = await loader.Load(_weaponScriptableObject.AdressablesPrefabPath.ToString());
                _weaponGameObject.transform.SetParent(_playerWeapon._steathedWeaponParentPosition);
                _weaponGameObject.transform.localPosition = _weaponScriptableObject.SteathedTransform.Position;
                _weaponGameObject.transform.localRotation = Quaternion.Euler(_weaponScriptableObject.SteathedTransform.Rotation);
            }
        }

        public void SteathWeapon()
        {
            throw new System.NotImplementedException();
        }
    }
}