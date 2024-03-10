using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    public GameObject[] bullets;

    int howManyShoots = 0;
    bool workedOnce = false;
    [SerializeField] bool isPlayerCollider = false;
    [SerializeField] GameObject orijinalMagazine;

    Player playerScript;
    Magazine orijinalMagazineScript;

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        orijinalMagazineScript = orijinalMagazine.GetComponent<Magazine>();
    }


    void Update()
    {
        if(howManyShoots >= 6 && !workedOnce)
        {
            playerScript.collectedAmmo += 6;
            Destroy(gameObject);
            workedOnce = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Projectile") && !isPlayerCollider)
        {
            howManyShoots++;

            if (howManyShoots >= 6)
            howManyShoots = 6;

            if (howManyShoots >= 0 && howManyShoots <= 6)
            bullets[howManyShoots - 1].SetActive(true);
            
            

            if(howManyShoots < 6)
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && isPlayerCollider)
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            playerScript.collectedAmmo += orijinalMagazineScript.howManyShoots;
            Destroy(orijinalMagazine);
        }
    }
}
