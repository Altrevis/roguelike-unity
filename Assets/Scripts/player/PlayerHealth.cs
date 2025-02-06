using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Le joueur a " + health + " PV restants.");
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("💀 Le joueur est mort !");
    }
}
