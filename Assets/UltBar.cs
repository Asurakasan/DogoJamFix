using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltBar : MonoBehaviour
{
    public Slider slide;
    public static UltBar Instance { get; private set; }
    public void Awake()
    {
        Instance = this;
    }
    public void MaxValue(int MaxUlt)
    {
        slide.maxValue = MaxUlt;
    }
    public void SetUlt(int Ult)
    {
        slide.value = Ult;
    }
}
