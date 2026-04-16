using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // �U���I�u�W�F�N�g
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private Collider2D downAttackCollider;

    // �U������
    [SerializeField] private float attackTime = 0.5f;
    [SerializeField] private float downAttackTime = 0.5f;

    [SerializeField] private float downSpeed = 10.0f;
    [SerializeField] private float attackForwardDistance = 0.4f;
    [SerializeField] private float attackForwardTime = 0.12f;

    // �U������true
    private bool isAttacking = false;

    private Animator anim;

    private Rigidbody2D rb;

    void Start()
    {
        attackCollider.enabled = false;
        downAttackCollider.enabled = false;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //左クリックを押すかつ
        if (Input.GetMouseButtonDown(0) && isAttacking  == false)
        {
            if (Input.GetKey(KeyCode.S))
            {
                StartCoroutine(DownAttack());
            }
            else
            {                      
                //攻撃開始
                StartCoroutine(Attack());
            }
        }
    }

    // �U�����郁�\�b�h
    private IEnumerator Attack()
    {
       //�U�����ł��邽��true�ɂ���
       isAttacking = true;

       //�U���p�̃I�u�W�F�N�g�̓����蔻���ON�ɂ���B
       attackCollider.enabled = true;

       //攻撃アニメーション開始
       anim.SetBool("Attack", true);
       
       //攻撃SE再生
       SEManager.Instance.SEPlayerAttack();

       //�w��ҋ@����
       float elapsedTime = 0.0f;
       float moveTime = Mathf.Min(attackForwardTime, attackTime);
       float facingDirection = transform.localScale.x >= 0.0f ? 2.5f : -2.5f;

       while (elapsedTime < attackTime)
       {
           if (elapsedTime < moveTime && moveTime > 0.0f)
           {
               float moveDeltaTime = Mathf.Min(Time.fixedDeltaTime, moveTime - elapsedTime);
               float moveDistance = attackForwardDistance * moveDeltaTime / moveTime;
               rb.MovePosition(rb.position + Vector2.right * facingDirection * moveDistance);
           }

           elapsedTime += Time.fixedDeltaTime;
           yield return new WaitForFixedUpdate();
       }

       anim.SetBool("Attack", false);

       //�U���p�̃I�u�W�F�N�g�̓����蔻���OFF�ɂ���
       attackCollider.enabled = false; 

       //�U���I���������false�ɂ���
       isAttacking=false;
    }

    private IEnumerator DownAttack()
    {
       isAttacking = true;

       downAttackCollider.enabled = true;
       anim.SetBool("DownAttack", true);

       rb.AddForce(Vector2.down * downSpeed, ForceMode2D.Impulse);
       
       //攻撃SE再生
       SEManager.Instance.SEPlayerAttack();

       yield return new WaitForSeconds(downAttackTime);

       anim.SetBool("DownAttack", false);
       downAttackCollider.enabled = false;

       isAttacking=false;
    }
}
