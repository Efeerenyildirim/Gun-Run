using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeWall : MonoBehaviour
{
    [SerializeField] GameObject upgradeTypeTextObject;
    [SerializeField] GameObject upgradeValueChangeTextObject;
    [SerializeField] GameObject upgradeValueTextObject;

    [SerializeField] GameObject otherUpgradeWall;

    TextMeshProUGUI upgradeTypeText;
    TextMeshProUGUI upgradeValueChangeText;
    TextMeshProUGUI upgradeValueText;

    public string upgradeType = "Fire Rate";
    public float upgradeValueChange = 1f;
    public float upgradeValue = 5f;

    int randomNumber;
    int randomUpgradeType;
    int randomUpgradeValueChange;
    int randomUpgradeValue;

    string[] upgradeTypes = new string[] { "Fire Rate", "Fire Range", "Gear" };

    bool once = true;
    public bool isDouble;

    void Start()
    {
        upgradeTypeText = upgradeTypeTextObject.GetComponent<TextMeshProUGUI>();
        upgradeValueChangeText = upgradeValueChangeTextObject.GetComponent<TextMeshProUGUI>();
        upgradeValueText = upgradeValueTextObject.GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        if (once)
        {
            randomNumber = Random.Range(0, 10);

            if (randomNumber >= 0 && randomNumber < 5)
            {
                randomUpgradeType = 0;
                randomUpgradeValueChange = Random.Range(1, 5);
                randomUpgradeValue = Random.Range(-20, 30);
            }
            else if (randomNumber >= 5 && randomNumber < 8)
            {
                randomUpgradeType = 1;
                randomUpgradeValueChange = Random.Range(1, 3);
                randomUpgradeValue = Random.Range(-10, 10);
            }
            else if (randomNumber >= 8 && randomNumber < 10)
            {
                randomUpgradeType = 2;
                randomUpgradeValueChange = Random.Range(1, 3);
                randomUpgradeValue = Random.Range(-10, 10);
            }

            upgradeType = upgradeTypes[randomUpgradeType];
            upgradeValueChange = randomUpgradeValueChange;
            upgradeValue = randomUpgradeValue;

            once = false;
        }

        upgradeTypeText.text = upgradeType;
        upgradeValueChangeText.text = "+" + upgradeValueChange.ToString();
        upgradeValueText.text = upgradeValue.ToString();
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            upgradeValue += upgradeValueChange;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if(isDouble)
            otherUpgradeWall.GetComponent<BoxCollider>().enabled = false;

            Player playerScript = collision.gameObject.GetComponent<Player>();
            
            if(randomUpgradeType == 0)
            {
                playerScript.fireRate += upgradeValue;
            }
            else if(randomUpgradeType == 1)
            {
                playerScript.projectileLifeTime += upgradeValue;
            }
            else if((randomUpgradeType == 2))
            {
                playerScript.gear += upgradeValue;
            }
            Destroy(gameObject);
        }
    }
}
