using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner main;
    [Header("References")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Attributes")]
    private Stack<int> wave = new Stack<int>();
    private int[,] enemyInfo = new int[3, 3]; // Speed HP Armor
    private Color[] color = new Color[3];
    private float timeSinceLastSpawn = 0f;
    private bool isSpawning = false;
    public static UnityEvent onEnemyDestroyed = new UnityEvent();
    private int enemyTotal;
    private int enemyKilled = 0;
    private float timeBeforeWave = 3f;
    private bool nextMission = false;
    public int boss;

    public int[] totalEnemiesKilled = new int[4];
    // Start is called before the first frame update
    void Start()
    {
        totalEnemiesKilled = new int[]{0, 0, 0, 0};
    }

    // Update is called once per frame
    void Update()
    {
        
        if(nextMission == true){
            nextMission = false;
            MissionSelect.main.MissionDone();
        }

        if(!isSpawning)
            return;

        timeSinceLastSpawn += Time.deltaTime; 

        if(enemyKilled >= enemyTotal && enemyKilled > 0){
            enemyKilled = 0;
            isSpawning = false;
            if(MissionSelect.main.currentMission % 5 == 0){
                TurretQuriks.main.currentQuriks.Add(MissionSelect.main.rewardQuirks[boss]);
            }
            StartCoroutine(NextMission());
        }       

        if(timeSinceLastSpawn >= 1f/MissionSelect.main.currentMission && wave.Count > 0){
            int enemy = wave.Pop();
            SpawnEnemy(enemy);
            timeSinceLastSpawn = 0f;
        }
    }

    void Awake(){
        main = this;
        onEnemyDestroyed.AddListener(EnemyDestroyed);
    }

    private IEnumerator StartWave(){
        yield return new WaitForSeconds(timeBeforeWave);
        isSpawning = true;
    }

    private IEnumerator NextMission(){
        yield return new WaitForSeconds(timeBeforeWave);
        nextMission = true;
    }
    private void EnemyDestroyed(){
        enemyKilled++;
    }
    // Do optymalizacji
    public void SetWaveData(int light, int medium, int heavy, int[,] upgrades){ 
        
        if(upgrades[0,0] == -1){
            enemyTotal = 1;
            wave.Push(light == 0 ? medium == 0 ? 2 : 1 : 0);
            Debug.Log(light == 0 ? medium == 0 ? 2 : 1 : 0);
            StartCoroutine(StartWave());
            return;
        }

        enemyTotal = light + medium + heavy;

        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 3; j++){
                    enemyInfo[i,j] = upgrades[i, j];
            }
        }

        color[0] = Color.green;
        color[1] = Color.blue;
        color[2] = Color.red;

        while(light+medium+heavy>0){
            int enemyType = UnityEngine.Random.Range(0, 3);
            if(light > 0 && enemyType == 0){
                wave.Push(0);
                light--;
            }
            if(medium > 0 && enemyType == 1){
                wave.Push(1);
                medium--;
            }
            if(heavy > 0 && enemyType == 2){
                wave.Push(2);
                heavy--;
            }
        }

        StartCoroutine(StartWave());
    }
    private void SpawnEnemy(int enemy){
        GameObject prefabToSpawn = Instantiate(enemyPrefab, Pathing.pathing.startingPoint.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Enemies").transform) as GameObject;
        if(MissionSelect.main.currentMission % 5 == 0){
                prefabToSpawn.transform.GetChild(0).GetComponent<EnemyData>().SetStatistics(new int[3]{1,2,3}, enemy);
                prefabToSpawn.transform.GetChild(0).GetComponent<EnemyData>().hp *= MissionSelect.main.currentMission;
                prefabToSpawn.transform.GetChild(0).GetComponent<EnemyData>().armor += MissionSelect.main.currentMission;
                prefabToSpawn.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
                prefabToSpawn.transform.GetChild(0).transform.GetChild(4).GetComponent<SpriteRenderer>().color = color[enemy];
                boss = enemy;
                return;
        }
        int[] stats = new int[3];

        for(int i = 0; i < 3; i++){
            stats[i] = enemyInfo[enemy,i];
        }

        prefabToSpawn.transform.GetChild(0).transform.GetComponent<EnemyData>().SetStatistics(stats, enemy);

        prefabToSpawn.transform.GetChild(0).transform.GetChild(4).GetComponent<SpriteRenderer>().color = color[enemy];
    }
}
