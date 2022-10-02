using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpawner : MonoBehaviour
{
    [SerializeField]
    float timeBetweenSpawns;
    [SerializeField]
    float velocity;
    [SerializeField]
    GameObject projectile;
    float Timer;
    Rigidbody2D rb;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer >= timeBetweenSpawns)
        {
            GameObject spawned = Instantiate(projectile, transform.position, transform.rotation);
            spawned.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocity);
            Timer = 0;
        }
    }
}
