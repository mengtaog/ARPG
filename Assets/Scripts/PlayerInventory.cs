using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MT
{

    public class PlayerInventory : MonoBehaviour
    {
        WeaponManager weaponManager;

        public WeaponItem leftHandWeapon;
        public WeaponItem rightHandWeapon;

        private void Awake()
        {
            weaponManager = GetComponentInChildren<WeaponManager>();
        }

        private void Start()
        {
            if (leftHandWeapon != null)
            {
                weaponManager.LoadWeaponOnSlot(leftHandWeapon, true);
            }

            if (rightHandWeapon != null)
            {
                weaponManager.LoadWeaponOnSlot(rightHandWeapon, false);
            }
        }
    }
}
