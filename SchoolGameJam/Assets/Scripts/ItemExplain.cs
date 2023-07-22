using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemExplain : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform ui;

    public Text explain;

    public string explainText;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.gameObject.SetActive(true);
        ui.transform.position = eventData.position;
        explain.text = explainText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.gameObject.SetActive(false);


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
