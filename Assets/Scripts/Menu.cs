using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] AudioSource menuMusic;
    private void Start()
    {
        if (!menuMusic.isPlaying)
        {
            menuMusic.Play();
        }
    }
    public void StartGame()
    {
        menuMusic.Stop();
        SceneManager.LoadScene("Sound Testing");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
