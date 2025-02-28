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
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

    Debug.Log("Nombre d'ennemis détectés : " + hitEnemies.Length);

    foreach (Collider2D enemy in hitEnemies)
    {
        Debug.Log("Ennemi touché : " + enemy.gameObject.name);
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth != null && !enemyHealth.isDead)
        {
            int damageDealt = enemyHealth.TakeDamage(attackDamage);
            Debug.Log("Dégâts infligés à : " + enemy.gameObject.name + " (" + damageDealt + " dégâts)");
            Debug.Log("Santé actuelle de l'ennemi : " + enemyHealth.currentHealth);
        }
        else
        {
            Debug.LogWarning(enemy.gameObject.name + " n'a pas de script EnemyHealth !");
        }
    }
}


    // Juste pour voir la zone d'attaque dans l'éditeur
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
