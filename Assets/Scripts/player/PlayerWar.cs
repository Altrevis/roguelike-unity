using UnityEngine;

public class PlayerWar : MonoBehaviour
{
    public float attackRange = 2f;
    public int attackDamage = 20;
    public LayerMask enemyLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Clique gauche pour attaquer
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage);
        }
    }
}
