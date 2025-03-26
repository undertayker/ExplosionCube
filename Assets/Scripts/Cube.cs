using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private int _minNumberCubes = 2;
    private int _maxNumberCubes = 6;
    private float _spawnRadius = 1f;

    private float _reduction = 2f;
    private float _splitChance = 1f;
    private float _reductionChance = 0.5f;

    private void OnMouseDown()
    {
        if (ShouldSplit())
        {
            Vector3 explosionPosition = transform.position;
            SpawnNewCubes(explosionPosition);
        }

        Destroy(gameObject);
    }

    private bool ShouldSplit()
    {
        return Random.value <= _splitChance;
    }

    private void SpawnNewCubes(Vector3 explosionPosition)
    {
        int randomNumber = Random.Range(_minNumberCubes, _maxNumberCubes + 1);
        List<Rigidbody> newCubeRigidbodie = new List<Rigidbody>();

        for (int i = 0; i < randomNumber; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * _spawnRadius;
            Cube newCube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);
           

            if (newCube != null)
            {
                newCube.transform.localScale = transform.localScale / _reduction;
                newCube._splitChance = _splitChance * _reductionChance;
                
                ColorizeCube(newCube);

                Rigidbody rigidbody = newCube.GetComponent<Rigidbody>();

                if (rigidbody != null)
                {
                    ApplyGravity(newCube);
                    newCubeRigidbodie.Add(rigidbody);
                }
                
                ApplyExplosion(explosionPosition, newCubeRigidbodie);
            }
        }
    }

    private void ColorizeCube(Cube cube)
    {
        Renderer renderer = cube.GetComponent<Renderer>();
        Colorizer colorizer = cube.GetComponent<Colorizer>();

        if (renderer != null && colorizer != null)
        {
            colorizer.SetRandomColor(renderer);
        }
    }

    private void ApplyExplosion(Vector3 explosionPosition, List<Rigidbody> rigidbodies)
    {
        Exploder exploder = GetComponent<Exploder>();

        if (exploder != null)
        {
            exploder.Explode(explosionPosition, rigidbodies);
        }
    }

    private void ApplyGravity(Cube cube)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.useGravity = true;
        }
    }
}