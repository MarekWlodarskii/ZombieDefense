using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MissionInfo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TextMeshProUGUI mission1;
    [SerializeField] public TextMeshProUGUI mission2;
    [SerializeField] public TextMeshProUGUI mission3;
    int[] quirks = new int[3];

    public void Awake(){

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MissionSelect.main.currentMission % 5 == 0){
                quirks = MissionSelect.main.rewardQuirks;
                mission1.text = $"Light\n\nReward: {TurretQuriks.main.possibleQuriks[quirks[0]]}";
                mission2.text = $"Medium\n\nReward: {TurretQuriks.main.possibleQuriks[quirks[1]]}";
                mission3.text = $"Heavy\n\nReward: {TurretQuriks.main.possibleQuriks[quirks[2]]}";
            return;
        }
        int[,] enemies = MissionSelect.main.missionEnemies;
        int[,,] upgrades = MissionSelect.main.missionUpgrades;
        mission1.text = $"Light: {enemies[0,0]}\n\nUpgrades:\nSpeed - {upgrades[0, 0, 0]}\nHP - {upgrades[0,0,1]}\nArmor - {upgrades[0,0,2]}\n\nMedium: {enemies[0,1]}\n\nUpgrades:\nSpeed - {upgrades[0,1, 0]}\nHP - {upgrades[0,1,1]}\nArmor - {upgrades[0,1,2]}\n\nHeavy: {enemies[0,2]}\n\nUpgrades:\nSpeed - {upgrades[0, 2, 0]}\nHP - {upgrades[0,2,1]}\nArmor - {upgrades[0,2,2]}";
        mission2.text = $"Light: {enemies[1,0]}\n\nUpgrades:\nSpeed - {upgrades[1, 0, 0]}\nHP - {upgrades[1,0,1]}\nArmor - {upgrades[1,0,2]}\n\nMedium: {enemies[1,1]}\n\nUpgrades:\nSpeed - {upgrades[1,1, 0]}\nHP - {upgrades[1,1,1]}\nArmor - {upgrades[1,1,2]}\n\nHeavy: {enemies[1,2]}\n\nUpgrades:\nSpeed - {upgrades[1, 2, 0]}\nHP - {upgrades[1,2,1]}\nArmor - {upgrades[1,2,2]}";
        mission3.text = $"Light: {enemies[2,0]}\n\nUpgrades:\nSpeed - {upgrades[2, 0, 0]}\nHP - {upgrades[2,0,1]}\nArmor - {upgrades[2,0,2]}\n\nMedium: {enemies[2,1]}\n\nUpgrades:\nSpeed - {upgrades[2,1, 0]}\nHP - {upgrades[2,1,1]}\nArmor - {upgrades[2,1,2]}\n\nHeavy: {enemies[2,2]}\n\nUpgrades:\nSpeed - {upgrades[2, 2, 0]}\nHP - {upgrades[2,2,1]}\nArmor - {upgrades[2,2,2]}";
    }


}
