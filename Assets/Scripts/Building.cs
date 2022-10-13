using System;
using System.Linq;
using UnityEngine;

public class Building : MonoBehaviour
{
    public event Action<int, bool> OnUpdate;

    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private GameObject deathParticles;

    [SerializeField] private int windowIndex;
    private MeshRenderer _meshRenderer;
    private Material _windowMat;

    [field: SerializeField] public int Population { get; private set; }
    [field: SerializeField] public double PowerConsumption { get; private set; }

    private double _fleeAmount, _fleeTimer;
    [SerializeField] private double fleeTime, fleeRate;
    [SerializeField] private int fleeNumber;

    private double _deathTimer;
    [SerializeField] [Range(0f, 1f)] private double deathRate;

    public bool Powered { get; private set; }
    public double CurrentConsumption => Powered ? PowerConsumption : 0;

    public bool TogglePower()
    {
        SetPower(!Powered);
        return Powered;
    }

    public void SetPower(bool value)
    {
        Powered = value;
        if (Powered)
            _windowMat.EnableKeyword("_EMISSION");
        else
            _windowMat.DisableKeyword("_EMISSION");
        OnUpdate?.Invoke(Population, Powered);
    }

    public void AddPopulation(int amount)
    {
        Population += amount;
        OnUpdate?.Invoke(Population, Powered);
    }

    public void RemovePopulation(int amount)
    {
        Population = Mathf.Max(0, Population - amount);
        OnUpdate?.Invoke(Population, Powered);
    }

    private void Awake()
    {
        InitializeWindowsMaterial();
    }

    private void Start()
    {
        _fleeAmount = 0;
        _fleeTimer = 0;
        _deathTimer = 0;
    }

    private void InitializeWindowsMaterial()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _windowMat = Instantiate(Resources.Load<Material>("window"));
        var materials = _meshRenderer.sharedMaterials.ToList();
        materials[windowIndex] = _windowMat;
        _meshRenderer.materials = materials.ToArray();
    }

    public void HandlePopulationBehaviours()
    {
        if (Population == 0) return;

        _fleeAmount = Mathf.Clamp((float) (_fleeAmount + Time.deltaTime * (Powered ? -1 : 1)), 0f, (float) fleeTime);

        if (_fleeAmount >= fleeTime)
        {
            _fleeTimer += Time.deltaTime * fleeRate;
            if (_fleeTimer >= 1)
            {
                _fleeTimer = 0;
                RemovePopulation(fleeNumber);
            }
        }
        else
        {
            _fleeTimer = 0;
        }

        if (Powered) return;
        
        _deathTimer += Time.deltaTime * deathRate;
        if (!(_deathTimer >= 1)) return;
        _deathTimer = 0;
        RemovePopulation(1);
        Instantiate(deathParticles, spawnPoint + transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(spawnPoint + transform.position, .1f);
    }
}