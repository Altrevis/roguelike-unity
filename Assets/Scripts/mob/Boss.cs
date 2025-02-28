using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    [Header("Boss Settings")]
    public Transform player;
    public int maxHealth = 500;
    private int currentHealth;

    [Header("Movement Settings")]
    public float speed = 3f;
    public float attackRange = 5f;
    public float chargeSpeed = 8f;

    [Header("Attack Settings")]
    public GameObject fireBreathPrefab;
    public GameObject meteorPrefab;
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
        }
    }

    IEnumerator AttackLoop()
    {
        while (currentHealth > 0)
        {
            yield return new WaitForSeconds(3f);
            isAttacking = true;
            int attackChoice = Random.Range(0, 3);
            
            switch (attackChoice)
            {
                case 0:
                    yield return StartCoroutine(FireBreath());
                    break;
                case 1:
                    yield return StartCoroutine(ChargeAttack());
                    break;
                case 2:
                    yield return StartCoroutine(SummonMeteor());
                    break;
            }
            
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
    
    IEnumerator ChargeAttack()
    {
        Debug.Log("⚡ Le dragon fonce sur sa cible !");
        Vector3 chargeDirection = (player.position - transform.position).normalized;
        float startTime = Time.time;
        float chargeDuration = 1f;

        while (Time.time < startTime + chargeDuration)
        {
            transform.position += chargeDirection * chargeSpeed * Time.deltaTime;
            yield return null;
        }
    }
    
    IEnumerator SummonMeteor()
    {
        Debug.Log("☄️ Le dragon invoque une pluie de météores !");
        Vector3 spawnPosition = player.position + new Vector3(Random.Range(-3f, 3f), 10f, Random.Range(-3f, 3f));
        Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
        yield return new WaitForSeconds(2f);
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
        chargeSpeed *= 1.5f;
        isEnraged = true;
    }

    void Die()
    {
        Debug.Log("💀 Le dragon est vaincu !");
        Destroy(gameObject);
    }
}
