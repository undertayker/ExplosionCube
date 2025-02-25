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
        int maxNumber = 7;
        int minNumber = 2;

        if (Random.value < _splitChance)
        {
            int randomNumber = Random.Range(minNumber, maxNumber);
            Vector3 nextScale = parentScale / _reduction;

            List<Rigidbody> childRigidbodies = new List<Rigidbody>();

            for (int i = 0; i < randomNumber; i++)
            {
                Vector3 ramdomPosition = spawnPosition + Random.insideUnitSphere * 1f;
                GameObject cubeGameObject = Instantiate(_cubePrefab.gameObject, ramdomPosition, Quaternion.identity);
                Cube cube = cubeGameObject.GetComponent<Cube>();

                if (cube != null)
                {
                    Colorizer colorizer = cubeGameObject.AddComponent<Colorizer>();
                    Renderer renderer = cubeGameObject.GetComponent<Renderer>();
                    colorizer.SetRandomColor(renderer);

                    cube.transform.localScale = nextScale;

                    Rigidbody rigidbody = cubeGameObject.GetComponent<Rigidbody>();

                    if (rigidbody != null)
                    {
                        childRigidbodies.Add(rigidbody);
                    }

                    cube.Clicked += (clickedCube) =>
                    {
                        Spawn(nextScale, cube.transform.position);
                    };
                }
            }

            _splitChance /= _reduction;

            _exploder.Explode(spawnPosition, childRigidbodies);
        }
    }
}