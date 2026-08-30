using UnityEngine;

public class ClearManager : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerTransform.position.x >= this.transform.position.x)
        {
            NewMonoBehaviourScript.is_success = true;
            Initiate.Fade("EndScene", Color.black, 1.0f);
        }
    }
}
