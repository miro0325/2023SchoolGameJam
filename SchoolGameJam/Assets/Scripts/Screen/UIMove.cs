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

    [SerializeField] Transform HousePos;
    [SerializeField] Transform house;

    Coroutine coroutine;
    Coroutine coroutine2;
    Coroutine coroutine3;
    
    bool isScreenMove = true;
    bool isUIMoving = false;
    Sequence finalSequence;

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
            //if(coroutine != null) StopCoroutine(coroutine);
            ui.DOMoveY(uiMovePos[(isScreenMove) ? 0 : 1], 1f);

            if (isScreenMove)
            {
                //coroutine = StartCoroutine(IIconSet(0));


            }
            else
            {
                //controlKey.gameObject.SetActive(false);
                //ui.DOMoveY(uiMovePos[(isScreenMove) ? 0 : 1], 1f);

            }






            //if (!isScreenMove)
            //{
            //    for(int i = 0; i < controlKey.childCount; i++)
            //        controlKey.GetChild(i).GetComponent<Image>().DOFade(0, 0.1f); //controlKey.gameObject.SetActive(false);  
            //}
            //else
            //{
            //    for (int i = 0; i < controlKey.childCount; i++)
            //        controlKey.GetChild(i).GetComponent<Image>().DOFade(1, 1f);

            //}

        }
    }

    IEnumerator IIconSet(float time)
    {
        var tween = ui.DOMoveY(700, 0.0f);
        yield return tween.WaitForCompletion();
        //controlKey.gameObject.SetActive(true);

    }

    IEnumerator IRectSet(float time)
    {
        float t = 0f;

        while(t < 1)
        {
            t += Time.deltaTime / time;
            RectTransform rectTransform = ui.GetComponent<RectTransform>();
            //Debug.Log((1080 + uiMovePos[(isScreenMove) ? 0 : 1]) / 1080);
            float screenValue = (1080 + rectTransform.offsetMax.y) / 1080;
            float screenY = 1.0f - screenValue;
            Camera.main.rect = new Rect(0, screenY, 1, screenValue);
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
