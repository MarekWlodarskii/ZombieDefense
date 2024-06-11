using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlKeys : MonoBehaviour
{
    // Start is called before the first frame update
    public static ControlKeys main;
    private GameObject[] enemies;
    private bool showHPBars;
    [SerializeField] GameObject pauseMenu;
    public bool isMenuOpen;
    void Start()
    {
        main = this;
        showHPBars = false;
        isMenuOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(Input.GetKeyDown(KeyCode.LeftAlt)){
            showHPBars = !showHPBars;
        }
            foreach(GameObject go in enemies){
                CanvasGroup canvas = go.transform.GetChild(0).transform.GetChild(0).GetComponent<CanvasGroup>();
                canvas.alpha = showHPBars == true ? 1 : 0;
                //canvas.GetComponent<CanvasGroup>().blocksRaycasts = !canvas.GetComponent<CanvasGroup>().blocksRaycasts;
            
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isMenuOpen){
                GameObject.FindGameObjectWithTag("Game").SetActive(true);
                isMenuOpen = false;
                Time.timeScale = 1f;
                Destroy(GameObject.FindGameObjectWithTag("Menu").gameObject);
                return;
            }

            GameObject pm = Instantiate(pauseMenu);
            pm.transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).GetComponent<ResumeGame>().menu = GameObject.FindGameObjectWithTag("Game");
            pm.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<EnemiesKilled>().enemiesKilled = EnemySpawner.main.totalEnemiesKilled;
            pm.transform.SetParent(null);
            Time.timeScale = 0f;
            isMenuOpen = true;
            GameObject.FindGameObjectWithTag("Game").SetActive(false);
        }
    }
}
