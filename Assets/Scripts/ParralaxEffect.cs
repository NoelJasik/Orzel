using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxEffect : MonoBehaviour
{
    [SerializeField]
    GameObject[] elementsToParralax;
    [SerializeField]
    float[] speeds;
    [SerializeField]
    Transform baseOn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < elementsToParralax.Length; i++)
        {
            elementsToParralax[i].transform.localPosition = new Vector3(baseOn.position.x * speeds[i],baseOn.position.y * speeds[i],transform.localPosition.z);
        }
    }
}
