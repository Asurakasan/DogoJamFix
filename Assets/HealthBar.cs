using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slide;
    public static HealthBar Instance { get; private set; }
    public void Awake()
    {
        Instance = this;
    }
    public void MaxValue(int Maxhealth)
    {
        slide.maxValue = Maxhealth;
        slide.value = Maxhealth;
    }
    public void SetHealth(int health)
    {
        slide.value = health;
    }
}
