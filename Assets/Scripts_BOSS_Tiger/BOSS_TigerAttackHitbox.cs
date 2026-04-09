using UnityEngine;

public class BOSS_TigerAttackHitbox : MonoBehaviour
{
    //攻撃力
    [SerializeField] private int attackPower = 1;

    //接触した瞬間を検知する
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //敵に接触したら
        if (collision.gameObject.CompareTag("Player"))
        {
            //PlayerHPスクリプトを取得してダメージを与える
            PlayerHP playerHP = collision.gameObject.GetComponent<PlayerHP>();
            if (playerHP != null)
            {
                playerHP.TakeDamage(attackPower);
            }
        }
    }
}
