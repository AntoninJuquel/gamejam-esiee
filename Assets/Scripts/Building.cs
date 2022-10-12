using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    double conso;
    bool isConsumming;
    int nbPeople;
    double mortalityRate;
    double unhappyRate;

    //Les getters
    public double getConso() { return conso; }
    public bool getIsConsumming() { return isConsumming; }
    public int getNbPeople() { return nbPeople; }
    public double getMortalityRate() { return mortalityRate; }
    public double getUnhappyRate() { return unhappyRate; }

    //Les setters
    public void setConso(double c) { this.conso = c; }
    public void setIsConsumming(bool a) { this.isConsumming = a; }
    public void setNbPeople(int p) { this.nbPeople = p; }
    public void setMortalityRate(double m) { this.mortalityRate = m; }
    public void setUnhappyRate(double u) { this.unhappyRate = u; }

    //Méthode qui ajoute des personnes
    public void add(int people) { this.nbPeople += people; }    

    //Méthode qui retire des personnes
    public void remove(int people) { this.nbPeople -= people; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
