using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] double consumption;
    [SerializeField] double deathRate;
    [SerializeField] double unhappinessRate;
    [SerializeField] bool isConsuming;

    public bool IsConsumming { get => isConsuming; set => setIsConsumming(value); }
    public double DeathRate => isConsuming ? 0 : deathRate;
    public double UnhappinessRate => isConsuming ? 0 : unhappinessRate;
    public double Consumption => consumption;


    [SerializeField] private int windowIndex;
    [SerializeField] private GameObject[] windows;
    public event Action<int, bool> OnUpdate;
    private MeshRenderer _meshRenderer;
    private Material _windowMat;


    HashSet<People> peoples = new HashSet<People>();
    public int PeopleCount => peoples.Count;
    public bool HasPeople => peoples.Count > 0;

    /*
    double unhappy = 0;
    double death = 0;
    */

    public double getActiveConsumption()
    {
        return (IsConsumming) ? this.Consumption : 0;
    }

    void setIsConsumming(bool a)
    {
        isConsuming = a;
        if (a)
        {
            _windowMat.EnableKeyword("_EMISSION");
        }
        else
        {
            _windowMat.DisableKeyword("_EMISSION");
        }

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

    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _windowMat = Instantiate(Resources.Load<Material>("window"));
        var materials = _meshRenderer.sharedMaterials.ToList();
        materials[windowIndex] = _windowMat;
        _meshRenderer.materials = materials.ToArray();
    }

    public void UpdateRates(double deltaTime)
    {
        Debug.Log("X");
        if (!IsConsumming && HasPeople)
        {
            var alivePeoples = new HashSet<People>();
            foreach (var p in peoples)
            {
                p.Death += DeathRate * deltaTime;
                p.Unhappiness += UnhappinessRate * deltaTime;

                if (p.IsAlive)
                {
                    alivePeoples.Add(p);
                }
            }
            peoples = alivePeoples;
        }
    }
}