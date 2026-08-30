using UnityEngine;

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
        if(player.transform.position.x >= this.transform.position.x)
        {
            player.SetGoal();
            EndingImageScript.is_success = true;
            Initiate.Fade("EndScene", Color.black, 1.0f);
        }
    }
}
