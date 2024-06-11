using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartGame : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject game;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(Time.timeScale == 0f)
            transform.parent.transform.GetChild(0).gameObject.GetComponent<ResumeGame>().OnPointerClick(new PointerEventData(EventSystem.current));
        Destroy(GameObject.FindGameObjectWithTag("Game"));
        Instantiate(game);
        //game.transform.SetParent(null);
        Cursor.SetCursor(null, UnityEngine.Vector2.zero, CursorMode.Auto);
        Destroy(GameObject.FindGameObjectWithTag("Menu").gameObject);
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
