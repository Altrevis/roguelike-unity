using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 1f; // Rayon d'aggro
    private Vector3 targetPosition;
    private Transform player;
    private bool isAggro = false;

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        StartCoroutine(ChangeDirection());
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isAggro = true;
            targetPosition = player.position;
        }
        else
        {
            isAggro = false;
        }

        Move();
    }

    void Move()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(targetPosition);
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            if (!isAggro) // Si l'ennemi n'a pas détecté le joueur
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized * 3f; // Mouvement aléatoire
                targetPosition = new Vector3(transform.position.x + randomDirection.x, transform.position.y, transform.position.z + randomDirection.y);
            }

            yield return new WaitForSeconds(Random.Range(2f, 4f)); // Change de direction toutes les 2 à 4 secondes
        }
    }

    public IEnumerator ApplySlow(float slowFactor, float duration)
    {
        moveSpeed *= slowFactor;
        Debug.Log(gameObject.name + " est ralenti !");
        
        yield return new WaitForSeconds(duration);
        
        moveSpeed /= slowFactor;
        Debug.Log(gameObject.name + " retrouve sa vitesse normale.");
    }
}
