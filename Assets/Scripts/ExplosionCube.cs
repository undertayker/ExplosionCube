using UnityEngine;

public class ExplosionCube : MonoBehaviour
{
    private float _explosionForse = 20f;
    private float _explosionRadius = 5f;

    private Cube _cube;

    private void Start()
    {
        _cube = GetComponent<Cube>();
    }
    private void OnEnable()
    {
        if (_cube != null)
        {
            _cube.Explosion += Puff;
        }
    }

    private void OnDisable()
    {
        if (_cube != null)
        {
            _cube.Explosion -= Puff;
        }
    }

    public void Puff(Vector3 scale, Vector3 explosionCenter)
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