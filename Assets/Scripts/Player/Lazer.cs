using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 50f;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Background") 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Meteor")
        {
            Destroy(gameObject);
        }
    }
}
