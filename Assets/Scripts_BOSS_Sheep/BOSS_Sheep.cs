using UnityEngine;

public class BOSS_Sheep : MonoBehaviour
{
    //移動スピード
    [SerializeField] private float moveSpeed = 5.0f;

    private Rigidbody2D rb;

    //現在のスケール
    [SerializeField] private Transform currentScale;

    //プレイヤーへのダメージ値
    [SerializeField] private int damageValue = 1;

    //プレイヤーのHP管理スクリプト
    private PlayerHP playerHP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHP = FindObjectOfType<PlayerHP>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }
    private void Walk()
    {
        Debug.Log("Walk");
        if (currentScale.localScale.x > 0)
        {
            //左方向
            rb.linearVelocityX = -moveSpeed;
        }
        else if (currentScale.localScale.x < 0)
        {
            //右方向
            rb.linearVelocityX = moveSpeed;
        }
    }


    //プレイヤーとの衝突検出
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //プレイヤーとの衝突判定
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerHP != null)
            {
                playerHP.TakeDamage(damageValue);
            }
        }
    }

    //トリガーコライダーの場合はこちらを使用
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーとの衝突判定
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerHP != null)
            {
                playerHP.TakeDamage(damageValue);
            }
        }
    }
}