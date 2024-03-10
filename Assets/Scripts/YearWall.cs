using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YearWall : MonoBehaviour
{
    public Material activeMaterial;
    public Material deactiveMaterial;

    [SerializeField] GameObject otherGearWall1;
    [SerializeField] GameObject otherGearWall2;

    Renderer objectsRenderer;
    Player playerScript;

    public GameObject[] bullets;
    int collectedAmmo = 0;
    int deletedAmmo = 0;

    public int whichWall;
    public int howManyGears;

    bool workedOnce = false;

    void Start()
    {
        playerScript = playerScript = GameObject.Find("Player").GetComponent<Player>();
        objectsRenderer = GetComponent<Renderer>();
    }

   
    void Update()
    {
        collectedAmmo = playerScript.collectedAmmo;

        if (collectedAmmo > 24)
        collectedAmmo = 24;

        if(whichWall == 1)
        {
            deletedAmmo = 8;

            for (int i = 0; i < collectedAmmo; i++)
            {
                if(collectedAmmo <= 8)
                bullets[i].SetActive(true);
                else if (collectedAmmo > 8)
                {
                    for (int j = 0; j < 8; j++)
                        bullets[j].SetActive(true);
                }
            }

            for (int i = 7; i >= collectedAmmo; i--)
            {
                if (collectedAmmo >= 0)
                    bullets[i].SetActive(false);
            }

            if (collectedAmmo >= 8)
            {
                objectsRenderer.material = activeMaterial;
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }
            else
            {
                objectsRenderer.material = deactiveMaterial;
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
        else if (whichWall == 2)
        {
            deletedAmmo = 16;
            collectedAmmo = collectedAmmo - 8;

            if (collectedAmmo < 0)
                collectedAmmo = 0;

            for (int i = 0; i < collectedAmmo; i++)
            {
                if(collectedAmmo <= 8)
                bullets[i].SetActive(true);
                else if(collectedAmmo > 8)
                {
                    for(int j = 0; j < 8; j++)
                    bullets[j].SetActive(true);
                }
            }

            for (int i = 7; i >= collectedAmmo; i--)
            {
                if (collectedAmmo >= 0)
                    bullets[i].SetActive(false);
            }

            if (collectedAmmo >= 8)
            {
                objectsRenderer.material = activeMaterial;
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }
            else
            {
                objectsRenderer.material = deactiveMaterial;
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
        else if (whichWall == 3)
        {
            deletedAmmo = 18;
            collectedAmmo -= 16;

            if (collectedAmmo < 0)
                collectedAmmo = 0;

            for (int i = 0; i < collectedAmmo; i++)
            {
                if (collectedAmmo <= 8)
                bullets[i].SetActive(true);
                else if (collectedAmmo > 8)
                {
                    for (int j = 0; j < 8; j++)
                        bullets[j].SetActive(true);
                }
            }

            for (int i = 7; i >= collectedAmmo; i--)
            {
                if (collectedAmmo >= 0)
                    bullets[i].SetActive(false);
            }

            if (collectedAmmo >= 8)
            {
                objectsRenderer.material = activeMaterial;
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }
            else
            {
                objectsRenderer.material = deactiveMaterial;
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            otherGearWall1.GetComponent<BoxCollider>().enabled = false;
            otherGearWall2.GetComponent<BoxCollider>().enabled = false;

            Player playerScript = collision.gameObject.GetComponent<Player>();

            if (!workedOnce)
            {
                playerScript.gear += howManyGears;
                playerScript.collectedAmmo -= deletedAmmo;
                workedOnce = true;
            }
            Destroy(gameObject);
            Destroy(otherGearWall1);
            Destroy(otherGearWall2);
        }
    }
}
