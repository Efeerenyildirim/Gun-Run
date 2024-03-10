using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Money : MonoBehaviour
{
    public float moneyAmount;
    [SerializeField] bool lastMoney = false;


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {


        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("sa");

            Player playerScript = collision.gameObject.GetComponent<Player>();
            playerScript.collectedMoney += moneyAmount * playerScript.moneyMultiplier;

            if (lastMoney)
            {
                playerScript.SavePlayerProgress();
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
            Destroy(gameObject);
        }
    }
}
