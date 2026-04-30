using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    //攻撃力
    [SerializeField] private int attackPower = 1;

    //接触した瞬間を検知する
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //敵またはBOSSに接触したら
        if (collision.gameObject.CompareTag("Enemy")) //|| collision.gameObject.CompareTag("Boss"))
        {
            //EnemyHPまたはBOSS_TigerHPスクリプトを取得してダメージを与える
            EnemyHP enemyHP = collision.gameObject.GetComponent<EnemyHP>();
            if (enemyHP != null)
            {
                enemyHP.TakeDamage(attackPower);
            }
            else
            {
                BOSS_TigerHP bossHP = collision.gameObject.GetComponent<BOSS_TigerHP>();
                if (bossHP != null)
                {
                    bossHP.TakeDamage(attackPower);
                }
            }
        }
    }

}
