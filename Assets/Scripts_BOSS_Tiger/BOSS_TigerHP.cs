using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BOSS_TigerHP : MonoBehaviour
{   
    //HP
    private int hp;

    //最大HP
    [SerializeField] private int maxHP = 5;

    //無敵時間
    [SerializeField] private float invincibleTime = 0.5f;

    //点滅時間
    [SerializeField] private float blinkingTime = 0.1f;

    //無敵中にtrue
    private bool isInvincible = false;

    [SerializeField] private SpriteRenderer sr;

    //HPのバー
    [SerializeField] private Image hpBar;

    private void Start()
    {
        hp = maxHP;
        UpdateUI();
    }

     public void TakeDamage(int damageValue)
    {
        if (hp <= 0 || isInvincible) return;

        hp -= damageValue;

        UpdateUI();

        SEManager.Instance.SEDamage();

        StartCoroutine(BecomeInvincible());

        Debug.Log(gameObject.name + "に" + damageValue + "のダメージを与えた。残りHP: " + hp);

        if (hp <= 0)
        {
            hp = 0;

            UpdateUI();

            Destroy(gameObject);
        }
    }

    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;

        float timer = 0;

        while (timer < invincibleTime)
        {
            //点滅処理
            sr.enabled = !sr.enabled;
            
            //経過時間を加算
            timer += blinkingTime;

            //点滅時間分処理を停止してからループ再開
            yield return new WaitForSeconds(blinkingTime);   
        }

        //点滅を元に戻す
        sr.enabled = true;

        //無敵状態を解除
        isInvincible = false;
    }

    private void UpdateUI()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = (float)hp / maxHP;
        }
    }
}

