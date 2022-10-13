using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] public double Consumption { get; }
    [SerializeField] public double DeathRate { get; }
    [SerializeField] public double UnhappyRate { get; }
    [SerializeField] public bool IsConsumming { get => IsConsumming; set => setIsConsumming(value); }

    [SerializeField] private int windowIndex;

    [SerializeField] private GameObject[] windows;
    public event Action<int, bool> OnUpdate;
    private MeshRenderer _meshRenderer;
    private Material _windowMat;

    HashSet<People> peoples = new HashSet<People>();

    public int PeopleCount => peoples.Count;
    public bool HasPeople => peoples.Count>0;

    double unhappy = 0;
    double death = 0;

    public double getActiveConsumption()
    {
        return (IsConsumming) ? this.Consumption : 0;
    }

    void setIsConsumming(bool a)
    {
        this.IsConsumming = a;

        if (a)
            _windowMat.EnableKeyword("_EMISSION");
        else
            _windowMat.DisableKeyword("_EMISSION");
foreach (var window in windows)
        {
            window.SetActive(a);
        }
        OnUpdate?.Invoke(PeopleCount, IsConsumming);
    }

    public void AddPeople(People p)
    {
        peoples.Add(p);
        OnUpdate?.Invoke(PeopleCount, IsConsumming);
    }

    public void RemovePeople(People p)
    {
        peoples.Remove(p);
        OnUpdate?.Invoke(PeopleCount, IsConsumming);
    }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _windowMat = Instantiate(Resources.Load<Material>("window"));
        var materials = _meshRenderer.sharedMaterials.ToList();
        materials[windowIndex] = _windowMat;
        _meshRenderer.materials = materials.ToArray();
    }

    void Update()
    {
        if (!IsConsumming && HasPeople)
        {
            death += DeathRate * Time.deltaTime;
            unhappy += UnhappyRate * Time.deltaTime;
        }

        while (death >= 1)
        {
            death--;

        }
    }
}