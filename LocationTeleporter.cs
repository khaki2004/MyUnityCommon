using UnityEngine;

/// <summary>
/// ボタンから呼び出されて、プレイヤーを指定座標へジャンプさせるスクリプト
/// </summary>
public class LocationTeleporter : MonoBehaviour
{
    [SerializeField] private Transform player;  // プレイヤーのTransformをアサイン

    // 位置データ（自由に追加可能）
    private Vector3 townCenterPos = new Vector3(0f, 0f, 0f);         // 町の中央
    private Vector3 libraryPos = new Vector3(500f, 0f, 500f);        // 中央図書館

    /// <summary>
    /// 町の中央へジャンプ
    /// </summary>
    public void TeleportToTownCenter()
    {
        if (player != null)
        {
            player.position = townCenterPos;
        }
    }

    /// <summary>
    /// 中央図書館へジャンプ
    /// </summary>
    public void TeleportToLibrary()
    {
        if (player != null)
        {
            player.position = libraryPos;
        }
    }

    // 新しいジャンプ先を追加する場合は、ここに関数を追加
    // 例：
    // public void TeleportToMarket() {
    //     player.position = new Vector3(100f, 0f, 200f);
    // }
}
