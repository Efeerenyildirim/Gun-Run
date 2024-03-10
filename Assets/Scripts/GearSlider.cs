using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearSlider : MonoBehaviour
{
    public GameObject player;
    Player playerScript;

    Slider slider;
    float currentValue;
    float maxValue = 40;

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        playerScript = player.GetComponent<Player>();
        slider.maxValue = maxValue;
        UpdateProgressBar();
    }

    void Update()
    {
        currentValue = playerScript.gear;

        if (currentValue > maxValue)
        currentValue = maxValue;
        
        UpdateProgressBar();
    }

    void UpdateProgressBar()
    {
        slider.value = currentValue;
    }
}
