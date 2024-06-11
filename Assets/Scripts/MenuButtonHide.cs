using UnityEngine;
using UnityEngine.EventSystems;


public class MenuButtonHide : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    [SerializeField] Texture2D cursor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color c = transform.GetComponent<UnityEngine.UI.Image>().color;
        transform.GetComponent<UnityEngine.UI.Image>().color = new Color(c.r, c.g, c.b, c.a + 0.05f);
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        //Cursor.
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color c = transform.GetComponent<UnityEngine.UI.Image>().color;
        transform.GetComponent<UnityEngine.UI.Image>().color = new Color(c.r, c.g, c.b, c.a - 0.05f);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
