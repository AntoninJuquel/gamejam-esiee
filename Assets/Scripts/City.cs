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
    int deathCount;
    int unhappinessCount;

    public bool isPause = false;
    private bool playingAlert = false;
    public int PeopleCount => peoples.Count;
    public bool HasPeople => peoples.Count > 0;

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
        return this.deathCount;
    }

    public double getChrono()
    {
        return this.timer;
    }

    public int getNbLeave()
    {
        return this.unhappinessCount;
    }

    //Fonction qui retourne la consommation total des b�timents
    public double GetCityConsumption()
    {
        double somme = 0;
        foreach (var b in buildings)
        {
            somme += b.getActiveConsumption();
        }
        return somme;
    }

    void Start()
    {
        AudioManager.Instance.Stop("alert");
        batteryCount = startingBatteryLevel;
        startingBatteryRef.Value = startingBatteryLevel;
        batteryLevelRef.Value = startingBatteryLevel;
        batteryPercentRef.Value = batteryLevelRef / startingBatteryLevel;
        playingAlert = false;
        buildings = FindObjectsOfType<Building>().ToList();
        peoples = peoplesInspector.ToHashSet();
        
        foreach (var p in peoples)
        {
            Debug.Log("before");
            p.Start();
            Debug.Log("after");
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
            b.IsConsumming = false;
        }
    }

    void updateAllPeoples()
    {
        Debug.Log("1");
        foreach (var b in buildings)
        {
            b.UpdateRates(Time.deltaTime);
        }

        Debug.Log("2");
        var alivePeoples = new HashSet<People>();
        foreach (var p in peoples)
        {
            p.UpdateTrips(tripTimer);
            if (p.IsDead)
            {
                Debug.Log(p);
                deathCount++;
            }
            else if (p.IsUnhappy)
            {
                Debug.Log(p);
                unhappinessCount++;
            }
            else
            {
                alivePeoples.Add(p);
            }
        }
        peoples = alivePeoples;
    }

    void Update()
    {
        if (!isPause)
        {
            Debug.Log("not paused");
            tripTimer += Time.deltaTime;
            updateAllPeoples();
            timer -= Time.deltaTime;
            timerRef.Value = timer;
            batteryCount -= GetCityConsumption() * Time.deltaTime;
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

        if (timer <= 0)
        {
            win();
        }

        if (!HasPeople && timer > 0)
        {
            lose();
        }
    }
}