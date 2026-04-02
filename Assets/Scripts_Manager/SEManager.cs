using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField] private AudioSource sourceDamage;
    [SerializeField] private AudioSource sourcePlayerAttack;

    public static SEManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SEDamage()
    {
        sourceDamage.Play();
    }

    public void SEPlayerAttack()
    {
        sourcePlayerAttack.Play();
    }

}
