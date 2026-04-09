using UnityEngine;

public class BOSS_TigerWallChecker : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemy == null) return;

        if (collision.CompareTag("Ground"))
        {
            enemy.localScale = new Vector2(-enemy.localScale.x, enemy.localScale.y);
        }
    }


}
