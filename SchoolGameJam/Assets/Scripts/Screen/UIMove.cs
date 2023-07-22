using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIMove : MonoBehaviour
{
    [SerializeField] Transform bg;
    [SerializeField] float[] bgMovePos;

    [SerializeField] Transform ui;
    [SerializeField] float[] uiMovePos;

    [SerializeField] Transform controlKeyPos;
    [SerializeField] Transform controlKey;

    Coroutine coroutine;
    Coroutine coroutine2;
    Coroutine coroutine3;
    
    bool isScreenMove = true;
    bool isUIMoving = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeScreen();
    }

    void ChangeScreen()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            isUIMoving = true;
            isScreenMove = !isScreenMove;
            if(coroutine != null) StopCoroutine(coroutine);
            if(coroutine2 != null) StopCoroutine(coroutine2);
            //if (coroutine3 != null) StopCoroutine(coroutine3);
            // bg.transform.DOMoveY(bgMovePos[(isScreenMove) ? 0 : 1], 1);
            //ui.transform.DOMoveY(uiMovePos[(isScreenMove) ? 0 : 1], 1f);
            //coroutine3 = StartCoroutine(IIconSet(1f));
            coroutine2 = StartCoroutine(IRectSet(1f));
            coroutine = StartCoroutine(IBGSet(1f));
            
            if(!isScreenMove) controlKey.gameObject.SetActive(false);    
        }
    }

    IEnumerator IIconSet(float time)
    {
        yield return new WaitForSeconds(time);
        controlKey.localPosition = new Vector2(controlKey.localPosition.x, controlKeyPos.position.y);
        controlKey.gameObject.SetActive(true);
    }

    IEnumerator IRectSet(float time)
    {
        float t = 0f;

        while(t < 1)
        {
            t += Time.deltaTime / time;
            RectTransform rectTransform = ui.GetComponent<RectTransform>();
            rectTransform.offsetMax = Vector2.Lerp(new Vector2(rectTransform.offsetMax.x, rectTransform.offsetMax.y), new Vector2(rectTransform.offsetMax.x, uiMovePos[(isScreenMove) ? 0 : 1]), t);
            if(Vector2.Distance(new Vector2(rectTransform.offsetMax.x, rectTransform.offsetMax.y), new Vector2(rectTransform.offsetMax.x, uiMovePos[(isScreenMove) ? 0 : 1])) < 0.5f && isScreenMove) {
                controlKey.localPosition = new Vector2(controlKey.localPosition.x, controlKeyPos.position.y);
                controlKey.gameObject.SetActive(true);
            }
            yield return null;
        }
        isUIMoving = false;
        //controlKey.localPosition = new Vector2(controlKey.localPosition.x, controlKeyPos.position.y);
        //if(isScreenMove)
            //controlKey.gameObject.SetActive(true);

    }

    IEnumerator IBGSet(float time)
    {
        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / time;
            //RectTransform rectTransform = ui.GetComponent<RectTransform>();
            bg.position = Vector2.Lerp(bg.position, new Vector2(bg.position.x, bgMovePos[(isScreenMove) ? 0 : 1]), t);
            yield return null;
        }
       

    }

    IEnumerator IControlKeySet()
    {
        controlKey.localPosition = new Vector2(controlKey.localPosition.x, controlKeyPos.position.y);
        yield return new WaitForSeconds(1);
        controlKey.gameObject.SetActive(true);
    }
    
}
