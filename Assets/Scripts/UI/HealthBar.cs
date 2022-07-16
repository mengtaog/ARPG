using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MT
{


    public class HealthBar : MonoBehaviour
    {
        public Slider slider;



        public void SetMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
        }

        public void SetHealth(int health)
        {
            slider.value = health;
        }

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }
    }
}