using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    [SerializeField] GameObject obstacleHealthTextObject;

    TextMeshProUGUI obstacleHealthText;

    public float obstacleHealth;
    float projectilePower;
    Player playerScript;



    void Start()
    {
        obstacleHealthText = obstacleHealthTextObject.GetComponent<TextMeshProUGUI>();
        playerScript = playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    
    void Update()
    {
        projectilePower = playerScript.projectilePower;
        obstacleHealthText.text = obstacleHealth.ToString();

        if(obstacleHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            obstacleHealth -= projectilePower;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            obstacleHealth =- playerScript.projectilePower;

            playerScript.SavePlayerProgress();
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
