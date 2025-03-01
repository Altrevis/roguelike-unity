using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobBase : MonoBehaviour
{
    public Transform player;
    public int maxHealth = 50;
    private int currentHealth;
    
    public float speed = 2f;
    public float attackRange = 1.5f;
    public int baseDamage = 10;
    
    private bool isAttacking = false;
    private bool isBuffed = false;

    public float buffRange = 3f;  // Distance pour être en groupe
    private float buffMultiplier = 1f;

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
        CheckForBuff();
    }

    void FollowPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * (speed * buffMultiplier) * Time.deltaTime;
            transform.LookAt(player);
        }
    }

    void CheckForBuff()
    {
        Collider[] nearbyDragons = Physics.OverlapSphere(transform.position, buffRange);
        int dragonCount = 0;

        foreach (Collider collider in nearbyDragons)
        {
            if (collider.CompareTag("Dragon"))
            {
                dragonCount++;
            }
        }

        if (dragonCount >= 3 && !isBuffed)
        {
            ActivateBuff();
        }
        else if (dragonCount < 3 && isBuffed)
        {
            DeactivateBuff();
        }
    }

    void ActivateBuff()
    {
        Debug.Log(gameObject.name + " devient PLUS PUISSANT en groupe !");
        buffMultiplier = 1.5f;  // +50% vitesse
        baseDamage = Mathf.RoundToInt(baseDamage * 1.2f);  // +20% dégâts
        currentHealth += 5;  // Bonus de résistance
        isBuffed = true;
    }

    void DeactivateBuff()
    {
        Debug.Log(gameObject.name + " perd son buff !");
        buffMultiplier = 1f;
        baseDamage = Mathf.RoundToInt(baseDamage / 1.2f);
        isBuffed = false;
    }

    IEnumerator AttackLoop()
    {
        while (currentHealth > 0)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= attackRange)
            {
                isAttacking = true;
                Attack();
                yield return new WaitForSeconds(1.5f);
                isAttacking = false;
            }
            yield return null;
        }
    }

    void Attack()
    {
        Debug.Log("Dragon " + gameObject.name + " attaque avec ses griffes !");
        if (player.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(baseDamage);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " touché ! Vie restante : " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("💀 " + gameObject.name + " est mort !");
        Destroy(gameObject);
    }
}
