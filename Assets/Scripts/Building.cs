using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] int peopleCount;
    [SerializeField] double consumption;
    [SerializeField] double mortalityRate;
    [SerializeField] double unhappyRate;

    bool isConsumming;

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
        return this.mortalityRate;
    }

    public double getUnhappyRate()
    {
        return this.unhappyRate;
    }

    //Les setters
    public void setConso(double c)
    {
        this.consumption = c;
    }

    public void setIsConsumming(bool a)
    {
        this.isConsumming = a;
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
    }

    //M�thode qui retire des personnes
    public void subPeople(int h)
    {
        this.peopleCount -= h;
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}