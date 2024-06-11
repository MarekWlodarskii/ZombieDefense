
using TMPro;

using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerData main;
    public int money;
    public int[] turretUpgrades;
    [SerializeField] private TextMeshProUGUI moneyText;
    public int towerCost;
    public int[] upgradeCost = new int[5];
    void Awake(){
        main = this;
        turretUpgrades = null;
        money = 2000;
        moneyText.text = $"Money: {money}";
        towerCost = 500;
        upgradeCost = new int[5]{100,200,400,800,1600};
    }
    public void MoneyChange(int amount){
        money += amount;
        moneyText.text = $"Money: {money}";
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
