using UnityEngine;

public class BOSS_TigerCliffChecker : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemy == null) return;

        if (collision.CompareTag("Ground"))
        {
            enemy.localScale = new Vector2(-enemy.localScale.x, enemy.localScale.y);
        }
    }


}
