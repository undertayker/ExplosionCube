using UnityEngine;

public class Exploder : MonoBehaviour
{
    private float _explosionForse = 20f;
    private float _explosionRadius = 5f;

    public void Explode(Vector3 explosionCenter)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionCenter, _explosionRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_explosionForse, explosionCenter, _explosionRadius, 0, ForceMode.Impulse);
            }
        }
    }
}