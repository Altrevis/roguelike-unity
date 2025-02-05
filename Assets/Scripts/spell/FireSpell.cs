using UnityEngine;

public class FireSpell : MonoBehaviour
{
    public Transform castPoint;  // Point d'origine du sort
    public LayerMask targetMask; // Masque pour identifier les cibles
    public float range = 10f;    // Portée du sort
    public int damage = 25;      // Dégâts de l'éclair
    public LineRenderer lineRenderer;
    public float lightningDuration = 0.1f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic gauche pour lancer l'éclair
        {
            CastLightning();
        }
    }

    void CastLightning()
    {
        RaycastHit hit;
        lineRenderer.SetPosition(0, castPoint.position);

        if (Physics.Raycast(castPoint.position, castPoint.forward, out hit, range, targetMask))
        {
            lineRenderer.SetPosition(1, hit.point);

            // Vérifier si l'objet touché a une santé
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, castPoint.position + castPoint.forward * range);
        }

        StartCoroutine(ShowLightning());
    }

    System.Collections.IEnumerator ShowLightning()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(lightningDuration);
        lineRenderer.enabled = false;
    }
}
