using UnityEngine;
using System.Collections;

public class BOSS_TigerAttack : MonoBehaviour
{
    //攻撃オブジェクト
    [SerializeField] private Collider2D attackCollider;

    //攻撃時間
    [SerializeField] private float attackTime = 0.5f;

    //攻撃間隔
    [SerializeField] private float attackIntervaltime = 2.0f;

    [SerializeField] private Animator anim;

    //攻撃中はtrue
    private bool isAttacking = false;

    //プレイヤーの参照
    private Transform playerTransform;

    //突進攻撃を行う距離
    [SerializeField] private float chargeAttackDistance = 5.0f;

    //突進攻撃の速度
    [SerializeField] private float chargeSpeed = 10.0f;

    //突進攻撃の時間
    [SerializeField] private float chargeAttackTime = 30.0f;

    private Vector2 _defaultScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //攻撃オブジェクトをOFFにする
        attackCollider.enabled = false;

        _defaultScale = transform.localScale;
         //プレイヤーの参照を取得
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (playerTransform == null)
        {
            Debug.LogWarning("プレイヤーが見つかりません。タグ「Player」が設定されているか確認してください。");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //攻撃中でなければ
        if (isAttacking == false)
        {
            //プレイヤーとの距離を計算
            float distanceToPlayer = GetDistanceToPlayer();

        if( transform.position.x - playerTransform.position.x < 0)
        {
            transform.localScale = _defaultScale;
        } else
        {
            transform.localScale = new Vector2 (-_defaultScale.x, _defaultScale.y);
        }
            //距離が一定以上なら突進攻撃、そうでなければ通常攻撃
            if (distanceToPlayer >= chargeAttackDistance)
            {
                //突進攻撃開始
                StartCoroutine(ChargeAttack());
            }
            else
            {
                //通常攻撃開始
                StartCoroutine(Attack());
            }
        }   
    }

    private IEnumerator Attack()
    {
       //攻撃中はtrue
       isAttacking = true;

       //攻撃アニメーション開始
       anim.SetBool("Attack", true);

    　　//攻撃時間の待機
        yield return new WaitForSeconds(attackTime);
       
       //攻撃オブジェクトをONにする
       attackCollider.enabled = true;

       //攻撃間隔時間の待機
       yield return new WaitForSeconds(attackIntervaltime);

       //攻撃オブジェクトをOFFにする
      //attackCollider.enabled = false; 

       //攻撃アニメーション終了
       anim.SetBool("Attack", false);

       //攻撃中はfalse
       isAttacking=false;
    }

    /// <summary>
    /// プレイヤーとの距離を計算
    /// </summary>
    private float GetDistanceToPlayer()
    {
        if (playerTransform == null)
            return 0f;

        return Vector3.Distance(transform.position, playerTransform.position);
    }

    /// <summary>
    /// 突進攻撃（距離が一定以上の時）
    /// </summary>
    private IEnumerator ChargeAttack()
    {
       //攻撃中はtrue
       isAttacking = true;
        Debug.Log("突進攻撃開始");

        float _direction = Input.GetAxisRaw("Horizontal");
        //突進アニメーション開始
        anim.SetBool("Charge", true);

        Vector3 chargeDirection = (playerTransform.position - transform.position).normalized;
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

    　　//攻撃時間の待機
        yield return new WaitForSeconds(0.5f);
Debug.Log("突進攻撃中: ");
        while (elapsedTime < chargeAttackTime)
        {
            elapsedTime += Time.deltaTime;
            Vector3 newPosition = startPosition + chargeDirection * chargeSpeed * elapsedTime;
            transform.position = newPosition;

            //攻撃判定をONにする
            attackCollider.enabled = true;

            yield return null;
        }

        //攻撃判定をOFFにする
        //attackCollider.enabled = false;

        //突進アニメーション終了
        anim.SetBool("Charge", false);

        Debug.Log("突進攻撃終了");
       //攻撃中はfalse
       isAttacking=false;
    }
}
