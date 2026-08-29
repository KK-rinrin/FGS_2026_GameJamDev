using UnityEngine;
using UnityEditor;

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
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void RetryGameBTN()
    {
        Initiate.Fade("TitleScene", Color.black, 1.0f);
    }
}
