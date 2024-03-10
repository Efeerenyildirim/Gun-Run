using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    float speed = 2f;
    float currentPosition;

    float direction = 1f;

    void Start()
    {
        currentPosition = transform.position.x;
    }


    void Update()
    {

        Vector3 newPosition = transform.position + Vector3.right * speed * direction * Time.deltaTime;

        if (newPosition.x < currentPosition - 1f || newPosition.x > currentPosition + 1f)
        {
            direction *= -1f;
        }

        newPosition.x = Mathf.Clamp(newPosition.x, currentPosition - 1f, currentPosition + 1f);
        transform.position = newPosition;
    }
}
