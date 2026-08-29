using UnityEngine;

public enum ItemType
{
    SpeedUp,
    SpeedDown,
}

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "CreateItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public float value;

    public void Apply(ItemComposer composer)
    {
        switch (type)
        {
            case ItemType.SpeedUp:
                // ここでプレイヤーのスピードアップ用関数を呼ぶなどをしていく
                //composer.player.
                Debug.Log("speed up");
                break;
            case ItemType.SpeedDown:
                //value = 1f;
                break;

        }
    }


}
