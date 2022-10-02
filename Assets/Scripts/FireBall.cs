using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    float damage;
    [SerializeField]
    GameObject attackEffect;
    float Timer;
    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start()
    {
         Destroy(gameObject, 2f);
         rb = GetComponent<Rigidbody2D>();
         sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y < 0)
        {
          sr.flipY = true;
        } else
        {
             sr.flipY = false;
        }
        Timer -= Time.deltaTime;
        if(Timer < 0)
        {
            sr.flipX = !sr.flipX;
            Timer = 0.1f;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            HeatSystem.RemoveTime(damage);
            Instantiate(attackEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
