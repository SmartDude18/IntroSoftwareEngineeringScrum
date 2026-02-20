using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject WinText;
    [Range(0,100)]
    [SerializeField] private float distanceValue;
    [SerializeField] private Transform endPos;

    public Vector3 spawnPoint { get; private set; }
    private Vector3 endPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoint = Player.transform.position;
        WinText.transform.position = new Vector3(WinText.transform.position.x + updatePos().x - distanceValue, WinText.transform.position.y, WinText.transform.position.z);
        endPoint = new Vector3(endPos.position.x, endPos.position.y + (distanceValue / 4), WinText.transform.position.z);
    }

    // Update is called once per frame

    

    void Update()
    {
        if (!(WinText.transform.position.x < (WinText.transform.position.x + updatePos().x - distanceValue)) && WinText.transform.position.x > endPos.position.x)
        {
            WinText.transform.position = new Vector3(WinText.transform.position.x + updatePos().x - distanceValue, WinText.transform.position.y + updatePos().y + (distanceValue / 4), WinText.transform.position.z);
        }
        else if (WinText.transform.position.x < endPos.position.x)
        {
            WinText.transform.position = endPoint;
        }
    }

    Vector3 updatePos() { return Player.transform.position - WinText.transform.position; } 
                  
    

    public void UpdateSpawnpoint(bool isRestart)
    {
        if (!isRestart)
        {    
            spawnPoint = Player.transform.position;
        }else
        {
            Player.transform.position = spawnPoint;
        }
    }

    public void ProceduralLevel(Collision collision)
    {
        collision.gameObject.SetActive(true);
    }




}
