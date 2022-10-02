using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePowerUp : MonoBehaviour
{
    [SerializeField]
    float howMuchToAdd;
    [SerializeField]
    GameObject pickupEffect;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            HeatSystem.AddTime(howMuchToAdd);
            Instantiate(pickupEffect, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
