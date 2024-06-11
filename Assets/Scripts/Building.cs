using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Building : MonoBehaviour
{
    public static Building main;
    
    [SerializeField] public GameObject[] buildingPrefabs;

    private void Awake(){
        main = this;
    }

    public GameObject GetSelectedTower(){
        return buildingPrefabs[0];
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
