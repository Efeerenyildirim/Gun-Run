using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barrier : MonoBehaviour
{
    public GameObject barrierUpgradeWall;
    public Slider slider;
    public float barrierHealth = 10;


    void Start()
    {
        slider.maxValue = barrierHealth;
    }

    void Update()
    {
        slider.value = barrierHealth;

        if(barrierHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            barrierHealth--;
            Destroy(collision.gameObject);
        }
    }
}
