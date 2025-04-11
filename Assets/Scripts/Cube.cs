using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _splitChance = 1f;
    [SerializeField] private float _reductionChance = 0.5f;
    [SerializeField] private float _scaleReduction = 2f;

    private float _currentSplitChance;

    private Spawner _spawner;

    private void Awake()
    {
        _currentSplitChance = _splitChance;
    }

    private void OnMouseDown()
    {
        if (ShouldSplit())
        {
            _spawner.SpawnNewCubes(this);
        }

        Destroy(gameObject);
    }

    public void Initialize(Spawner spawner, float splitChance)
    {
        _spawner = spawner;
        _splitChance = splitChance;
    }

    private bool ShouldSplit()
    {
        return Random.value <= _splitChance;
    }

    public float GetSplitChance()
    {
        return _splitChance;
    }

    public float GetReductionChance()
    {
        return _reductionChance;
    }

    public float GetScaleReduction()
    {
        return _scaleReduction;
    }

    public void SetSpawner(Spawner spawner)
    {
        _spawner = spawner;
    }
}