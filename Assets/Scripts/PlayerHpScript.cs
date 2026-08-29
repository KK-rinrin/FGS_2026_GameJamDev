using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerHpScript : MonoBehaviour
{
    [SerializeField] private static int player_hp = 0;
    [SerializeField] private List<GameObject> heart_list;
    private int total_hp;

    void Start()
    {
        total_hp = heart_list.Count;
        player_hp = total_hp;
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

    private int HpMinus(int damage)
    {
        if (damage > total_hp || damage < 1){
            return -1;
        }
        for (int i = 0 ; i < damage ; i++){
            if (player_hp - 1 < 0){
                break;
            }
            heart_list[player_hp - 1].GetComponent<UnityEngine.UI.Image>().enabled = false;
            player_hp--;
        }
        if (player_hp <= 0){
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
        }
        return player_hp;
    }

}
