using UnityEngine;

public class PlayerHpScript : MonoBehaviour
{
    [SerializeField] private int full_hp = 10;
    [SerializeField] private static int player_hp = 0;

    void Start()
    {
        player_hp = full_hp;
    }
    
}
