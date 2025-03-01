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
            MobBase mobBase = enemy.GetComponent<MobBase>();
                Boss boss = enemy.GetComponent<Boss>();

                if (mobBase != null)
                {
                    Debug.Log("Xmob prend des dégât");
                    mobBase.TakeDamage(attackDamage);
                }
                else if (boss != null)
                {
                    Debug.Log("Yboss prend des dégât");
                    boss.TakeDamage(attackDamage);
                }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
