using UnityEngine;

public class PlayerWar : MonoBehaviour
{
    public float attackRange = 2f;
    public int attackDamage = 20;
    public LayerMask enemyLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  // Appuyer sur la touche espace pour attaquer
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        Debug.Log("Nombre d'ennemis détectés : " + hitEnemies.Length);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.gameObject.CompareTag("Enemy")) // Vérifie si l'objet détecté est un ennemi
            {
                MobBase mobBase = enemy.GetComponent<MobBase>();
                Boss boss = enemy.GetComponent<Boss>();

                if (mobBase != null)
                {
                    mobBase.TakeDamage(attackDamage);
                }
                else if (boss != null)
                {
                    boss.TakeDamage(attackDamage);
                }
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
