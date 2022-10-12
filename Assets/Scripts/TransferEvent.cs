using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferEvent : MonoBehaviour, IGameEvent
{
   public Building buildingA;
   public Building buildingB;
   public int nbPeopleTransfer;

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
