using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // アイテムはプレイヤー以外と処理をしない
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        Player getPlayer = collision.GetComponent<Player>();

        if (getPlayer == null)
        {
            Debug.LogWarning("アイテムがプレイヤーを取得できませんでした");
            return;
        }

        // もしカメラ関連の処理を行う場合はシーンからFindObjectWithTypeしないといけないかも
        CameraController getCameraController = FindFirstObjectByType<CameraController>();

        if (getCameraController == null)
        {
            Debug.LogWarning("アイテムがカメラコントローラーを取得できませんでした");
            return;
        }

        ItemComposer composer = new ItemComposer { player = getPlayer, cameraController = getCameraController };

        itemData.Apply(composer);

        Destroy(gameObject);
    }


}
