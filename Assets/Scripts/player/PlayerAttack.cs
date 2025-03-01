using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 20;
    public float attackRange = 1.5f;
    public LayerMask enemyLayer;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Espace pour attaquer
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
