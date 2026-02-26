using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

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

    private float furthestDistance = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        furthestDistance = Player.transform.position.x - distanceValue;
        WinText.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + (distanceValue / 4), Player.transform.position.z - 4);
        
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

        spawnPoint = Player.transform.position;
        restartPoint = spawnPoint;
        //UpdateWinSign();
        endPoint = new Vector3(endPos.position.x, endPos.position.y + (distanceValue / 4), WinText.transform.position.z);
    }

    // Update is called once per frame

    

    void Update()
    {

        float shiftX = Player.transform.position.x - distanceValue;
        float clampedX = Mathf.Clamp(shiftX, endPoint.x, furthestDistance);
        furthestDistance = clampedX;
        WinText.transform.position = new Vector3(clampedX, WinText.transform.position.y, WinText.transform.position.z);
        
        /*
        var currentTransform = new Vector3();
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
        */
        
    }

    //Vector3 UpdatePos() { return Player.transform.position - WinText.transform.position; } 


    public void UpdateWinSign()
    {
        float shiftX = Player.transform.position.x - distanceValue;
        furthestDistance = shiftX;
        WinText.transform.position = new Vector3(shiftX, WinText.transform.position.y, WinText.transform.position.z);
        //WinText.transform.position = new Vector3(WinText.transform.position.x + UpdatePos().x - distanceValue, WinText.transform.position.y + UpdatePos().y + (distanceValue / 5), WinText.transform.position.z + (UpdatePos().z - 4));
    }

    public void UpdateSpawnpoint(bool isRestart)
    {
        if (!isRestart)
        {    
            spawnPoint = Player.transform.position;
        }else
        {
            Player.transform.position = restartPoint;
            spawnPoint = Player.transform.position;
            UpdateWinSign();
        }
    }

    public void UpdateInvisibleLevel()
    {

    }

   



}
