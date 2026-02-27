using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject WinText;
    [SerializeField] private GameObject[] CheckPoints;
    [SerializeField] private Material[] CheckPointMaterials;

    [Range(0,100)]
    [SerializeField] private float distanceValue;
    [SerializeField] private Transform endPos;
    [SerializeField] private Transform WinTransform;

    public Vector3 spawnPoint { get; private set; }
    private Vector3 endPoint;
    private Vector3 restartPoint;

    private List<GameObject> invisiblePlatforms = new List<GameObject>();
    private List<GameObject> activeObjects = new List<GameObject>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < CheckPoints.Length; i++)
        {
            int randTag = Random.Range(0, 4);

            Debug.Log(randTag);
            if (randTag > 0) 
            { 
                CheckPoints[i].gameObject.tag = "Checkpoint";
                CheckPoints[i].gameObject.transform.GetChild(0).GetComponent<Renderer>().material = CheckPointMaterials[0];

            }
            else 
            {
                CheckPoints[i].gameObject.tag = "Restart";
                CheckPoints[i].gameObject.transform.GetChild(0).GetComponent<Renderer>().material = CheckPointMaterials[1];
            }
        }

        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        Debug.Log(platforms.Length);
        for (int i = 0;i < platforms.Length;i++)
        {
            int randPlat = Random.Range(0, platforms.Length);
            if (randPlat <= platforms.Length / 3) { invisiblePlatforms.Add(platforms[i]); }
        }

        spawnPoint = Player.transform.position;
        restartPoint = spawnPoint;
        UpdateWinSign();
        endPoint = new Vector3(endPos.position.x, endPos.position.y + (distanceValue / 4), WinText.transform.position.z);
    }

    // Update is called once per frame

    

    void Update()
    {
        if (!(WinText.transform.position.x < (WinText.transform.position.x + UpdatePos().x - distanceValue)) && WinText.transform.position.x > endPos.position.x)
        {
            UpdateWinSign();
        }
        else if (WinText.transform.position.x < endPos.position.x)
        {
            WinText.transform.position = endPoint;
        }
        else
        {
            WinText.transform.position = new Vector3(WinText.transform.position.x, WinText.transform.position.y, WinText.transform.position.z + (UpdatePos().z - 4));
        }
        
    }

    Vector3 UpdatePos() { return Player.transform.position - WinText.transform.position; } 


    public void UpdateWinSign()
    {
        WinText.transform.position = new Vector3(WinText.transform.position.x + UpdatePos().x - distanceValue, WinText.transform.position.y + UpdatePos().y + (distanceValue / 5), WinText.transform.position.z + (UpdatePos().z - 4));
    }

    public void UpdateSpawnpoint(bool isRestart)
    {
        if (!isRestart)
        {    
            spawnPoint = Player.transform.position;
        }else
        {
            spawnPoint = restartPoint;
        }
    }

    public void UpdateInvisibleLevel(bool visible)
    {

        for (int i = 0; i < invisiblePlatforms.Count; i++)
        {
            if (invisiblePlatforms[i].gameObject.GetComponent<Renderer>().enabled == true && !activeObjects.Contains(invisiblePlatforms[i]))
            {
                activeObjects.Add(invisiblePlatforms[i].gameObject);
            }
        }

        if (visible)
        {
            Debug.Log("Active true: " + activeObjects.Count);
            for (int i = 0; i < activeObjects.Count; i++)
            {
                activeObjects[i].GetComponent<Renderer>().enabled = true;
            }
        }
        else
        {
            Debug.Log("Active false: " + activeObjects.Count);

            for (int i = 0; i < activeObjects.Count; i++)
            {
                activeObjects[i].GetComponent<Renderer>().enabled = false;
            }
        }

    }





}
