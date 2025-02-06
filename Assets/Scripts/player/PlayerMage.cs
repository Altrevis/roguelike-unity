using UnityEngine;

public class PlayerMage : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform castPoint;
    public float fireballSpeed = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Clique gauche pour tirer
        {
            CastFireball();
        }
    }

    void CastFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, castPoint.position, Quaternion.identity);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * fireballSpeed;
    }
}
