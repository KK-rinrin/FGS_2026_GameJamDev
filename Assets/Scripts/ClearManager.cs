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
            Initiate.Fade("ClearScene", Color.black, 1.0f);
        }
    }
}
