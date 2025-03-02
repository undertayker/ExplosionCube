using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Exploder _exploder;

    private Vector3 _initialSize = new Vector3(2.0f, 2.0f, 2.0f);
    private float _splitChance = 1.0f;
    private float _reduction = 2.0f;

    private void Start()
    {
        Spawn(_initialSize, transform.position);
    }

    private void Spawn(Vector3 parentScale, Vector3 spawnPosition)
    {
        if (ShouldSplit())
        {
            Vector3 nextScale = CalculateNextScale(parentScale);
            List<Rigidbody> childRigidbodies = CreateChildCubes(spawnPosition, nextScale);
            ApplyExplosion(spawnPosition, childRigidbodies);

            _splitChance /= _reduction;
        }
    }

    private void ConfigureCube(Cube cube, Vector3 nextScale, Vector3 randomPosition)
    {
        Colorizer colorizer = cube.gameObject.AddComponent<Colorizer>();
        Renderer renderer = cube.GetComponent<Renderer>();
        colorizer.SetRandomColor(renderer);

        cube.transform.localScale = nextScale;

        cube.Clicked += (clickedCube) =>
        {
            Spawn(nextScale, cube.transform.position);
        };
    }

    private List<Rigidbody> CreateChildCubes(Vector3 spawnPosition, Vector3 nextScale)
    {
        int randomNumber = Random.Range(2, 7);
        List<Rigidbody> childRigidbodies = new List<Rigidbody>();

        for (int i = 0; i < randomNumber; i++)
        {
            Vector3 randomPosition = spawnPosition + Random.insideUnitSphere * 1f;
            Cube cube = Instantiate(_cubePrefab, randomPosition, Quaternion.identity);

            if (cube != null)
            {
                ConfigureCube(cube, nextScale, randomPosition);

                Rigidbody rigidbody = cube.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    childRigidbodies.Add(rigidbody);
                }
            }
        }

        return childRigidbodies;
    }

    private bool ShouldSplit()
    {
        return Random.value < _splitChance;
    }

    private Vector3 CalculateNextScale(Vector3 parentScale)
    {
        return parentScale / _reduction;
    }

    private void ApplyExplosion(Vector3 spawnPosition, List<Rigidbody> childRigidbodies)
    {
        _exploder.Explode(spawnPosition, childRigidbodies);
    }   
}