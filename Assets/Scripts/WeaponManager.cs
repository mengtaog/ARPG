using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{
    public class WeaponManager : MonoBehaviour
    {
        public WeaponHolderSlot rightHandSlot;
        public WeaponHolderSlot leftHandSlot;

        private DamageCollider leftHandDamageCollider;
        private DamageCollider rightHandDamageCollider;


        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                leftHandDamageCollider = GetComponentInChildren<DamageCollider>();
            }
            else
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                rightHandDamageCollider = GetComponentInChildren<DamageCollider>();
            }
        }

        public void OpenRightHandDamageCollider()
        {
            rightHandDamageCollider.EnableDamageCollider();
        }

        public void CloseRightHandDamageCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }

        public void OpenLeftHandDamageCollider()
        {
            leftHandDamageCollider.EnableDamageCollider();
        }

        public void CloseLeftHandDamageCollider()
        {
            leftHandDamageCollider.DisableDamageCollider();
        }

        



        private void Awake()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (var slot in weaponHolderSlots)
            {
                if (slot.isLeftHandSlot)
                {
                    leftHandSlot = slot;
                }
                else if (slot.isRightHandSlot)
                {
                    rightHandSlot = slot;
                }
            }
        }

        
    }
}