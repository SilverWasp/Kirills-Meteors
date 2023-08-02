using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookShoot : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private Lazer lazer;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 direction = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        { 
            Lazer newLazer = Instantiate(lazer, transform.position, rotation);
            newLazer.Shoot(transform.up);
        }

        
    }
}
