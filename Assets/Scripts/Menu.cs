using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public static Menu main;
    public bool isGameLost;
    [SerializeField] private GameObject gameLost;
    void Start()
    {
        main = this;
        isGameLost = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameLost == true){
            isGameLost = false;
            GameObject go = Instantiate(gameLost);
            go.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<EnemiesKilled>().enemiesKilled = EnemySpawner.main.totalEnemiesKilled;
            go.transform.SetParent(null);
            Destroy(GameObject.FindGameObjectWithTag("Game").gameObject);
        }
    }

    public void GameLost(){
        isGameLost = true;
    }
}
