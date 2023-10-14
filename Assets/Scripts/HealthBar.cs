using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void Set_MaxHealth(int maxhealth)
    {
        slider.maxValue = maxhealth;
        slider.value = maxhealth;
    }

    public void Set_Health(int health)
    {
        slider.value = health;
    }
}
