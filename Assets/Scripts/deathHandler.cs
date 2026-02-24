using System;
using System.Linq;
using UnityEngine;

public class deathHandler : MonoBehaviour
{

    [SerializeField]
    private PlayerDataBroadcast dataBroadcast;

    private int playerDeathCount = 0;

    [SerializeField]
    private string[] deathTags;
    [SerializeField]
    private string respawnTag,badRespawnTag;

    private Vector3 respawnPoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(deathTags.Contains(collision.gameObject.tag))
        {
            die(collision.gameObject.tag);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == respawnTag)
        {
            Debug.Log("Checkpoint");
            respawnPoint = other.transform.position;
        }
        else if(other.gameObject.tag == badRespawnTag)
        {
            die(other.gameObject.tag);
        }
    }

    private void die(string deathTag)
    {
        
        playerDeathCount++;
        Debug.Log("DIE: " + playerDeathCount + " : " + deathTag);
        dataBroadcast.PlayerDies(deathTag);
        transform.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        transform.position = respawnPoint;

    }

    
}
