using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOnTouch : MonoBehaviour
{
    [SerializeField]
    GameObject winScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
               other.GetComponent<PlayerMovement>().rb.velocity = new Vector2(0,0);
            other.GetComponent<PlayerMovement>().enabled = false;
            other.GetComponent<HeatSystem>().enabled = false;
            winScreen.SetActive(true);
        }
    }
}
