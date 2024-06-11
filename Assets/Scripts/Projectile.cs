using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float projectileSpeed = 5f;
    public int damage;
    [SerializeField] private Rigidbody2D rb;
    public string towerType;
    public void SetTarget(Transform target){
        this.target = target;
    }
    private void FixedUpdate(){
        if(!target || target.GetComponent<EnemyData>().hp <= 0){
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.GetComponent<EnemyData>().enemyType == 0)
        {
            if(towerType == "Vs Light Armor"){
                float temp = damage;
                damage = Convert.ToInt32(Mathf.Floor(temp * 2f));
            }
            if(towerType == "Vs Light Armor x2"){
                float temp = damage;
                damage = Convert.ToInt32(Mathf.Floor(temp * 4f));
            }
            if(towerType == "Vs Heavy Armor"){
                float temp = damage;
                damage = Convert.ToInt32(Mathf.Floor(temp * 0.5f));
            }
            if(towerType == "Vs Heavy Armor x2"){
                float temp = damage;
                damage = Convert.ToInt32(Mathf.Floor(temp * 0.5f)) * 1/2;
            }
            if(towerType == "Normal" && towerType == "Normal x2"){
                float temp = damage;
                damage = Convert.ToInt32(Mathf.Floor(temp * 0.75f));
            }
        }
        if(other.gameObject.GetComponent<EnemyData>().enemyType == 2)
        {
            if(towerType == "Vs Light Armor"){
                float temp = damage;
                damage = Convert.ToInt32(Mathf.Floor(temp * 0.5f));
            }
            if(towerType == "Vs Light Armor x2"){
                float temp = damage;
                damage = Convert.ToInt32(Mathf.Floor(temp * 0.5f)) * 1/2;
            }
            if(towerType == "Vs Heavy Armor"){
                float temp = damage;
                damage = Convert.ToInt32(Mathf.Floor(temp * 2f));
            }
            if(towerType == "Vs Heavy Armor"){
                float temp = damage;
                damage = Convert.ToInt32(Mathf.Floor(temp * 4f));
            }
            if(towerType == "Normal" || towerType == "Normal x2"){
                float temp = damage;
                damage = Convert.ToInt32(Mathf.Floor(temp * 0.75f));
            }
        }
        if(other.gameObject.GetComponent<EnemyData>().enemyType == 1)
        {
            if(towerType == "Vs Light Armor" || towerType == "Vs Light Armor x2"){
                float temp = damage;
                damage = Convert.ToInt32(Mathf.Floor(temp * 0.75f));
            }
            if(towerType == "Vs Heavy Armor" && towerType == "Vs Heavy Armor x2"){
                float temp = damage;
                damage = Convert.ToInt32(Mathf.Floor(temp * 0.75f));
            }
            if(towerType == "Normal" && towerType == "Normal x2"){
                float temp = damage;
                damage = Convert.ToInt32(Mathf.Floor(temp * 1.25f));
            }
        }
        //Debug.Log(damage);
        other.gameObject.GetComponent<EnemyData>().TakeDamage(damage);
        Destroy(gameObject);
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
