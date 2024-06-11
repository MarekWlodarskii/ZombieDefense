using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResumeGame : MonoBehaviour, IPointerClickHandler
{
    public GameObject menu;

    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = 1f;
        menu.SetActive(true);
        ControlKeys.main.isMenuOpen = false;
        Destroy(GameObject.FindGameObjectWithTag("Menu").gameObject);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Time.timeScale = 1f;
        menu.SetActive(true);
        ControlKeys.main.isMenuOpen = false;
        Destroy(GameObject.FindGameObjectWithTag("Menu").gameObject);
        }
    }
}
