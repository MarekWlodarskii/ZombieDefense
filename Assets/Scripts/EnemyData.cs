using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.AI;

public class EnemyData : MonoBehaviour
{
    private float speed;
    public float hp;
    public float armor;
    private float[,] baselineStats = new float[3,3];

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    private Transform target;
    private int pathIndex = 0;

    public int enemyType;
    public bool isDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        target = Pathing.pathing.path[pathIndex];
    }
    void Awake(){
        gameObject.transform.parent.GetComponent<Animator>().SetBool("IsRunning", true);
        SetBaselineStats();
        SetStatistics(new int[]{0, 0, 0}, 1);
        isDestroyed = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(isDestroyed == true){
            StartCoroutine(DeathDelay());
        }
        if(Vector2.Distance(target.position, transform.position) <= 0.1f){
            pathIndex++;
            if(pathIndex >= Pathing.pathing.path.Length){
                Menu.main.GameLost();
            EnemySpawner.onEnemyDestroyed.Invoke();
            Destroy(gameObject.transform.parent.gameObject);
            return;
        }
        else{
            target = Pathing.pathing.path[pathIndex];
            
            if(pathIndex > 0 && Pathing.pathing.path[pathIndex-1].position.x < Pathing.pathing.path[pathIndex].position.x){
            gameObject.transform.localRotation = Quaternion.Euler(0f,180f,0f);
        }
        else{
            gameObject.transform.localRotation = Quaternion.Euler(0f,0f,0f);
        }
        }
        }
        
    }

    void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * speed;

    }

    public void SetStatistics(int[] stats, int enemyType){
        this.speed = baselineStats[enemyType, 0] + 0.125f * stats[0];
        this.hp = baselineStats[enemyType, 1] + 1 * stats[1];
        this.armor = stats[2];
        this.enemyType = enemyType;
    }
    private void SetBaselineStats(){
        // Light
        baselineStats[0, 0] = 0.5f;
        baselineStats[0, 1] = 10f;
        baselineStats[0, 2] = 0f;
        // Medium
        baselineStats[1, 0] = 0.25f;
        baselineStats[1, 1] = 20f;
        baselineStats[1, 2] = 0f;
        // Heavy
        baselineStats[2, 0] = 0.125f;
        baselineStats[2, 1] = 40f;
        baselineStats[2, 2] = 0f;
    }
    public void TakeDamage(int damage){
        hp -= Convert.ToInt32(Mathf.Floor(damage/(armor+1)));
        transform.GetChild(0).transform.GetChild(0).GetComponent<HealthBar>().UpdateHealth(hp);
        transform.GetChild(0).transform.GetChild(1).GetComponent<EnemyTooltip>().UpdateHealth(hp);
        if(hp <= 0 && isDestroyed == false){
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            speed = 0f;
            gameObject.layer = 0;
            Animator anim = transform.parent.gameObject.GetComponent<Animator>();
            int death = Animator.StringToHash("DeathTrigger");
            anim.SetTrigger(death);
            isDestroyed = true;
            StartCoroutine(DeathDelay());
            if(MissionSelect.main.currentMission % 5 == 0){
                EnemySpawner.main.totalEnemiesKilled[3] += 1;
            }
            else{
                EnemySpawner.main.totalEnemiesKilled[enemyType] += 1;
            }
            
            EnemySpawner.onEnemyDestroyed.Invoke();
            PlayerData.main.MoneyChange(50);
            
        }
    }

    private IEnumerator DeathDelay(){
        yield return new WaitForSeconds(1);
        Destroy(gameObject.transform.parent.gameObject);
    }
    public void OnMouseOver()
    {
        transform.GetChild(0).transform.GetChild(1).GetComponent<EnemyTooltip>().ShowHP();
    }
    public void OnMouseExit()
    {
        transform.GetChild(0).transform.GetChild(1).GetComponent<EnemyTooltip>().HideHP();
    }
}
