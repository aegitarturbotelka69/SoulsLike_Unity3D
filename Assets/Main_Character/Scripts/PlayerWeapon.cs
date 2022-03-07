using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

using SLGame.Input;
using SLGame.Modules;

namespace SLGame.Gameplay
{
    public class PlayerWeapon : MonoBehaviour
    {
        [Serializable]
        public class Weapon
        {
            public uint WeaponID;
            public string Name;

            public WeaponType WeaponType;
            public GameObject WeaponPrefab;
            public GameObject ArmedPosition;
            public GameObject SteathedPosition;
        }
        [Header("References:")]
        [SerializeField] private Animator _playerAnimator;

        [Header("Stats: ")]
        [SerializeField] private Weapon Unarmed;
        [SerializeField] private List<Weapon> ListOfWeapons = new List<Weapon>();

        [Header("In game:")]

        [SerializeField] private int _currentSelectedWeaponIndex;
        [SerializeField] public Weapon CurrentEquipedWeapon;
        [SerializeField] private Weapon _currentSelectedWeapon;

        [SerializeField] public bool WeaponEquiped = false;

        private void EquipWeapon()
        {
            Debug.LogWarning("Changing from steathed to armed parent position for weapon");

            _currentSelectedWeapon.SteathedPosition.transform.GetChild(0).gameObject.transform.parent = _currentSelectedWeapon.ArmedPosition.transform;
            _currentSelectedWeapon.ArmedPosition.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            _currentSelectedWeapon.ArmedPosition.transform.GetChild(0).gameObject.transform.localRotation = Quaternion.identity;
        }

        private void SteathWeapon()
        {
            Debug.LogWarning("Changing from armed to steathed parent position for weapon");

            _currentSelectedWeapon.ArmedPosition.transform.GetChild(0).gameObject.transform.parent = _currentSelectedWeapon.SteathedPosition.transform;
            _currentSelectedWeapon.SteathedPosition.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            _currentSelectedWeapon.SteathedPosition.transform.GetChild(0).gameObject.transform.localRotation = Quaternion.identity;
        }

        private void InitializeWeaponPrefabs()
        {
            foreach (Weapon weapon in ListOfWeapons)
            {
                GameObject obj = GameObject.Instantiate(weapon.WeaponPrefab, weapon.SteathedPosition.transform);
            }
        }
        private void Awake()
        {
            _playerAnimator = this.gameObject.GetComponent<Animator>();
            _currentSelectedWeaponIndex = 0;
            _currentSelectedWeapon = ListOfWeapons[_currentSelectedWeaponIndex];
            InitializeWeaponPrefabs();
        }
        private void Update()
        {
            if (VirtualInputManager.Instance.EquipWeapon)
            {
                switch (WeaponEquiped)
                {
                    case true:
                        WeaponEquiped = false;
                        _playerAnimator.SetBool("EquipWeapon", WeaponEquiped);
                        break;
                    case false:
                        CurrentEquipedWeapon = _currentSelectedWeapon;
                        WeaponEquiped = true;
                        _playerAnimator.SetBool("EquipWeapon", WeaponEquiped);
                        break;
                }
            }

            if (VirtualInputManager.Instance.MoveToTopSelected)
            {
                try
                {
                    _currentSelectedWeaponIndex++;
                    _currentSelectedWeapon = ListOfWeapons[_currentSelectedWeaponIndex];
                }
                catch
                {
                    _currentSelectedWeaponIndex = 0;
                    _currentSelectedWeapon = ListOfWeapons[_currentSelectedWeaponIndex];
                }
            }
        }
    }
}