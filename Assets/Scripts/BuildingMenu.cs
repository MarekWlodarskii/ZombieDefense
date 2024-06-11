using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
public class BuildingMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject menuButton;
    public static bool isPointerOverUI = false;
    public GameObject buildingArea;
    void Start()
    {
        if(buildingArea.GetComponent<buildingArea>().tower == null){
            foreach(string go in Building.main.buildingPrefabs[0].GetComponent<Turret>().towerTypes){
                GameObject button = Instantiate(menuButton, transform);
                button.GetComponentInChildren<TextMeshProUGUI>().text = go;
            }
            int panels = Convert.ToInt32((Mathf.Floor(Mathf.Sqrt(Building.main.buildingPrefabs[0].GetComponent<Turret>().towerTypes.Length))+1f))*100;
            gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(panels, panels);
        }
        else{
            //Debug.Log(buildingArea.GetComponent<buildingArea>().tower.name);
            if(Regex.IsMatch(buildingArea.GetComponent<buildingArea>().tower.name, @"^Tower.*")){
                int panels = Convert.ToInt32((Mathf.Floor(Mathf.Sqrt(Building.main.buildingPrefabs[0].GetComponent<Turret>().currentUpgrades.Length))+1f))*100;
                gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(panels, panels);
                int i = 0;
                foreach(String s in buildingArea.GetComponent<buildingArea>().tower.GetComponent<Turret>().possibleUpgrades){
                    GameObject button = Instantiate(menuButton, transform);
                    button.GetComponent<MenuButtonHandle>().tower = buildingArea.GetComponent<buildingArea>().tower;
                    Turret turret = buildingArea.GetComponent<buildingArea>().tower.GetComponent<Turret>();
                    button.GetComponentInChildren<TextMeshProUGUI>().text = s + " " + turret.currentUpgrades[i] + " / " + turret.maxUpgrades[i];
                    i++;
                }
                GameObject destroyTower = Instantiate(menuButton, transform);
                destroyTower.GetComponentInChildren<TextMeshProUGUI>().text = "Destroy";
                destroyTower.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                destroyTower.GetComponent<MenuButtonHandle>().tower = buildingArea.GetComponent<buildingArea>().tower;
            }
        }
    }
    
    
    private void Awake(){
        GameObject tooltip = GameObject.FindGameObjectWithTag("TooltipPanel");
        foreach(Transform child in tooltip.transform){
             Destroy(child.gameObject);
        } 
    }
    // Update is called once per frame
    void Update()
    {
        buildingArea.GetComponent<SpriteRenderer>().material = buildingArea.GetComponent<buildingArea>().mouseOver;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverUI = false;
    }
}
