using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{


    public LayerMask enemyLayer;
    public int currentHealth = 100;
    private bool isTakingDot = false; // Empêche plusieurs DOT en même temps
    private bool isKnockedBack = false; // Empêche plusieurs coups de bouclier simultanés

    public bool isDead = false;
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(LayerMask.LayerToName(enemyLayer));
    }

    // Dégâts instantanés (ex: foudre, glace, sort brut)
    public int TakeDamage(int amount)
{
    Debug.Log(gameObject.name + " prend " + amount + " dégâts !");
    currentHealth -= amount;
    CheckDeath();
    return amount;
}


    // Dégâts sur la durée (DOT)
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
            yield return new WaitForSeconds(1f); // Applique les dégâts chaque seconde
            elapsed += 1f;
        }

        isTakingDot = false;
    }

    // Effet de recul pour le ShieldSpell (coup de bouclier)
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
    GetComponent<Collider2D>().enabled = false; // Disable the collider to prevent further interactions
}
}
