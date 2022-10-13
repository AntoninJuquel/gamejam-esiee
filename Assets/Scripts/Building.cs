using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] int peopleCount;
    [SerializeField] double consumption;
    [SerializeField] double mortalityRate;
    [SerializeField] double unhappyRate;
    [SerializeField] private GameObject[] windows;
    public event Action<int, bool> OnUpdate;
    public double mortalityCount { get; set; } = 0;
    public double unhappyCount { get; set; } = 0;

    [SerializeField] private int windowIndex;

    bool isConsumming;
    private MeshRenderer _meshRenderer;
    private Material _windowMat;

    //Les getters
    public double getActiveConsumption()
    {
        return (isConsumming) ? this.consumption : 0;
    }

    public double getConsumption()
    {
        return this.consumption;
    }

    public bool getIsConsumming()
    {
        return this.isConsumming;
    }

    public int getPeopleCount()
    {
        return this.peopleCount;
    }

    public double getMortalityRate()
    {
        return (isConsumming || peopleCount <= 0) ? 0 : mortalityRate;
    }

    public double getUnhappyRate()
    {
        return (isConsumming || peopleCount <= 0) ? 0 : unhappyRate;
    }

    //Les setters
    public void setConso(double c)
    {
        this.consumption = c;
    }

    public void setIsConsumming(bool a)
    {
        this.isConsumming = a;

        if (a)
            _windowMat.EnableKeyword("_EMISSION");
        else
            _windowMat.DisableKeyword("_EMISSION");
        foreach (var window in windows)
        {
            window.SetActive(a);
        }
        OnUpdate?.Invoke(peopleCount, isConsumming);
    }

    public void setNbHab(int p)
    {
        this.peopleCount = p;
    }

    public void setMortalityRate(double m)
    {
        this.mortalityRate = m;
    }

    public void setUnhappyRate(double u)
    {
        this.unhappyRate = u;
    }

    //M�thode qui ajoute des personnes
    public void addPeople(int h)
    {
        this.peopleCount += h;
        OnUpdate?.Invoke(peopleCount, isConsumming);
    }

    //M�thode qui retire des personnes
    public void subPeople(int h)
    {
        peopleCount -= (peopleCount > h) ? h : peopleCount;
        OnUpdate?.Invoke(peopleCount, isConsumming);
    }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _windowMat = Instantiate(Resources.Load<Material>("window"));
        var materials = _meshRenderer.sharedMaterials.ToList();
        materials[windowIndex] = _windowMat;
        _meshRenderer.materials = materials.ToArray();
    }

    // Start is called before the first frame update
    void Start()
    {
        mortalityCount = 0;
        unhappyCount = 0;
    }
}