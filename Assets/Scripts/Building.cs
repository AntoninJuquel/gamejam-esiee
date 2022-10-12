using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public double conso;
    public int nbHab;
    public double mortalityRate;
    public double unhappyRate;
    
    bool isConsumming;

    //Les getters
    public double getConso() 
    {
        if (isConsumming)
        {
            return this.conso;
        }
        return 0;
    }
    public bool getIsConsumming() { return this.isConsumming; }
    public int getNbHab() { return this.nbHab; }
    public double getMortalityRate() { return this.mortalityRate; }
    public double getUnhappyRate() { return this.unhappyRate; }

    //Les setters
    public void setConso(double c) { this.conso = c; }
    public void setIsConsumming(bool a) { this.isConsumming = a; }
    public void setNbHab(int p) { this.nbHab = p; }
    public void setMortalityRate(double m) { this.mortalityRate = m; }
    public void setUnhappyRate(double u) { this.unhappyRate = u; }

    //Méthode qui ajoute des personnes
    public void addHab(int h) { this.nbHab += h; }    

    //Méthode qui retire des personnes
    public void removeHab(int h) { this.nbHab -= h; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
