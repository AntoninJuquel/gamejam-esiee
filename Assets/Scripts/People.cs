using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class People
{
    [System.Serializable]
    class Trip
    {
        [SerializeField] double timer;
        [SerializeField] Building destination;
        public Building Destination => destination;
        public double Timer => timer;
    }

    public double Death { get; set; } = 0;
    public double Unhappiness { get; set; } = 0;
    public bool IsDead { get => Death >= 1; }
    public bool IsUnhappy { get => Unhappiness >= 1; }
    public bool IsAlive { get => !IsDead && !IsUnhappy; }

    [SerializeField] List<Trip> trips = new List<Trip>();
    [SerializeField] Building currentPosition;
    int currentTripIndex = 0;

    private bool hasTripsLeft()
    {
        return currentTripIndex < trips.Count;
    }

    private bool isTraveling(double travelTimer)
    {
        return trips[currentTripIndex].Timer >= travelTimer;
    }

    public void travel()
    {
        currentPosition.RemovePeople(this);
        currentPosition = trips[currentTripIndex++].Destination;
        currentPosition.AddPeople(this);
    }

    public void Start()
    {
        currentPosition.AddPeople(this);
    }

    public void UpdateTrips(double travelTimer)
    {
        while (hasTripsLeft() && isTraveling(travelTimer))
        {

        }
    }
}
