using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollower : MonoBehaviour
{
    [SerializeField]
    Transform thingToFollow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(Mathf.Clamp(thingToFollow.position.x,-1, 500), Mathf.Clamp(thingToFollow.position.y - 2f,0, 500), transform.position.z);
    }
}
