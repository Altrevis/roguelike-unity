using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public LayerMask enemyLayer;
    public int currentHealth = 100;
    private bool isTakingDot = false;
    private bool isKnockedBack = false;
    public bool isDead = false;

    void Start()
    {
        if (enemyLayer.value == 0)
        {
            Debug.LogError(gameObject.name + " : Aucun layer d'ennemi assigné !");
        }
        else
        {
            gameObject.layer = Mathf.RoundToInt(Mathf.Log(enemyLayer.value, 2));
        }
    }

    public int TakeDamage(int amount)
    {
        Debug.Log(gameObject.name + " prend " + amount + " dégâts !");
        currentHealth -= amount;
        CheckDeath();
        return amount;
    }

    public void ApplyDot(int damagePerSecond, float duration)
    {
        if (!isTakingDot)
        {
            StartCoroutine(DotCoroutine(damagePerSecond, duration));
        }
    }

    private IEnumerator DotCoroutine(int damagePerSecond, float duration)
    {
        isTakingDot = true;
        float elapsed = 0;

        while (elapsed < duration)
        {
            TakeDamage(damagePerSecond);
            yield return new WaitForSeconds(1f);
            elapsed += 1f;
        }

        isTakingDot = false;
    }

    public void KnockBack(Vector3 force, float duration)
    {
        if (!isKnockedBack)
        {
            StartCoroutine(KnockBackCoroutine(force, duration));
        }
    }

    private IEnumerator KnockBackCoroutine(Vector3 force, float duration)
    {
        isKnockedBack = true;
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(force, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(duration);
        isKnockedBack = false;
    }

    void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
{
    Debug.Log(gameObject.name + " est mort !");
    isDead = true;

    // Vérifie si un Collider est attaché avant de le désactiver
    Collider collider = GetComponent<Collider>();
    if (collider != null)
    {
        collider.enabled = false;
    }
    else
    {
        Debug.LogWarning(gameObject.name + " n'a pas de Collider attaché !");
    }

    Destroy(gameObject); // Détruit l'objet après la mort
}

}