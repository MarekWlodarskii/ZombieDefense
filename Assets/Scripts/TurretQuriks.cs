using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretQuriks : MonoBehaviour
{
    public static TurretQuriks main;

    public string[] possibleQuriks = new string[]{"Turret atakuje 4 razy szybciej, ale zadaje 4 razy mniejsze obrazenia",
    "Turret atakuje 4 razy wolniej, ale zadaje 4 razy wieksze obrazenia",
    "Turret zadaje obrazenia z modifier x1 w kazdy typ przeciwnika",
    "Modifiery dla Light i Heavy Turretow sa 2 razy wieksze / mniejsze",
    "Turret ma 25% szans na wystrzelenie dodatkowego strzalu",
    "Turret ma 25% szans na zadanie podwojnych obrazen"};
    public List<int> currentQuriks= new List<int>();
    void Start()
    {
        main = this;
        currentQuriks = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        foreach(GameObject turret in turrets){
            foreach(int quirk in currentQuriks){
                if(turret.GetComponent<Turret>().quirks.Contains(quirk)){
                    //
                }
                else{
                    turret.GetComponent<Turret>().quirks.Add(quirk);
                    switch(quirk){
                        case 0:{
                            turret.GetComponent<Turret>().firePerSecond *= 4;
                            turret.GetComponent<Turret>().damageBottom /= 4;
                            turret.GetComponent<Turret>().DamageTop /= 4;
                            break;
                        }
                        case 1:{
                            turret.GetComponent<Turret>().firePerSecond /= 4;
                            turret.GetComponent<Turret>().damageBottom *= 4;
                            turret.GetComponent<Turret>().DamageTop *= 4;
                            break;
                        }
                        case 2:{
                            turret.GetComponent<Turret>().towerType = "x1";
                            break;
                        }
                        case 3:{
                            turret.GetComponent<Turret>().towerType += " x2";
                            break;
                        }
                        case 4:{
                            turret.GetComponent<Turret>().doubleShot = true;
                            break;
                        }
                        case 5:{
                            turret.GetComponent<Turret>().krytyk = true;
                            break;
                        }
                        default: break;
                    }
                }
            }
        }
    }

    public int RandQuirk(int quirk1, int quirk2){
        int randomQurik;
        while(true){
            randomQurik = UnityEngine.Random.Range(0, possibleQuriks.Length);
            if(randomQurik != quirk1 && randomQurik != quirk2)
            {
                if(currentQuriks.Count != 0){
                    foreach(int i in currentQuriks){
                        if(randomQurik == i){
                            break;
                        }
                    }
                    Debug.Log("Jeden");
                    return randomQurik;
                }
                else{
                    Debug.Log("Dwa");
                    return randomQurik;
                }
            }
        }
        //return -1;
    }
}
