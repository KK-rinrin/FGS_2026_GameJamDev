using UnityEngine;

public enum ItemType
{
    SpeedUp,
    CameraInverse,
    FreePlayerInverse
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
                composer.player.AddSpeed(value);
                break;
            case ItemType.CameraInverse:
                composer.cameraController.Inverse();
                break;
            case ItemType.FreePlayerInverse:
                composer.player.SetFreeInverse(value);
                break;

        }
    }


}
