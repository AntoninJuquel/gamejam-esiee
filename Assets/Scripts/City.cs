using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour 
{
    [System.Serializable] double timer;
    [System.Serializable] double startingBatteryLevel;
    [System.Serializable] List<Building> building = new List<Building>() ;
    
    double batteryCount;
    int nbDead;
    int nbLeave;

    //Les getters
    public double getBattery() { return this.battery; }
    public List<Building> getBuilding() { return this.building; }
    public int getNbDead() { return this.nbDead; }
    public double getChrono() { return this.timer; }
    public int getNbLeave() { return this.nbLeave; }

    //Les setters
    public void setBattery(double b) { this.battery = b; }
    public void setBuilding(List<Building> b) { this.building = b; }
    public void setNbDead(int n) { this.nbDead = n; }
    public void setChrono(double c) { this.timer = c; }
    public void setNbLeave(int n) { this.nbLeave = n; }

    //Méthode qui ajoute des morts 
    public void addDead(int d) { this.nbDead += d; }

    //Méthode qui ajoute des personnes qui partent
    public void addLeave(int l) { this.nbLeave += l; }

    //Fonction qui retourne le nb d'habitant en sommant le nb d'habitant dans chaque bâtiment
    public int getNbPeople() 
    {
        int somme = 0;
        for (int i = 0; i < this.building.Count; i++)
        {
            somme += this.building[i].getNbHab();
        }
        return somme;
    }

    //Fonction qui retourne la consommation total des bâtiments
    public double getCityConsumption()
    {
        double somme = 0;
        for(int i = 0; i < this.building.Count; i++)
        {
            somme += this.building[i].getConso();
        }
        return somme;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        batteryCount -= getCityConsumption();
    }
}
