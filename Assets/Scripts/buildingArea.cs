using TMPro;
using UnityEngine;


public class buildingArea : MonoBehaviour
{
    public GameObject tower = null;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor = Color.grey;
    [SerializeField] private GameObject buildingMenu;
    [SerializeField] private GameObject buildingButton;
    [SerializeField] private GameObject[] buildArea;
    [SerializeField] private Texture2D cursor;
    [SerializeField] private GameObject tooltip;
    [SerializeField] public Material mouseOver;
    [SerializeField] public Material standardMaterial;
    private Color color;
    private GameObject menu;
    private void OnMouseEnter(){
        if (BuildingMenu.isPointerOverUI == true || !Interactable())
        {
            sr.color = color;
            return;
        }
        gameObject.GetComponent<SpriteRenderer>().material = mouseOver;
        Cursor.SetCursor(cursor, UnityEngine.Vector2.zero, CursorMode.Auto);
        sr.color = hoverColor;
    }
    private void OnMouseOver(){
        if (BuildingMenu.isPointerOverUI == true || !Interactable())
        {
            sr.color = color;
            return;
        }
        sr.color = hoverColor;
        if(Input.GetMouseButton(1)){
            if(tower != null && GameObject.FindGameObjectWithTag("TooltipPanel").transform.childCount == 0){
                //Debug.Log("Jestem");
                GameObject tooltipActive = Instantiate(tooltip, Input.mousePosition, UnityEngine.Quaternion.identity, GameObject.FindGameObjectWithTag("TooltipPanel").transform);
                    string towerType = "Normal Turret";
                    string vsLight = "x 0.75";
                    string vsNormal = "x 1.25";
                    string vsHeavy = "x 0.75";
                Debug.Log(tower.GetComponent<Turret>().towerType);
                if(tower.GetComponent<Turret>().towerType == "Vs Light Armor"){
                    towerType = "Light Turret";
                    vsLight = "x 2";
                    vsNormal = "x 0.75";
                    vsHeavy = "x 0.5";
                }
                if(tower.GetComponent<Turret>().towerType == "Vs Heavy Armor"){
                    towerType = "Heavy Turret";
                    vsLight = "x 0.5";
                    vsNormal = "x 0.75";
                    vsHeavy = "x 2";
                }
                if(tower.GetComponent<Turret>().towerType == "Vs Heavy Armor x2"){
                    towerType = "Heavy Turret x2";
                    vsLight = "x 0.25";
                    vsNormal = "x 0.75";
                    vsHeavy = "x 4";
                }
                if(tower.GetComponent<Turret>().towerType == "Vs Light Armor x2"){
                    towerType = "Light Turret x2";
                    vsLight = "x 4";
                    vsNormal = "x 0.75";
                    vsHeavy = "x 0.25";
                }
                if(tower.GetComponent<Turret>().towerType == "x1"){
                    towerType = "Turret";
                    vsLight = "x 1";
                    vsNormal = "x 1";
                    vsHeavy = "x 1";
                }
                int damageBottom = tower.GetComponent<Turret>().damageBottom;
                int DamageTop= tower.GetComponent<Turret>().DamageTop;

                tooltipActive.GetComponentInChildren<TextMeshProUGUI>().text = $"Tower Type: {towerType}\nDamage: {damageBottom} - {DamageTop}\nModifier vs Light: {vsLight}\nModifier vs Normal: {vsNormal}\nModifier vs Heavy: {vsHeavy}";
                tooltipActive.GetComponentInChildren<TextMeshProUGUI>().text += $"\nQuirks:";
                foreach(int quirk in tower.GetComponent<Turret>().quirks){
                    tooltipActive.GetComponentInChildren<TextMeshProUGUI>().text += $"\n{TurretQuriks.main.possibleQuriks[quirk]}";
                }
            }
        }
    }
    private void OnMouseExit(){
        gameObject.GetComponent<SpriteRenderer>().material = standardMaterial;
        if (BuildingMenu.isPointerOverUI == true || !Interactable())
        {
            sr.color = color;
            return;
        }
        Cursor.SetCursor(null, UnityEngine.Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseDown(){
        sr.color = color;
        if(!Interactable())
            return;
        
        if (BuildingMenu.isPointerOverUI == true)
        {
            return;
        }
        


        GameObject currentMenu = GameObject.FindGameObjectWithTag("Mission");

        if(currentMenu.transform.childCount > 0){
            currentMenu.transform.GetChild(0).GetComponent<BuildingMenu>().buildingArea.GetComponent<SpriteRenderer>().material = currentMenu.transform.GetChild(0).GetComponent<BuildingMenu>().buildingArea.GetComponent<buildingArea>().standardMaterial;
            foreach(Transform child in currentMenu.transform){
                Destroy(child.gameObject);
            }
        }
        menu = Instantiate(buildingMenu, Input.mousePosition, UnityEngine.Quaternion.identity, GameObject.FindGameObjectWithTag("Mission").transform);
        menu.GetComponent<BuildingMenu>().buildingArea = gameObject;
    }
    private bool Interactable(){
        return !MissionSelect.main.IsMenuActive();
    }
    void Start()
    {
        color = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
