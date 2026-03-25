using UnityEngine;

public class Player : MonoBehaviour
{
    // 移動スピード
    [SerializeField] private float moveSpeed = 10.0f;

    // ジャンプ力
    [SerializeField] private float jumpForce = 15.0f;

    // プレイヤーの足場におけるヒットオブジェクト
    [SerializeField] private Transform groundChecker;
    // 地面をチェックする円の半径
    [SerializeField] private float checkerRadius = 0.1f;
    // 地面のレイヤー
    [SerializeField] private LayerMask groundLayer;

    // 地面に着地しているとtrue、していないとfalse
    private bool isGround;

    private Rigidbody2D rb;

    private Vector2 defaultScale;

    // 攻撃スクリプトの参照
    private PlayerAttack playerAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        defaultScale = transform.localScale;

        // PlayerAttackコンポーネントの参照を取得
        playerAttack = GetComponent<PlayerAttack>();
        if (playerAttack == null)
        {
            Debug.LogError("⚠️ PlayerAttack component is not found on this GameObject!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Jump();
        Attack();
    }

    /// 歩行するメソッド
    private void Walk()
    {
        // 入力状態を調べる(右なら1/左なら-1/何もないと0)
        float direction = Input.GetAxisRaw("Horizontal");

        // Rigidbody2Dの速度ベクトルで左右に移動
        rb.linearVelocityX = direction * moveSpeed;

        // 右の場合
        if (direction > 0)
        {
            transform.localScale = defaultScale;
        }
        // 左の場合
        if (direction < 0)
        {
            transform.localScale = new Vector2 (-defaultScale.x, defaultScale.y);
        }

    }

    /// ジャンプするメソッド
    private void Jump()
    {
        // OverlapCircle(円の中心, 円の半径, 検出するレイヤー);
        // 以下のチェッカーが地面に接触しているか判定
        if (Physics2D.OverlapCircle(groundChecker.position, checkerRadius, groundLayer))
        {
            isGround = true;
        }
        // 接触していないと
        else
        {
            isGround = false;
        }

        // Wキーまたはスペースキーが押されたタイミングでジャンプ
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGround == true)
        {
            // 上方向に力を加える
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    /// <summary>
    /// 攻撃メソッド
    /// </summary>
    private void Attack()
    {
        // Zキーで攻撃
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // playerAttack が null でないか確認
            if (playerAttack == null)
            {
                Debug.LogError("❌ playerAttack is null! Cannot fire pea.");
                return;
            }

            // 現在の向きを取得（スケールから判定）
            int direction = transform.localScale.x > 0 ? 1 : -1;
            Debug.Log($"🎮 Attack key pressed! Direction: {(direction == 1 ? "RIGHT →" : "LEFT ←")}");
            // PlayerAttackの射撃メソッドを呼び出し
            playerAttack.FirePea(transform.position, direction);
        }
    }

    ///// 衝突判定
    ///// <param name = "collision"></param> 
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     // 衝突した オブジェクトからタグを取得する
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         isGround = true;
    //     }
    // }

    ///// 衝突判定
    ///// <param name = "collision"></param> 
    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     // 衝突した オブジェクトからタグを取得する
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         isGround = false;
    //     }

    // }
}
