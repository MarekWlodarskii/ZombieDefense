using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemiesKilled : MonoBehaviour
{
    // Start is called before the first frame update
    public int[] enemiesKilled = new int[4];
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Light Enemies Killed: {enemiesKilled[0]}";
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Normal Enemies Killed: {enemiesKilled[1]}";
        gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"Heavy Enemies Killed: {enemiesKilled[2]}";
        gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = $"Bosses Killed: {enemiesKilled[3]}";
    }
}
