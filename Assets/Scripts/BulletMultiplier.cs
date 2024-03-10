using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMultiplier : MonoBehaviour
{
    public Transform firePoint1, firePoint2;
    public GameObject projectilePrefab;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f);
            Vector3 newFirePoint1Position = firePoint1.position + randomOffset;
            Vector3 newFirePoint2Position = firePoint2.position + randomOffset;

            GameObject newProjectile1 = Instantiate(projectilePrefab, newFirePoint1Position, firePoint1.rotation);
            GameObject newProjectile2 = Instantiate(projectilePrefab, newFirePoint2Position, firePoint2.rotation);

            Projectile projectileScrpit1 = newProjectile1.GetComponent<Projectile>();
            Projectile projectileScrpit2 = newProjectile2.GetComponent<Projectile>();

            projectileScrpit1.directionX = 1;
            projectileScrpit1.projectileSpeed = 8f;
            projectileScrpit1.projectileLifeTime = 100f;
            projectileScrpit1.projectilePower = 1f;

            projectileScrpit2.directionX = -1;
            projectileScrpit2.projectileSpeed = 8f;
            projectileScrpit2.projectileLifeTime = 100f;
            projectileScrpit2.projectilePower = 1f;

            Destroy(collision.gameObject);
        }

    }
}
