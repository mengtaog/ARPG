using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MT
{

    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject prefab;
        public bool isUnarmed;
    }
}
