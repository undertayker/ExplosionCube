using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private int _cubeCount = 3;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < _cubeCount; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 1f;
            Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);
        }
    }
}