using UnityEngine;

public class Pea : MonoBehaviour
{
    // 豆つぶが画面外に出たときの削除判定距離
    [SerializeField] private float maxDistance = 30f;

    // 初期位置
    private Vector2 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // 初期位置からの距離をチェック
        float distance = Vector2.Distance(transform.position, initialPosition);
        if (distance > maxDistance)
        {
            // 画面外に出たら削除
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 豆つぶが何かに衝突した時の処理
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 敵や障害物に当たった場合
        if (collision.CompareTag("Enemy"))  // タグを指定（例："Enemy", "Wall"）
        {
            // ダメージ処理など
            // 衝突したオブジェクトに処理を送る

            // 豆つぶを削除
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 豆つぶが他のコライダーと衝突した時
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 敵や障害物に当たった場合、豆つぶを削除
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// シーンビューに豆粒を表示するためのGizmo描画
    /// </summary>
    private void OnDrawGizmos()
    {
        // 黄色い球体を描画
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.25f);
        
        // 外枠も描画（より見やすく）
        Gizmos.color = new Color(1f, 0.8f, 0f);
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }
}
