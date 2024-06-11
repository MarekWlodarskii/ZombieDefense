using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color c;
    private float duration;
    private Coroutine transitionCoroutine;
    void Start()
    {
        c = gameObject.GetComponent<UnityEngine.UI.Image>().color;
        duration = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }
        transitionCoroutine = StartCoroutine(Transition(true));
        //Color c = gameObject.GetComponent<UnityEngine.UI.Image>().color;
        //gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(c.r, c.g, c.b, 0.7f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }
        transitionCoroutine = StartCoroutine(Transition(false));
    }

    private IEnumerator Transition(bool temp){
        float time = 0f;
        float x;
        if(temp == true){
            x = 0f;
        }
        else{
            x = duration;
        }
        while(time <= duration){
            time += Time.unscaledDeltaTime;
            gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(c.r, c.g, c.b, 0.7f*(Mathf.Abs(time-x)/duration));
            yield return null;
        }
    }
}
