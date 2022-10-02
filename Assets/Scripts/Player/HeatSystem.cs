using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatSystem : MonoBehaviour
{
    static public float heatTimer;
    [SerializeField]
    string coolerTag;
    public bool isNearCooler;
    public float maxHeat;
    [SerializeField]
    Slider heatDisplay;
    [SerializeField]
    Image heatImageDisplay;
    [SerializeField]
    GameObject DeathScreen;
    Vector3 lastCheckpoint;
    [SerializeField]
    float respawnTime;
    float timerForRespawn;
    [SerializeField]
    GameObject playerSprite;
    PlayerMovement pm;
    bool isRespawning;
    public static bool isHotter = false;
    [SerializeField]
    GameObject deathParticle;
    [SerializeField]
    GameObject[] heatStates;
    [SerializeField]
    GameObject hitEffect;
    static float hitTimer;

    [SerializeField]
    Transform pickupHolder;
    public List<GameObject> pickups = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        heatTimer = maxHeat;
        timerForRespawn = respawnTime;
        pm = GetComponent<PlayerMovement>();
        foreach(Transform child in pickupHolder)
        {
            pickups.Add(child.gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
         if(hitTimer >= 0.05)
        {
             hitEffect.SetActive(true);
        } else
        {
                   hitEffect.SetActive(false);
        }
        hitTimer -= Time.deltaTime;
        heatDisplay.value = heatTimer;
        if (heatTimer > 1)
        {
            byte red = (byte)Mathf.Clamp(Mathf.FloorToInt((255 / heatTimer) * 2), 0, 255);
            // heatImageDisplay.color = new Color32(red, 30, (byte) Mathf.FloorToInt(15 * heatTimer), 255);
        }
        if (!isNearCooler)
        {
            heatTimer -= Time.deltaTime * (isHotter ? 8 : 1);
        }
        else
        {
            heatTimer += Time.deltaTime * 50;
        }
        heatTimer = Mathf.Clamp(heatTimer, 0, maxHeat);
        if (heatTimer == 0 && !isRespawning)
        {
            Instantiate(deathParticle, transform.position, transform.rotation);
            pm.rb.velocity = new Vector2(0,0);
            PlayerMovement.hasWings = false;
            DeathScreen.SetActive(true);
            playerSprite.SetActive(false);
            pm.enabled = false;
            timerForRespawn = respawnTime;
            isRespawning = true;
           // heatDisplay.maxValue = maxHeat;
        }
        timerForRespawn -= Time.deltaTime;
        if (timerForRespawn <= 0.3 && isRespawning && !pm.enabled)
        {
            transform.position = new Vector3(lastCheckpoint.x, lastCheckpoint.y + 1f, lastCheckpoint.z);
            playerSprite.SetActive(true);
            isNearCooler = true;
            heatTimer = 10;
            for (int i = 0; i < pickups.Count; i++)
            {
                pickups[i].SetActive(true);
            }
                        pm.enabled = true;
        }
        if (timerForRespawn <= 0 && isRespawning)
        {
            DeathScreen.SetActive(false);
            isRespawning = false;
        }
        if(heatTimer > maxHeat / 1.25)
        {
            heatStates[0].SetActive(false);
             heatStates[1].SetActive(false);
              heatStates[2].SetActive(false);
        }
        if(heatTimer < maxHeat / 1.25)
        {
            heatStates[0].SetActive(true);
             heatStates[1].SetActive(false);
              heatStates[2].SetActive(false);
        }
        if(heatTimer < maxHeat / 2)
        {
             heatStates[0].SetActive(false);
             heatStates[1].SetActive(true);
              heatStates[2].SetActive(false);
        }
        if(heatTimer < maxHeat / 3)
        { heatStates[0].SetActive(false);
             heatStates[1].SetActive(false);
              heatStates[2].SetActive(true);

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == coolerTag)
        {
            isNearCooler = true;
            lastCheckpoint = other.transform.position;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == coolerTag)
        {
            isNearCooler = false;
        }
    }

  public void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "HotBoy")
        {
              isHotter = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "HotBoy")
        {
              isHotter = true;
        }
    }

    public static void AddTime(float _timeToAdd)
    {

            heatTimer += _timeToAdd;

        
    }
    public static void RemoveTime(float _timeToRemove)
    {
         if(hitTimer <= 0)
        {
            heatTimer -= _timeToRemove;
            hitTimer = 0.25f;
        } else if(_timeToRemove > 10)
        {
            heatTimer -= _timeToRemove;
        }
    }
}
