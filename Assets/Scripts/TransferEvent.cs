using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferEvent : MonoBehaviour, IGameEvent
{
   public Building buildingA;
   public Building buildingB;
   public int nbPeopleTransfer;

   //M�thode qui transf�re nbPeopleTransfer du b�timent A au b�timent B
   public void action()
    {
        if(this.buildingA.getNbHab() == this.nbPeopleTransfer)
        {
        this.buildingB.addHab(this.nbPeopleTransfer);
        this.buildingA.removeHab(this.nbPeopleTransfer);
        }
        else { throw new InvalidOperationException("Le b�timent ne contient pas le nombre d'habitant que vous souhaitez tranf�rer"); }
    }
}
