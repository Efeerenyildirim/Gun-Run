using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 10f;
    public float projectileLifeTime = 100f;
    public float projectilePower = 1f;

    public float directionX = 0, directionZ = 1;

    public Vector3 bulletDirection;

    void Start()
    {
        StartCoroutine(DestroyBullet());
    }

   
    void Update()
    {
        bulletDirection = new Vector3(directionX, 0, directionZ);

        transform.Translate(bulletDirection * projectileSpeed * Time.deltaTime, Space.World);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(projectileLifeTime/100);
        Destroy(gameObject);
    }
}
