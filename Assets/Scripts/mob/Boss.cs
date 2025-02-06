using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public Transform player;
    public int maxHealth = 500;
    private int currentHealth;
    
    public float speed = 3f;
    public float attackRange = 5f;
    public float chargeSpeed = 8f;
    
    public GameObject fireBreathPrefab;  // Effet de feu
    public GameObject meteorPrefab;      // Météores

    private bool isEnraged = false;
    private bool isAttacking = false;

    private Rigidbody rb;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(AttackLoop());
    }

    void Update()
    {
        if (!isAttacking)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.LookAt(player);
        }
    }

    IEnumerator AttackLoop()
    {
        while (currentHealth > 0)
        {
            yield return new WaitForSeconds(3f);
            isAttacking = true;

            int attackChoice = Random.Range(0, 3);
            if (attackChoice == 0)
                StartCoroutine(FireBreath());
            else if (attackChoice == 1)
                StartCoroutine(Charge());
            else
                StartCoroutine(SummonMeteors());

            yield return new WaitForSeconds(2f);
            isAttacking = false;
        }
    }

    IEnumerator FireBreath()
    {
        Debug.Log("🔥 Le dragon crache du feu !");
        GameObject fire = Instantiate(fireBreathPrefab, transform.position + transform.forward * 2, transform.rotation);
        yield return new WaitForSeconds(2f);
        Destroy(fire);
    }

    IEnumerator Charge()
    {
        Debug.Log("🐉 Le dragon charge !");
        Vector3 chargeDirection = (player.position - transform.position).normalized;
        rb.AddForce(chargeDirection * chargeSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(1f);
        rb.linearVelocity = Vector3.zero;
    }

    IEnumerator SummonMeteors()
    {
        Debug.Log("☄️ Pluie de météores !");
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-3f, 3f), 10f, Random.Range(-3f, 3f));
            Instantiate(meteorPrefab, player.position + randomOffset, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Le boss a " + currentHealth + " PV restants.");

        if (currentHealth <= maxHealth * 0.3f && !isEnraged)
        {
            Enrage();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Enrage()
    {
        Debug.Log("🔥 Le dragon devient FURIEUX !");
        speed *= 1.5f;
        attackRange += 2f;
        isEnraged = true;
    }

    void Die()
    {
        Debug.Log("💀 Le dragon est vaincu !");
        Destroy(gameObject);
    }
}
