using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferEvent : MonoBehaviour, IGameEvent
{
    [SerializeField] Building buildingA;
    [SerializeField] Building buildingB;
    [SerializeField] int nbPeopleTransfer;

    //M�thode qui transf�re nbPeopleTransfer du b�timent A au b�timent B
    public void action()
    {
        if (this.buildingA.getPeopleCount() == this.nbPeopleTransfer)
        {
            this.buildingB.addPeople(this.nbPeopleTransfer);
            this.buildingA.subPeople(this.nbPeopleTransfer);
        }
        else
        {
            throw new InvalidOperationException("Le b�timent ne contient pas le nombre d'habitant que vous souhaitez tranf�rer");
        }
    }
}