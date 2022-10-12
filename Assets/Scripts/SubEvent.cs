using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubEvent : MonoBehaviour, IGameEvent
{
    public Building building;
    public int nbPeopleToSub;

    public void action()
    {
        if (this.building.getNbHab() == this.nbPeopleToSub)
        {
            this.building.removeHab(this.nbPeopleToSub);
        }
        else { throw new InvalidOperationException("Le bâtiment ne contient pas le nombre d'habitant que vous souhaitez supprimer"); }
    }
}
