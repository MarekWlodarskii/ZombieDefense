using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyTooltip : MonoBehaviour
{
    private float maxHealth;
    public void ShowHP(){
        gameObject.GetComponent<CanvasGroup>().alpha = 1;//gameObject.GetComponent<CanvasGroup>().alpha == 0 ? 1 : 0;
    }
    public void HideHP(){
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }
    public void UpdateHealth(float hp){
        gameObject.GetComponent<TextMeshProUGUI>().text = $"{hp} / {maxHealth}";
    }
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = gameObject.transform.parent.parent.GetComponent<EnemyData>().hp;
        gameObject.GetComponent<TextMeshProUGUI>().text = $"{maxHealth} / {maxHealth}";
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
