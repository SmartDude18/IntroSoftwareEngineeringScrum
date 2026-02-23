using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject WinText;
    [SerializeField] private GameObject[] CheckPoints;
    [SerializeField] private Material[] Materials;

    [Range(0,100)]
    [SerializeField] private float distanceValue;
    [SerializeField] private Transform endPos;

    public Vector3 spawnPoint { get; private set; }
    private Vector3 endPoint;
    private Vector3 restartPoint;



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
                CheckPoints[i].gameObject.transform.GetChild(0).GetComponent<Renderer>().material = Materials[0];

            }
            else 
            {
                CheckPoints[i].gameObject.tag = "Restart";
                CheckPoints[i].gameObject.transform.GetChild(0).GetComponent<Renderer>().material = Materials[1];
            }
           
        }

        spawnPoint = Player.transform.position;
        restartPoint = spawnPoint;
        WinText.transform.position = new Vector3(WinText.transform.position.x + UpdatePos().x - distanceValue, WinText.transform.position.y, WinText.transform.position.z);
        endPoint = new Vector3(endPos.position.x, endPos.position.y + (distanceValue / 4), WinText.transform.position.z);
    }

    // Update is called once per frame

    

    void Update()
    {
        if (!(WinText.transform.position.x < (WinText.transform.position.x + UpdatePos().x - distanceValue)) && WinText.transform.position.x > endPos.position.x)
        {
            WinText.transform.position = new Vector3(WinText.transform.position.x + UpdatePos().x - distanceValue, WinText.transform.position.y + UpdatePos().y + (distanceValue / 4), WinText.transform.position.z);
        }
        else if (WinText.transform.position.x < endPos.position.x)
        {
            WinText.transform.position = endPoint;
        }
    }

    Vector3 UpdatePos() { return Player.transform.position - WinText.transform.position; } 

    public void UpdateSpawnpoint(bool isRestart)
    {
        if (!isRestart)
        {    
            spawnPoint = Player.transform.position;
        }else
        {
            Player.transform.position = restartPoint;
        }
    }

    public void UpdateInvisibleLevel()
    {

    }

   



}
