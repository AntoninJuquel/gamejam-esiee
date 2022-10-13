using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ReferenceSharing;
using UnityEngine;
using UnityEngine.Events;

public class City : MonoBehaviour
{
    public event Action<bool> isGameWin;
    [SerializeField] List<People> peoplesInspector = new List<People>();
    [SerializeField] double timer;
    [SerializeField] double startingBatteryLevel;
    List<Building> buildings = new List<Building>();
    [SerializeField] Reference<double> batteryLevelRef, startingBatteryRef, timerRef, batteryPercentRef;
    [SerializeField] private Reference<int> totalPeopleRef, deathRef, leaveRef;

    HashSet<People> peoples = new HashSet<People>();
    double tripTimer = 0;
    double batteryCount;
    int deadCount;
    int unhappyCount;

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
        return this.deadCount;
    }

    public double getChrono()
    {
        return this.timer;
    }

    public int getNbLeave()
    {
        return this.unhappyCount;
    }

    //M�thode qui ajoute des morts 
    public void addDead(int d)
    {
        this.deadCount += d;
    }

    //M�thode qui ajoute des personnes qui partent
    public void addUnhappy(int l)
    {
        this.unhappyCount += l;
    }

    //Fonction qui retourne le nb d'habitant en sommant le nb d'habitant dans chaque b�timent
    public int getNbPeople()
    {
        return peoples.Count;
    }

    //Fonction qui retourne la consommation total des b�timents
    public double getCityConsumption()
    {
        double somme = 0;
        foreach (var b in buildings)
        {
            somme += b.getActiveConsumption();
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
        buildings = FindObjectsOfType<Building>().ToList();
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

    void updateAllPeople()
    {
        HashSet<People> peopleToRemove = new HashSet<People>();
        foreach (var p in peoples)
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
        foreach (var p in peopleToRemove)
        {
            peoples.Remove(p);
        }
        totalPeopleRef.Value = getNbPeople();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPause)
        {
            tripTimer += Time.deltaTime;
            updateAllPeople();
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

        updateAllPeople();

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