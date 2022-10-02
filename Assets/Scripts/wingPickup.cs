using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wingPickup : MonoBehaviour
{
    [SerializeField]
    GameObject pickupEffect;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerMovement.hasWings = true;
            Instantiate(pickupEffect, transform.position, transform.rotation);
                        gameObject.SetActive(false);
        }
    }
}