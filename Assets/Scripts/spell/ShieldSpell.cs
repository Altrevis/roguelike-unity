using UnityEngine;

public class ShieldSpell : MonoBehaviour
{
    public Transform castPoint;         // Point d'origine du sort
    public LayerMask targetMask;        // Cibles affectées par le sort
    public float radius = 3f;           // Portée de l'effet
    public int damage = 20;             // Dégâts infligés
    public float knockbackForce = 5f;   // Force de repoussement
    public ParticleSystem shieldEffect; // Effet visuel

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Touche 'Q' pour activer le bouclier
        {
            ActivateShield();
        }
    }

    void ActivateShield()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(castPoint.position, radius, targetMask);

        foreach (Collider enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Rigidbody rb = enemy.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = enemy.transform.position - castPoint.position;
                direction.y = 0; // Éviter de faire sauter l'ennemi
                rb.AddForce(direction.normalized * knockbackForce, ForceMode.Impulse);
            }
        }

        if (shieldEffect != null)
        {
            shieldEffect.Play();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(castPoint.position, radius);
    }
}
