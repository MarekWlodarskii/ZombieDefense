using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
public class MenuButtonHandle : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public GameObject tower = null;
    private GameObject tooltip;
    [SerializeField] GameObject tooltipPrefab;
    [SerializeField] private Texture2D cursor;
    [SerializeField] private Material buildableMaterial;
    [SerializeField] private Sprite buildableSprite;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(gameObject.GetComponentInParent<BuildingMenu>().buildingArea.GetComponent<buildingArea>().tower == null){
            if(PlayerData.main.money < PlayerData.main.towerCost)
                return;
            gameObject.GetComponentInParent<BuildingMenu>().buildingArea.GetComponent<SpriteRenderer>().material = null;
            gameObject.GetComponentInParent<BuildingMenu>().buildingArea.GetComponent<SpriteRenderer>().sprite = null;
            GameObject towerToBuild = Building.main.GetSelectedTower();
            tower = Instantiate(towerToBuild, gameObject.GetComponentInParent<BuildingMenu>().buildingArea.transform.position, UnityEngine.Quaternion.identity, GameObject.FindGameObjectWithTag("TurretGroup").transform);
            gameObject.GetComponentInParent<BuildingMenu>().buildingArea.GetComponent<buildingArea>().tower = tower;
            BuildingMenu.isPointerOverUI = false;
            PlayerData.main.MoneyChange(-PlayerData.main.towerCost);
            tower.GetComponent<Turret>().ChangeTowerType(gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
            Debug.Log(gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
            Destroy(tooltip);
            Destroy(transform.parent.gameObject);
        }
        else{
            if(Regex.IsMatch(gameObject.GetComponentInChildren<TextMeshProUGUI>().text, @"^Reload Time.*")){
                if(PlayerData.main.money < PlayerData.main.upgradeCost[tower.GetComponent<Turret>().currentUpgrades[1]])
                    return;
                if(tower.GetComponent<Turret>().currentUpgrades[1] < tower.GetComponent<Turret>().maxUpgrades[1]){
                    PlayerData.main.MoneyChange(-PlayerData.main.upgradeCost[tower.GetComponent<Turret>().currentUpgrades[1]]);
                    tower.GetComponent<Turret>().UpgradeTurret(1);
                    gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Reload Time " + tower.GetComponent<Turret>().currentUpgrades[1] + " / " + tower.GetComponent<Turret>().maxUpgrades[1];
                }
            }
            if(Regex.IsMatch(gameObject.GetComponentInChildren<TextMeshProUGUI>().text, @"^Damage.*")){
                if(PlayerData.main.money < PlayerData.main.upgradeCost[tower.GetComponent<Turret>().currentUpgrades[2]])
                    return;
                if(tower.GetComponent<Turret>().currentUpgrades[2] < tower.GetComponent<Turret>().maxUpgrades[2]){
                    PlayerData.main.MoneyChange(-PlayerData.main.upgradeCost[tower.GetComponent<Turret>().currentUpgrades[2]]);
                    tower.GetComponent<Turret>().UpgradeTurret(2);
                    gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Damage " + tower.GetComponent<Turret>().currentUpgrades[2] + " / " + tower.GetComponent<Turret>().maxUpgrades[2];
                }
            }
            if(Regex.IsMatch(gameObject.GetComponentInChildren<TextMeshProUGUI>().text, @"^Range.*")){
                if(PlayerData.main.money < PlayerData.main.upgradeCost[tower.GetComponent<Turret>().currentUpgrades[0]])
                    return;
                if(tower.GetComponent<Turret>().currentUpgrades[0] < tower.GetComponent<Turret>().maxUpgrades[0]){
                    PlayerData.main.MoneyChange(-PlayerData.main.upgradeCost[tower.GetComponent<Turret>().currentUpgrades[0]]);
                    tower.GetComponent<Turret>().UpgradeTurret(0);
                    gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Range " + tower.GetComponent<Turret>().currentUpgrades[0] + " / " + tower.GetComponent<Turret>().maxUpgrades[0];
                }
            }
            if(gameObject.GetComponentInChildren<TextMeshProUGUI>().text == "Destroy"){
                gameObject.GetComponentInParent<BuildingMenu>().buildingArea.GetComponent<SpriteRenderer>().material = buildableMaterial;
            gameObject.GetComponentInParent<BuildingMenu>().buildingArea.GetComponent<SpriteRenderer>().sprite = buildableSprite;
                tower.GetComponent<Turret>().DestroyTurret();
                Destroy(tower);
                BuildingMenu.isPointerOverUI = false;
                GameObject go = GameObject.FindGameObjectWithTag("MenuCancel");
                go.GetComponent<MenuCancel>().OnPointerClick(new PointerEventData(EventSystem.current));
                
                return;
                
            }
            OnPointerExit(new PointerEventData(EventSystem.current));
            OnPointerEnter(new PointerEventData(EventSystem.current));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RectTransform spawnAtObject = tooltipPrefab.GetComponent<RectTransform>();//gameObject.GetComponentInParent<BuildingMenu>().gameObject.GetComponent<RectTransform>();
        Vector2 size = spawnAtObject.rect.size;
        Vector2 spawnPoint = (Vector2)gameObject.GetComponentInParent<BuildingMenu>().gameObject.transform.position;// + new Vector2(size.x, size.y);
        Cursor.SetCursor(cursor, UnityEngine.Vector2.zero, CursorMode.Auto);
        gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
        gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0f, 0f, 0f, 1f);

        tooltip = Instantiate(tooltipPrefab, spawnPoint, UnityEngine.Quaternion.identity, GameObject.FindGameObjectWithTag("Mission").transform);
        if(gameObject.GetComponentInParent<BuildingMenu>().buildingArea.GetComponent<buildingArea>().tower == null){
            tooltip.GetComponentInChildren<TextMeshProUGUI>().text = $"{PlayerData.main.money} / {PlayerData.main.towerCost}";
            tooltip.GetComponentInChildren<TextMeshProUGUI>().color = PlayerData.main.towerCost > PlayerData.main.money ? Color.red : Color.green;
        }
        else{
            tooltip.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
            if(Regex.IsMatch(gameObject.GetComponentInChildren<TextMeshProUGUI>().text, @"^Reload Time.*")){
                tooltip.GetComponentInChildren<TextMeshProUGUI>().text = tower.GetComponent<Turret>().currentUpgrades[1] >= 5 ? "Max Upgrade" : $"{PlayerData.main.money} / {PlayerData.main.upgradeCost[tower.GetComponent<Turret>().currentUpgrades[1]]}";
                if(tower.GetComponent<Turret>().currentUpgrades[1] < 5)
                    tooltip.GetComponentInChildren<TextMeshProUGUI>().color = PlayerData.main.upgradeCost[tower.GetComponent<Turret>().currentUpgrades[1]] > PlayerData.main.money ? Color.red : Color.green;
            }
            if(Regex.IsMatch(gameObject.GetComponentInChildren<TextMeshProUGUI>().text, @"^Damage.*")){
                tooltip.GetComponentInChildren<TextMeshProUGUI>().text = tower.GetComponent<Turret>().currentUpgrades[2] >= 5 ? "Max Upgrade" : $"{PlayerData.main.money} / {PlayerData.main.upgradeCost[tower.GetComponent<Turret>().currentUpgrades[2]]}";
                if(tower.GetComponent<Turret>().currentUpgrades[2] < 5)
                    tooltip.GetComponentInChildren<TextMeshProUGUI>().color = PlayerData.main.upgradeCost[tower.GetComponent<Turret>().currentUpgrades[2]] > PlayerData.main.money ? Color.red : Color.green;
            }
            if(Regex.IsMatch(gameObject.GetComponentInChildren<TextMeshProUGUI>().text, @"^Range.*")){
                tooltip.GetComponentInChildren<TextMeshProUGUI>().text = tower.GetComponent<Turret>().currentUpgrades[0] >= 5 ? "Max Upgrade" : $"{PlayerData.main.money} / {PlayerData.main.upgradeCost[tower.GetComponent<Turret>().currentUpgrades[0]]}";
                if(tower.GetComponent<Turret>().currentUpgrades[0] < 5)
                    tooltip.GetComponentInChildren<TextMeshProUGUI>().color = PlayerData.main.upgradeCost[tower.GetComponent<Turret>().currentUpgrades[0]] > PlayerData.main.money ? Color.red : Color.green;
            }
            if(Regex.IsMatch(gameObject.GetComponentInChildren<TextMeshProUGUI>().text, @"^Destroy")){
                
                tooltip.GetComponentInChildren<TextMeshProUGUI>().text = "+" + tower.GetComponent<Turret>().CalculateDestroyMoney().ToString();
                tooltip.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0f, 0f, 1f);
        gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 1f);
        Cursor.SetCursor(null, UnityEngine.Vector2.zero, CursorMode.Auto);
        Destroy(tooltip);
    }

    
}
