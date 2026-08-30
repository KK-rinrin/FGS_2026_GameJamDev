using System.Collections.Generic;
using UnityEngine;

public class PlayerHpScript : MonoBehaviour
{
    [SerializeField] private int player_hp = 5;
    [SerializeField] private List<GameObject> heart_list;
    private int total_hp = 5;

    void Awake()
    {
        if (heart_list != null && heart_list.Count > 0)
        {
            total_hp = heart_list.Count;
            player_hp = Mathf.Clamp(player_hp, 0, total_hp);
        }
    }

    void Start()
    {
    }

    void Update()
    {
        #if DEBUG
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            HpMinus(1);
            Debug.Log(player_hp);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)){
            HpMinus(2);
            Debug.Log(player_hp);
        }
        #endif
    }

    public int HpMinus(int damage)
    {
        if (heart_list == null || heart_list.Count == 0)
        {
            Debug.LogError("heart_list is null or empty");
            return -1;
        }

        total_hp = heart_list.Count;

        if (damage > total_hp || damage <= 0){
            return -1;
        }
        for (int i = 0 ; i < damage ; i++){
            if (player_hp - 1 < 0){
                break;
            }

            if (player_hp - 1 >= heart_list.Count || heart_list[player_hp - 1] == null)
            {
                Debug.LogError("heart_list count insufficient");
                return -1;
            }

            heart_list[player_hp - 1].GetComponent<UnityEngine.UI.Image>().enabled = false;
            player_hp--;
        }
        if (player_hp <= 0){
            EndingImageScript.is_success = false;
            Initiate.Fade("EndScene", Color.black, 1.0f);
            //UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
        }
        return player_hp;
    }

}
