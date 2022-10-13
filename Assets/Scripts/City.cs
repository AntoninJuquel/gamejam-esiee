using System;
using System.Collections;
using System.Collections.Generic;
using ReferenceSharing;
using UnityEngine;
using UnityEngine.Events;

public class City : MonoBehaviour
{
    [SerializeField] EventManager eventManager;
    public event Action<bool> isGameWin;
    [SerializeField] double timer;
    [SerializeField] double startingBatteryLevel;
    List<Building> buildings = new List<Building>();
    [SerializeField] Reference<double> batteryLevelRef, startingBatteryRef, timerRef, batteryPercentRef;
    [SerializeField] private Reference<int> totalPeopleRef, deathRef, leaveRef;

    double eventTimer = 0;
    double batteryCount;
    int nbDead;
    int nbLeave;

    public bool isPause = false;
    private bool playingAlert = false;

    //Les getters
    public double getBattery()
    {
        return this.batteryCount;
    }

    public List<Building> getBuilding()
    {
        return this.buildings;
    }

    public int getNbDead()
    {
        return this.nbDead;
    }

    public double getChrono()
    {
        return this.timer;
    }

    public int getNbLeave()
    {
        return this.nbLeave;
    }

    //Les setters
    public void setBattery(double b)
    {
        this.batteryCount = b;
    }

    public void setBuilding(List<Building> b)
    {
        this.buildings = b;
    }

    public void setNbDead(int n)
    {
        this.nbDead = n;
    }

    public void setChrono(double c)
    {
        this.timer = c;
    }

    public void setNbLeave(int n)
    {
        this.nbLeave = n;
    }

    //M�thode qui ajoute des morts 
    public void addDead(int d)
    {
        this.nbDead += d;
    }

    //M�thode qui ajoute des personnes qui partent
    public void addLeave(int l)
    {
        this.nbLeave += l;
    }

    //Fonction qui retourne le nb d'habitant en sommant le nb d'habitant dans chaque b�timent
    public int getNbPeople()
    {
        int somme = 0;
        for (int i = 0; i < this.buildings.Count; i++)
        {
            somme += this.buildings[i].getPeopleCount();
        }

        totalPeopleRef.Value = somme;
        return somme;
    }

    //Fonction qui retourne la consommation total des b�timents
    public double getCityConsumption()
    {
        double somme = 0;
        for (int i = 0; i < this.buildings.Count; i++)
        {
            somme += this.buildings[i].getActiveConsumption();
        }

        return somme;
    }


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.Stop("alert");
        batteryCount = startingBatteryLevel;
        startingBatteryRef.Value = startingBatteryLevel;
        batteryLevelRef.Value = startingBatteryLevel;
        batteryPercentRef.Value = batteryLevelRef / startingBatteryLevel;
        isPause = false;
        playingAlert = false;
        Building[] tabBuilding = FindObjectsOfType<Building>();
        for (int i = 0; i < tabBuilding.Length; i++)
        {
            this.buildings.Add(tabBuilding[i]);
        }
    }

    void win()
    {
        AudioManager.Instance.Stop("alert");
        isPause = true;
        isGameWin?.Invoke(true);
    }

    void lose()
    {
        AudioManager.Instance.Stop("alert");
        isPause = true;
        isGameWin?.Invoke(false);
    }

    void turnOffAllBuilding()
    {
        foreach (var b in buildings)
        {
            b.setIsConsumming(false);
        }
    }

    void updateAllPeopleCount()
    {
        foreach (var b in buildings)
        {
            b.unhappyCount += Time.deltaTime * b.getUnhappyRate();
            int unhappyPeople = (int)Math.Floor(b.unhappyCount);
            b.unhappyCount -= unhappyPeople;
            leaveRef.Value += unhappyPeople;

            b.mortalityCount += Time.deltaTime * b.getMortalityRate();
            int mortalityPeople = (int)Math.Floor(b.mortalityCount);
            b.mortalityCount -= mortalityPeople;
            deathRef.Value += mortalityPeople;

            b.subPeople(unhappyPeople + mortalityPeople);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPause)
        {
            eventTimer += Time.deltaTime;
            eventManager.UpdateEventList(eventTimer);
            timer -= Time.deltaTime;
            timerRef.Value = timer;
            batteryCount -= getCityConsumption() * Time.deltaTime;
            batteryLevelRef.Value = batteryCount;
            batteryPercentRef.Value = batteryCount / startingBatteryLevel;
            if (batteryCount / startingBatteryLevel <= .25 && !playingAlert)
            {
                playingAlert = true;
                AudioManager.Instance.Play("alert");
            }
        }

        if (batteryCount <= 0)
        {
            turnOffAllBuilding();
        }

        updateAllPeopleCount();

        if (timer <= 0)
        {
            win();
        }

        if (getNbPeople() == 0 && timer > 0)
        {
            lose();
        }
    }
}