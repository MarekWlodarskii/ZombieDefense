using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.Security.Cryptography;
using System.Drawing;
using System;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.EventSystems;
public class Turret : MonoBehaviour
{
    public float range;
    public float firePerSecond;
    public int damageBottom;
    public int DamageTop;
    private float reloadTime;
    public string[] possibleUpgrades = {"Range", "Reload Time", "Damage"};
    public int[] currentUpgrades = {0, 0, 0};
    public int[] maxUpgrades = {5, 5, 5};
    private Transform turretRotation;
    private Transform target;
    public string towerType = null;
    public string[] towerTypes = new string[]{"Vs Light Armor", "Normal", "Vs Heavy Armor"};
    public List<int> quirks = new List<int>();
    public bool krytyk;
    public bool doubleShot;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject tooltip;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    // Start is called before the first frame update
    private void OnDrawGizmosSelected()
    {
    #if UNITY_EDITOR
        UnityEditor.Handles.color = UnityEngine.Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, range);
    #endif
    }

    private void FindTarget(){
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, enemyMask);
        if(hits.Length > 0 && hits[0].transform.GetComponent<EnemyData>().isDestroyed == false){
            target = hits[0].transform;
            //Debug.Log("Jest");
        }
    }
    void Start()
    {
        damageBottom = 5;
        DamageTop = 7;
        range = 2f;
        firePerSecond = 1f;
        krytyk = false;
        doubleShot = false;
    }

    public void OnMouseExit(){
        Destroy(tooltip);
    }

    public void DestroyTurret(){
        PlayerData.main.MoneyChange(CalculateDestroyMoney());
    }

    public int CalculateDestroyMoney(){
        int money = 0;
        for(int i = 0; i < possibleUpgrades.Length; i++){
            while(currentUpgrades[i] > 0){
                money += PlayerData.main.upgradeCost[currentUpgrades[i]-1]/2;
                currentUpgrades[i] -= 1;
            }
        }
        return money+250;
    }
    public void UpgradeTurret(int upgradeType){
        currentUpgrades[upgradeType] += 1;
        if(upgradeType == 0){
            range *= 1.25f;
        }
        if(upgradeType == 1){
            firePerSecond *= 1.25f;
        }
        if(upgradeType == 2){
            damageBottom = Convert.ToInt32(1.2f * damageBottom);
            DamageTop = Convert.ToInt32(1.4f * DamageTop);
        }
        int upgrades = currentUpgrades[0] + currentUpgrades[1] + currentUpgrades[2];
        if(upgrades >= 5){
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
            if(upgrades >= 10){
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
        }
    }

    public void ChangeTowerType(string type){
        towerType = type;
        if(type == "Vs Light Armor"){
            Debug.Log("Light");
            gameObject.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(0.2666667f,0.6235294f,0.2767864f);
        }
        if(type == "Vs Heavy Armor"){
            gameObject.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(0.6226415f,0.2672659f,0.2672659f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        reloadTime += Time.deltaTime;
        if(target == null || target.GetComponent<EnemyData>().isDestroyed == true){
            FindTarget();
            return;
        }
        //RotateTower();

        if(!CheckIfTargetIsInRange()){
            target = null;
        }
        else{
            if(reloadTime >= 1f/firePerSecond){
                Shoot();
                if(doubleShot == true){
                    int ds = UnityEngine.Random.Range(0, 4);
                    if(ds == 3)
                    {
                        Shoot();
                    }
                }
                reloadTime = 0;
            }
        }
    }

    private void Shoot(){
        source.PlayOneShot(clip);
        Debug.Log("Shoot");
        GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        int damage = UnityEngine.Random.Range(damageBottom,DamageTop + 1);
        if(krytyk == true){
                    int kryt = UnityEngine.Random.Range(0, 4);
                    if(kryt == 3)
                    {
                        damage *= 2;
                    }
                }
        bullet.GetComponent<Projectile>().damage = damage;
        bullet.SendMessage("SetTarget", target);
    }
    private bool CheckIfTargetIsInRange(){
        return Vector2.Distance(target.position, transform.position) <= range;
    }

    private void RotateTower(){
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = targetRotation;
    }

    
}
