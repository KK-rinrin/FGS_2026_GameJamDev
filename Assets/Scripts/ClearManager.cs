using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearManager : MonoBehaviour
{

    [SerializeField] private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // アイテムはプレイヤー以外と処理をしない
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        player.SetGoal();
        EndingImageScript.is_success = true;
        //Initiate.Fade("EndScene", Color.black, 1.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
    }
}
