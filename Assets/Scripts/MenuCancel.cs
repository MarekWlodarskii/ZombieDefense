using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class MenuCancel : MonoBehaviour, IPointerClickHandler
{
    public GameObject go;
    public void OnPointerClick(PointerEventData eventData){

        GameObject menu = GameObject.FindGameObjectWithTag("Mission");
        GameObject tooltip = GameObject.FindGameObjectWithTag("TooltipPanel");
        GameObject menuArea = GameObject.FindGameObjectWithTag("BuildingMenu");

        if(menuArea)
        menuArea.GetComponent<BuildingMenu>().buildingArea.GetComponent<SpriteRenderer>().material = menuArea.GetComponent<BuildingMenu>().buildingArea.GetComponent<buildingArea>().standardMaterial;

        foreach(Transform child in menu.transform){
            Destroy(child.gameObject);
        }

        foreach(Transform child in tooltip.transform){
             Destroy(child.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
