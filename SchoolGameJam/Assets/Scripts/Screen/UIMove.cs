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
     
    bool isScreenMove = false;
    
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
            isScreenMove = !isScreenMove;
            if(coroutine != null) StopCoroutine(coroutine);
            bg.transform.DOMoveY(bgMovePos[(isScreenMove) ? 0 : 1], 1f);
            ui.transform.DOMoveY(uiMovePos[(isScreenMove) ? 0 : 1], 1f);
            if (isScreenMove) coroutine = StartCoroutine(IControlKeySet());
            else controlKey.gameObject.SetActive(false);    
        }
    }

    IEnumerator IControlKeySet()
    {
        controlKey.localPosition = new Vector2(controlKey.localPosition.x, controlKeyPos.position.y);
        yield return new WaitForSeconds(1);
        controlKey.gameObject.SetActive(true);
    }
    
}
