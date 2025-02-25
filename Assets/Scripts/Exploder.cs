using UnityEngine;
using System.Collections.Generic;

public class Exploder : MonoBehaviour
{
    private float _explosionForse = 10f;
    private float _explosionRadius = 5f;

    public void Explode(Vector3 explosionCenter, List<Rigidbody> rigidbodies)
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            if (rigidbody != null)
            {
                float distance = Vector3.Distance(explosionCenter, rigidbody.transform.position);

                if (distance <= _explosionRadius)
                {
                    rigidbody.AddExplosionForce(_explosionForse, explosionCenter, _explosionRadius, 0, ForceMode.Impulse);
                }
            }
        }
    }
}