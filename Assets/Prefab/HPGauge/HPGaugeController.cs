using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGaugeController : MonoBehaviour
{
    [SerializeField]
    GameObject panelHP; 
    [SerializeField]
    GameObject panelMaxHP;
    RectTransform panelHPTransform;
    RectTransform panelmaxHPTransform;
    [SerializeField]
    public Vector2 panelSize;
    public float panelWidth;
    // Start is called before the first frame update
    void Start()
    {
        panelHPTransform = panelHP.GetComponent<RectTransform>();
        panelmaxHPTransform = panelMaxHP.GetComponent<RectTransform>();
        panelHPTransform.sizeDelta = panelSize;
        panelmaxHPTransform.sizeDelta = panelSize;
           
    }

    // Update is called once per frame
    void Update()
    {
        panelSize.Set(panelWidth, panelSize.y);
        panelHPTransform.sizeDelta = panelSize;
    }
}
