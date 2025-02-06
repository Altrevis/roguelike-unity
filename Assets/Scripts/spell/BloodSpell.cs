using UnityEngine;
using System.Collections;

public class BloodSpell : MonoBehaviour
{
    public Transform castPoint;       // Point de lancement du sort
    public float range = 3f;          // Portée du sort
    public int directDamage = 30;     // Dégâts bruts immédiats
    public int dotDamage = 15;        // Dégâts du DOT par seconde
    public float dotDuration = 3f;    // Durée du DOT en secondes
    public LayerMask targetMask;      // Cibles touchées
    public ParticleSystem effectPrefab; // Effet visuel du sort

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Touche E pour lancer le sort
        {
            CastSpell();
        }
    }

    void CastSpell()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(castPoint.position, range, targetMask);

        foreach (Collider enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(directDamage); // Dégâts bruts immédiats
                StartCoroutine(ApplyDot(enemyHealth, dotDuration)); // Appliquer le DOT
            }

            if (effectPrefab != null)
            {
                Instantiate(effectPrefab, enemy.transform.position, Quaternion.identity);
            }
        }

        Debug.Log("Sort lancé avec dégâts bruts + DOT !");
    }

    IEnumerator ApplyDot(EnemyHealth enemyHealth, float duration)
    {
        float timePassed = 0;
        while (timePassed < duration)
        {
            enemyHealth.TakeDamage(dotDamage);
            Debug.Log(enemyHealth.gameObject.name + " subit " + dotDamage + " dégâts du DOT !");
            yield return new WaitForSeconds(1f); // Applique les dégâts toutes les secondes
            timePassed += 1f;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(castPoint.position, range);
    }
}
