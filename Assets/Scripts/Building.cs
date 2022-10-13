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
    public event Action<int> OnPeopleUpdate; 
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
        OnPeopleUpdate?.Invoke(peopleCount);
    }

    //M�thode qui retire des personnes
    public void subPeople(int h)
    {
        peopleCount -= (peopleCount > h) ? h : peopleCount;
        OnPeopleUpdate?.Invoke(peopleCount);
    }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _windowMat = Instantiate(_meshRenderer.sharedMaterials[windowIndex]);
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

    // Update is called once per frame
    void Update()
    {
    }
}