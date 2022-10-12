using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    [System.Serializable]
    public class TimedEvent : IGameEvent
    {
        public TimedEvent(double time, Component gameEvent)
        {
            this.time = time;
            this.gameEvent = gameEvent;
        }
        public double time;
        public Component gameEvent;
        public void action()
        {
            (gameEvent as IGameEvent)?.action();
        }
        public double getTime()
        {
            return time;
        }
    };

    private int currentEventIndex = 0;
    private double timer = 0;
    [System.Serializable] private List<TimedEvent> timelines = new List<TimedEvent>();
/*
    public void addEvent(double time, IGameEvent gameEvent)
    {
        timelines.Add(new TimedEvent(time, gameEvent));
    }
*/
    // Start is called before the first frame update
    void Start()
    {

    }

    private bool isCurrentEventOccuring()
    {
        return timelines[currentEventIndex].getTime() < timer;
    }


    private bool hasEventLeft()
    {
        return timelines.Count > currentEventIndex;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        while (hasEventLeft() && isCurrentEventOccuring())
        {
            var currentEvent = timelines[currentEventIndex++];
            currentEvent.action();
        }

    }
}
