using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferEvent : MonoBehaviour, IGameEvent
{

    [System.Serializable] Building buildingA;
    [System.Serializable] Building buildingB;
    [System.Serializable] int nbPeopleTransfer;

    //Méthode qui transfère nbPeopleTransfer du bâtiment A au bâtiment B
    public void action()
    {
        if(this.buildingA.getNbHab() == this.nbPeopleTransfer)
        {
            this.buildingB.addHab(this.nbPeopleTransfer);
            this.buildingA.removeHab(this.nbPeopleTransfer);
        }
        else { throw new InvalidOperationException("Le bâtiment ne contient pas le nombre d'habitant que vous souhaitez tranférer"); }
    }
}
