using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{


    public GameObject projectilePrefab;
    public Transform firePoint;
    public GameObject moneyTextObject;

    Projectile projectileScrpit;
    TextMeshProUGUI moneyText;

    public GameObject[] weapons;

    float forwardMoveSpeed = 3f;
    public float sideMoveSpeed = 4f;

    public float projectileSpeed = 10f;
    public float projectileLifeTime = 100f;
    public float projectilePower = 1f;
    public float fireRate = 30f;
    public float gear = 0f;
    public int collectedAmmo = 0;
    public float collectedMoney = 0f;
    public float moneyMultiplier = 0f;

    public float fireRateUpgradeLevel = 0f;
    public float fireRangeUpgradeLevel = 0f;
    public float moneyMultiplierUpgradeLevel = 0f;

    bool[] weaponActiveStates = new bool[5];

    void Start()
    {
        collectedMoney = PlayerPrefs.GetFloat("Money");
        fireRateUpgradeLevel = PlayerPrefs.GetFloat("fireRateUpgradeLevel");
        fireRangeUpgradeLevel = PlayerPrefs.GetFloat("fireRangeUpgradeLevel");
        moneyMultiplierUpgradeLevel = PlayerPrefs.GetFloat("moneyMultiplierUpgradeLevel");

        projectileLifeTime = 100f + fireRangeUpgradeLevel * 15f;
        fireRate = 30f + fireRateUpgradeLevel * 5f;
        moneyMultiplier = 1 + moneyMultiplierUpgradeLevel * 0.1f;

        moneyText = moneyTextObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(ContinuousShoot());
    }


    void Update()
    {
        if (gear < 0f)
            gear = 0f;

        if (fireRate > 1000)
            fireRate = 1000;

        transform.Translate(Vector3.forward * forwardMoveSpeed * Time.deltaTime, Space.World);

        /*
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if(this.gameObject.transform.position.x > -2.25f)
            {
                transform.Translate(Vector3.left * sideMoveSpeed * Time.deltaTime, Space.World);
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (this.gameObject.transform.position.x < 2.25f)
            {
                transform.Translate(Vector3.right * sideMoveSpeed * Time.deltaTime, Space.World);
            }
        }*/

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.x > Screen.width / 2)
            {
                if (this.gameObject.transform.position.x < 2.25f)
                {
                    transform.Translate(Vector3.right * sideMoveSpeed * Time.deltaTime, Space.World);
                }
            }
            else if (touch.position.x < Screen.width / 2)
            {

                if (this.gameObject.transform.position.x > -2.25f)
                {
                    transform.Translate(Vector3.left * sideMoveSpeed * Time.deltaTime, Space.World);
                }
            }
        }

        projectilePower = 1 + Mathf.Floor(gear / 10);

        if (gear >= 0 && gear < 10)
        {
            SetWeaponActiveStates(0);
        }
        else if (gear >= 10 && gear < 20)
        {
            SetWeaponActiveStates(1);
        }
        else if (gear >= 20 && gear < 30)
        {
            SetWeaponActiveStates(2);
        }
        else if (gear >= 30 && gear < 40)
        {
            SetWeaponActiveStates(3);
        }
        else if (gear >= 40)
        {
            SetWeaponActiveStates(4);
        }


        moneyText.text = collectedMoney.ToString();

        if(collectedMoney >= 1000)
        {
            float collectedMoneyText = collectedMoney / 1000;
            moneyText.text = collectedMoneyText.ToString() + "k";
        }
    }

    void SetWeaponActiveStates(int activeIndex)
    {

        for (int i = 0; i < weaponActiveStates.Length; i++)
        {
            weaponActiveStates[i] = false;
        }

        weaponActiveStates[activeIndex] = true;

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(weaponActiveStates[i]);
        }
    }

    IEnumerator ContinuousShoot()
    {
        while (true)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f);
            Vector3 newFirePointPosition = firePoint.position + randomOffset;


            GameObject newProjectile = Instantiate(projectilePrefab, newFirePointPosition, firePoint.rotation);
            projectileScrpit = newProjectile.GetComponent<Projectile>();

            projectileScrpit.projectileSpeed = projectileSpeed;
            projectileScrpit.projectileLifeTime = projectileLifeTime;
            projectileScrpit.projectilePower = projectilePower;

            yield return new WaitForSeconds(10f/fireRate);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Decelerator"))
        {
            forwardMoveSpeed = 2.7f;
        }

        if (collision.gameObject.CompareTag("Accelerator"))
        {
            forwardMoveSpeed = 4.2f;
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            gear--;
            forwardMoveSpeed = -3f;
            Invoke("returnMovementSpeedNormal", 0.5f);
        }

        if (collision.gameObject.CompareTag("Saw"))
        {
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(collision.gameObject);
            gear--;
            forwardMoveSpeed = -3f;
            Invoke("returnMovementSpeedNormal", 0.5f);
        }

        if (collision.gameObject.CompareTag("Barrier"))
        {
            Barrier barrierScript = collision.gameObject.GetComponent<Barrier>();
            Destroy(barrierScript.barrierUpgradeWall);

            Destroy(collision.gameObject);

            gear--;
            forwardMoveSpeed = -3f;
            Invoke("returnMovementSpeedNormal", 0.5f);
        }
    }

    void returnMovementSpeedNormal()
    {
        forwardMoveSpeed = 3.5f;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Decelerator"))
        {
            forwardMoveSpeed = 3.5f;
        }

        if (collision.gameObject.CompareTag("Accelerator"))
        {
            forwardMoveSpeed = 3.5f;
        }
    }

    public void SavePlayerProgress()
    {
        PlayerPrefs.SetFloat("Money", collectedMoney);
        PlayerPrefs.Save();
    }

    public void ResetPlayerProgress()
    {
        Debug.Log("reset");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
