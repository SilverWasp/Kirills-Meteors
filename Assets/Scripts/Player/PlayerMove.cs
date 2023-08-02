using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float force = 10f;
    private bool isExelarating = false;
    private bool isSlowing = false;
    private bool isRight = false;
    private bool isLeft = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isExelarating = Input.GetKey(KeyCode.W);
        isSlowing = Input.GetKey(KeyCode.S);
        isRight = Input.GetKey(KeyCode.D);
        isLeft = Input.GetKey(KeyCode.A);
    }

    private void FixedUpdate()
    {
        if (isExelarating && !isSlowing)
        {
            rb.AddForce(transform.up * force);
        }
        else if (isSlowing && !isExelarating)
        {
            rb.AddForce(transform.up * -force);
        }

        if (isRight && !isLeft)
        {
            rb.AddForce(transform.right * force);
        }
        else if (isLeft && !isRight)
        {
            rb.AddForce(transform.right * -force);
        }
    }
}
