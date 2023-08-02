using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Meteor : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 20f;
    [SerializeField] private int points;
    public static event Action<int> OnLaserHitMeteor;
    public static event Action<int> OnMeteorHitPlayer;
    



    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    public void Kick(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
        rb.AddTorque(UnityEngine.Random.Range(-50, 50));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Background")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            OnLaserHitMeteor?.Invoke(points);
            // make VFX with particle system explosion prefab, make instanciate in meteor position
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // move to player manager and reset score when dead after saving in hall of fame
    {
        if (collision.gameObject.tag == "Player" && FindObjectOfType<StageManager>().GetGameState() == true)
        {
           
            collision.gameObject.SetActive(false); // player
            gameObject.SetActive(false); // meteor
            // make VFX with particle system explosion prefab, make instanciate in player position

            Destroy(collision.gameObject);
            // wait for time with coroutine
            // move to Lose scene
            OnMeteorHitPlayer?.Invoke(-1);
            Destroy(gameObject);
        }
    }

    
}
