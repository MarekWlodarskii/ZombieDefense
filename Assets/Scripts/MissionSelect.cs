using UnityEngine;

public class MissionSelect : MonoBehaviour
{
    public static MissionSelect main;
    public int currentMission;
    public int[,] missionEnemies = new int[3,3];
    public int[,,] missionUpgrades = new int[3,3,3];
    private int maxUpgrades = 5;
    [SerializeField] public GameObject canvas;
    [SerializeField] public GameObject canvasMenu;
    public int[] rewardQuirks = new int[3];
    private bool isMenuActive;
    private void Awake(){
        isMenuActive = true;
        main = this;
        currentMission = 1;
        RandomEnemies();
        RandomUpgrades();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsMenuActive(){
        return isMenuActive;
    }

    public void RandomEnemies(){
        int amoutOfEnemies;
        for(int i = 0; i < 3; i++){
            amoutOfEnemies = currentMission * 10;
            for(int j = 0; j < 3; j++){
                if(j == 2){
                    missionEnemies[i, j] = amoutOfEnemies;
                    continue;
                }
                missionEnemies[i, j] = UnityEngine.Random.Range(0, amoutOfEnemies);
                amoutOfEnemies -= missionEnemies[i, j];
            }
        }
    }

    public void RandomUpgrades(){
        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 3; j++)
            {
                for(int k = 0; k < 3; k++){
                    missionUpgrades[i, j, k] = 0;
                }
            }
        }

        int amoutOfUpgrades;
        int enemyType;
        int enemyUpgrade;
        for(int k = 0; k < 3; k++){
            amoutOfUpgrades = currentMission;
        while(amoutOfUpgrades != 0){
            enemyType = UnityEngine.Random.Range(0, 3);
            enemyUpgrade = UnityEngine.Random.Range(0, 3);
            if(missionUpgrades[k, enemyType, enemyUpgrade] <= maxUpgrades){
                missionUpgrades[k, enemyType, enemyUpgrade] += 1;
                amoutOfUpgrades -= 1;
            }
        }}
    }

    public void ToggleMenu(){
        canvasMenu.GetComponent<CanvasGroup>().alpha = canvasMenu.GetComponent<CanvasGroup>().alpha > 0 ? 0 : 255;
        canvasMenu.GetComponent<CanvasGroup>().interactable = !canvasMenu.GetComponent<CanvasGroup>().interactable;
        canvasMenu.GetComponent<CanvasGroup>().blocksRaycasts = !canvasMenu.GetComponent<CanvasGroup>().blocksRaycasts;
        isMenuActive = !isMenuActive;
    }

    public void ToggleCanvas(){
        canvas.GetComponent<CanvasGroup>().alpha = canvas.GetComponent<CanvasGroup>().alpha > 0 ? 0 : 255;
        canvas.GetComponent<CanvasGroup>().interactable = !canvas.GetComponent<CanvasGroup>().interactable;
        canvas.GetComponent<CanvasGroup>().blocksRaycasts = !canvas.GetComponent<CanvasGroup>().blocksRaycasts;
        isMenuActive = !isMenuActive;
    }
    public int GetMissionInfo(int missionNumber, int enemyType){
        return missionEnemies[missionNumber, enemyType];
    }
    public void SelectMission(int missionNumber){
        isMenuActive = false;
        ToggleCanvas();
        ToggleMenu();
        int[,] selectedMissionUpgrades = new int [3,3];
        if(currentMission % 5 == 0){
            
            for(int i = 0; i < 3; i++){
            for(int j = 0; j < 3; j++){
                selectedMissionUpgrades[i,j] = -1;
            }
        }
            EnemySpawner.main.SetWaveData(missionNumber == 0 ? 1 : 0, missionNumber == 1 ? 1 : 0, missionNumber == 2 ? 1 : 0, selectedMissionUpgrades);
            return;
        }
        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 3; j++){
                selectedMissionUpgrades[i,j] = missionUpgrades[missionNumber, i, j];
            }
        }
        EnemySpawner.main.SetWaveData(missionEnemies[missionNumber, 0], missionEnemies[missionNumber, 1], missionEnemies[missionNumber, 2], selectedMissionUpgrades);
    }

    public void RandomBoss(){

        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 3; j++){
                missionEnemies[i, j] = 0;
            }
        }
        rewardQuirks[0] = TurretQuriks.main.RandQuirk(-1, -1);
        rewardQuirks[1] = TurretQuriks.main.RandQuirk(rewardQuirks[0], -1);
        rewardQuirks[2] = TurretQuriks.main.RandQuirk(rewardQuirks[0], rewardQuirks[1]);
    }
    public void MissionDone(){
        isMenuActive = true;
        ToggleCanvas();
        ToggleMenu();
        currentMission++;
        if(currentMission % 5 == 0){
            RandomBoss();
        }
        else{
            RandomEnemies();
            RandomUpgrades();
        }
    }


}
