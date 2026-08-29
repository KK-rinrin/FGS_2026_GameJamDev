using UnityEngine;

public class StartingScript : MonoBehaviour
{
    void Start()
    {
        
    }

    public void StartGameBTN()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("KaminagaScene");
    }

    public void QuitGameBTN()
    {
        Application.Quit();
    }
}
