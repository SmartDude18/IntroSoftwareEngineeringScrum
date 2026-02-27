using UnityEngine;

public class ProceduralLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Renderer>() != null)
        {
            other.gameObject.GetComponent<Renderer>().enabled = true;
        }
    }
}
