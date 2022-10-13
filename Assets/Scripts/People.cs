using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class People
{
    [System.Serializable]
    class Trip
    {
        [SerializeField] public double Timer { get; }
        [SerializeField] public Building Destination { get; }
    }

    [SerializeField] List<Trip> trips = new List<Trip>();
    Building currentPosition;
    int currentTripIndex;

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

    public void UpdateTrips(double travelTimer)
    {
        while (hasTripsLeft() && isTraveling(travelTimer))
        {

        }
    }
}
