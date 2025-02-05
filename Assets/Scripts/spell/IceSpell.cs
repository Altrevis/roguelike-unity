using UnityEngine;

public class IceSpell : MonoBehaviour
{
    public Transform castPoint;   // Point d'origine du sort
    public LayerMask targetMask;  // Cibles touchées
    public float radius = 2f;     // Portée de l'AOE
    public int damage = 15;       // Dégâts du sort
    public float slowDuration = 3f; // Durée du ralentissement
    public float slowFactor = 0.5f; // Facteur de ralentissement (50%)

    public ParticleSystem iceEffect; // Effet visuel de gel

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Clic droit pour lancer le sort
        {
            CastIce();
        }
    }

    void CastIce()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(castPoint.position, radius, targetMask);

        foreach (Collider enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        if (iceEffect != null)
        {
            iceEffect.Play();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(castPoint.position, radius);
    }
}
