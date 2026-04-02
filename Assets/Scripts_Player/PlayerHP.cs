using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerHP : MonoBehaviour
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

    private SpriteRenderer sr;

    //HPのテキスト
    [SerializeField] private TextMeshProUGUI hpText;

    //HPのバー
    [SerializeField] private Image hpBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = maxHP;
        UpdateUI();
        sr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damageValue)
    {
        if (hp <= 0 || isInvincible) return;

        hp -= damageValue;

        SEManager.Instance.SEDamage();

        UpdateUI();

        StartCoroutine(BecomeInvincible());

        Debug.Log(gameObject.name + "は" + damageValue + "のダメージを受けた。残りHP: " + hp);

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
        isInvincible = false;
    }

    private void UpdateUI()
    {
        if (hpText != null) 
        {
            hpText.text = hp.ToString();
        }

        if (hpBar != null)
        {
            hpBar.fillAmount = (float)hp / maxHP;
        }
    }
}
