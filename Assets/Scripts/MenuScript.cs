using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject player;

    Player playerScript;

    [SerializeField] Button fireRateUpgradeButton;
    [SerializeField] Button fireRangeUpgradeButton;
    [SerializeField] Button moneyMultiplierUpgradeButton;

    [SerializeField] Sprite activeButtonSprite;
    [SerializeField] Sprite deactiveButtonSprite;

    [SerializeField] GameObject fireRateLevelTextObject;
    [SerializeField] GameObject fireRateCostTextObject;

    TextMeshProUGUI fireRateLevelText;
    TextMeshProUGUI fireRateCostText;

    [SerializeField] GameObject fireRangeLevelTextObject;
    [SerializeField] GameObject fireRangeCostTextObject;

    TextMeshProUGUI fireRangeLevelText;
    TextMeshProUGUI fireRangeCostText;

    [SerializeField] GameObject moneyMultiplierLevelTextObject;
    [SerializeField] GameObject moneyMultiplierCostTextObject;

    [SerializeField] GameObject tutorialBoard;

    TextMeshProUGUI moneyMultiplierLevelText;
    TextMeshProUGUI moneyMultiplierCostText;


    float fireRateUpgradeCost = 0f;
    float fireRangeUpgradeCost = 0f;
    float moneyMultiplierUpgradeCost = 0f;

    int firstTime = 0;

    void Start()
    {
        fireRateLevelText = fireRateLevelTextObject.GetComponent<TextMeshProUGUI>();
        fireRateCostText = fireRateCostTextObject.GetComponent<TextMeshProUGUI>();

        fireRangeLevelText = fireRangeLevelTextObject.GetComponent<TextMeshProUGUI>();
        fireRangeCostText = fireRangeCostTextObject.GetComponent<TextMeshProUGUI>();

        moneyMultiplierLevelText = moneyMultiplierLevelTextObject.GetComponent<TextMeshProUGUI>();
        moneyMultiplierCostText = moneyMultiplierCostTextObject.GetComponent<TextMeshProUGUI>();

        playerScript = player.GetComponent<Player>();
        Pause();

        firstTime = PlayerPrefs.GetInt("firstTime");

        if (firstTime == 0)
            tutorialBoard.SetActive(true);
    }

    void Update()
    {
        fireRateLevelText.text = "Level " + playerScript.fireRateUpgradeLevel.ToString();
        fireRangeLevelText.text = "Level " + playerScript.fireRangeUpgradeLevel.ToString();
        moneyMultiplierLevelText.text = "Level " + playerScript.moneyMultiplierUpgradeLevel.ToString();

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }

        fireRateUpgradeCost = 200 + playerScript.fireRateUpgradeLevel * 100;
        fireRangeUpgradeCost = 200 + playerScript.fireRangeUpgradeLevel * 100;
        moneyMultiplierUpgradeCost = 200 + playerScript.moneyMultiplierUpgradeLevel * 100;

        fireRateCostText.text = fireRateUpgradeCost.ToString();
        fireRangeCostText.text = fireRangeUpgradeCost.ToString();
        moneyMultiplierCostText.text = moneyMultiplierUpgradeCost.ToString();

        if (playerScript.collectedMoney >= fireRateUpgradeCost)
        {
            fireRateUpgradeButton.image.sprite = activeButtonSprite;

        }
        else
        {
            fireRateUpgradeButton.image.sprite = deactiveButtonSprite;
        }

        if (playerScript.collectedMoney >= fireRangeUpgradeCost)
        {
            fireRangeUpgradeButton.image.sprite = activeButtonSprite;
        }
        else
        {
            fireRangeUpgradeButton.image.sprite = deactiveButtonSprite;
        }

        if (playerScript.collectedMoney >= moneyMultiplierUpgradeCost)
        {
            moneyMultiplierUpgradeButton.image.sprite = activeButtonSprite;
        }
        else
        {
            moneyMultiplierUpgradeButton.image.sprite = deactiveButtonSprite;
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        playerScript.projectileLifeTime = 100f + playerScript.fireRangeUpgradeLevel * 15f;
        playerScript.fireRate = 30f + playerScript.fireRateUpgradeLevel * 5f;
        playerScript.moneyMultiplier = 1 + playerScript.moneyMultiplierUpgradeLevel * 0.1f;

        isPaused = false;
        startMenu.SetActive(false);
    }

    public void upgradeFireRate()
    {
        if (playerScript.collectedMoney >= fireRateUpgradeCost)
        {
            playerScript.fireRateUpgradeLevel++;
            playerScript.collectedMoney -= fireRateUpgradeCost;
            PlayerPrefs.SetFloat("fireRateUpgradeLevel", playerScript.fireRateUpgradeLevel);
            PlayerPrefs.Save();
        }
    }

    public void upgradeFireRange()
    {
        if (playerScript.collectedMoney >= fireRangeUpgradeCost)
        {
            playerScript.fireRangeUpgradeLevel++;
            playerScript.collectedMoney -= fireRangeUpgradeCost;
            PlayerPrefs.SetFloat("fireRangeUpgradeLevel", playerScript.fireRangeUpgradeLevel);
            PlayerPrefs.Save();
        }
    }

    public void upgradeMoneyMultiplier()
    {
        if (playerScript.collectedMoney >= moneyMultiplierUpgradeCost)
        {
            playerScript.moneyMultiplierUpgradeLevel++;
            playerScript.collectedMoney -= moneyMultiplierUpgradeCost;
            PlayerPrefs.SetFloat("moneyMultiplierUpgradeLevel", playerScript.moneyMultiplierUpgradeLevel);
            PlayerPrefs.Save();
        }
    }

    public void tutorialButton()
    {
        tutorialBoard.SetActive(false);
        PlayerPrefs.SetInt("firstTime", 1);
        PlayerPrefs.Save();
    }
}
