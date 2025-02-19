using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

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
                    Exploder exploder = cubeGameObject.AddComponent<Exploder>();

                    cube.Clicked += (scale, position) =>
                    {
                        Spawn(nextScale, position);
                        exploder.Explode(position);
                    };
                }
            }

            _splitChance /= _reduction;
        }
    }
}