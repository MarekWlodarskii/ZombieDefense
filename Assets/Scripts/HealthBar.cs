using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHealth;
    public void UpdateHealth(float hp){
        gameObject.GetComponent<Slider>().value = hp/maxHealth;
    }
    void Start()
    {
        maxHealth = gameObject.transform.parent.parent.GetComponent<EnemyData>().hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
