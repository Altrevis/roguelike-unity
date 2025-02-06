using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // Vitesse de base

    public IEnumerator ApplySlow(float slowFactor, float duration)
    {
        moveSpeed *= slowFactor; // Réduit la vitesse
        Debug.Log(gameObject.name + " est ralenti !");
        
        yield return new WaitForSeconds(duration);
        
        moveSpeed /= slowFactor; // Remet la vitesse normale
        Debug.Log(gameObject.name + " retrouve sa vitesse normale.");
    }
}
