using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //攻撃オブジェクトをOFFにする
        attackCollider.enabled = false;    
    }

    // Update is called once per frame
    void Update()
    {
        //攻撃中でなければ
        if (isAttacking == false)
        {
            //攻撃開始
            StartCoroutine(Attack());
        }   
    }

    private IEnumerator Attack()
    {
       //攻撃中はtrue
       isAttacking = true;

       //攻撃オブジェクトをONにする
       attackCollider.enabled = true;

       //攻撃アニメーション開始
       anim.SetBool("Attack", true);

       //攻撃時間
       yield return new WaitForSeconds(attackTime);

       //攻撃アニメーション終了
       anim.SetBool("Attack", false);

       //攻撃オブジェクトをOFFにする
       attackCollider.enabled = false; 

       //攻撃間隔時間の待機
       yield return new WaitForSeconds(attackIntervaltime);

       //攻撃中はfalse
       isAttacking=false;
    }


}
