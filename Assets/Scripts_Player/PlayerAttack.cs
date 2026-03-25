using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    // 射撃するビーム/豆つぶのプリフェブ
    [SerializeField] private GameObject peaPrefab;

    // ビーム/豆つぶの発射速度
    [SerializeField] private float peaSpeed = 15.0f;

    // 発射位置のオフセット
    [SerializeField] private Vector2 fireOffset = new Vector2(0.5f, 0f);

    // 攻撃中かどうか
    private bool isAttacking = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 豆つぶ攻撃を発射する
    /// </summary>
    /// <param name="spawnPosition">発射位置</param>
    /// <param name="direction">発射方向（1 = 右、-1 = 左）</param>
    public void FirePea(Vector2 spawnPosition, int direction)
    {
        // スポーン位置を計算
        Vector2 adjustedSpawnPosition = spawnPosition + (fireOffset * direction);

        GameObject pea;

        // プリフェブが設定されているなら使用、なければデフォルト豆粒を作成
        if (peaPrefab != null)
        {
            pea = Instantiate(peaPrefab, adjustedSpawnPosition, Quaternion.identity);
        }
        else
        {
            // デフォルトの豆粒を動的に作成
            pea = new GameObject("Pea");
            pea.transform.position = adjustedSpawnPosition;

            // スプライトレンダラーを追加（黄色い小さな円）
            SpriteRenderer spriteRenderer = pea.AddComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(1f, 0.9f, 0f); // 黄色

            // スケールを適切に設定（見やすいが大きすぎない）
            pea.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

            // Rigidbody2D を追加
            Rigidbody2D peaRb = pea.AddComponent<Rigidbody2D>();
            peaRb.gravityScale = 0; // 重力を無視
            peaRb.bodyType = RigidbodyType2D.Kinematic; // Kinematic にして他のオブジェクトに影響されない
            peaRb.linearVelocity = new Vector2(peaSpeed * direction, 0);

            // Collider をトリガーとして設定（これによってプレイヤーに影響しない）
            CircleCollider2D collider = pea.AddComponent<CircleCollider2D>();
            collider.radius = 0.25f;
            collider.isTrigger = true; // トリガーに設定

            // Pea スクリプトを添付
            pea.AddComponent<Pea>();

            Debug.Log($"✅ Default Pea created! Position: {adjustedSpawnPosition}, Direction: {direction}, Scale: 0.5");
        }

        // Rigidbody2D を取得して速度を設定
        Rigidbody2D peaRb2D = pea.GetComponent<Rigidbody2D>();
        if (peaRb2D != null && peaPrefab != null)
        {
            peaRb2D.linearVelocity = new Vector2(peaSpeed * direction, 0);
            Debug.Log($"✅ Pea fired! Position: {adjustedSpawnPosition}, Direction: {direction}, Speed: {peaSpeed}");
        }

        // スプライトの向きを反映（必要に応じて）
        if (direction == -1)
        {
            pea.transform.localScale = new Vector2(-pea.transform.localScale.x, pea.transform.localScale.y);
        }
    }

    /// <summary>
    /// 接続状態を確認するメッセージ
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        // 発射位置を表示（左右両方）
        // 右向き時の発射位置
        Gizmos.color = new Color(0, 1f, 0, 0.5f); // 緑
        Gizmos.DrawWireSphere(transform.position + (Vector3)fireOffset, 0.3f);
        
        // 左向き時の発射位置
        Gizmos.color = new Color(1f, 0, 0, 0.5f); // 赤
        Gizmos.DrawWireSphere(transform.position - (Vector3)fireOffset, 0.3f);
    }

    /// <summary>
    /// Gizmo表示（常に）
    /// </summary>
    private void OnDrawGizmos()
    {
        // 発射位置を常に表示（小さい円）
        Gizmos.color = new Color(1, 1, 0, 0.3f); // 黄半透明
        Gizmos.DrawWireSphere(transform.position + (Vector3)fireOffset, 0.2f);
    }

    //// 攻撃する処理メソッド
    //private void IEnumerator Attack()
    //{
    //    // 攻撃中であるためtrue設定
    //    isAttacking = true;

    //    // 攻撃用のオブジェクトの割り当て判定ONにする
    //    attackCollider.enabled = true;

    //    // 指定時間待機
    //    yield return new WaitForSeconds(attackTime);

    //    // 攻撃用のオブジェクトの割り当て判定OFFにする
    //    attackCollider.enabled = false; 

    //    // 攻撃完了後、フラッグをfalseに設定
    //    isAttacking=false;

    //}
}
