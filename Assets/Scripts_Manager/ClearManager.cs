using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearManager : MonoBehaviour
{
    [SerializeField] private GameObject _boss;
    [SerializeField] private string _clearScene;

    [SerializeField] private GameObject _bgmBoss;
    [SerializeField] private GameObject _bgmClear;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _bgmBoss.SetActive(true);
        _bgmClear.SetActive(false);      
    }

    // Update is called once per frame
    void Update()
    {
        //ボスを倒したら
        if (_boss == null)
        {
            //BGMの切り替え
            _bgmBoss.SetActive(false);
            _bgmClear.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //もしボスがいない　かつ　Playerがふれたら
        if (_boss == null && collision.gameObject.CompareTag("Player"))
        {
            //シーン移動する
            SceneManager.LoadScene(_clearScene);
        }
    }
}
