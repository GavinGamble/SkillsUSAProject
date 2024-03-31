using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    float originalSize;
    public Image mask;

    public static TowerHealth instance { get;  private set; } 

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        originalSize = mask.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setValue(float value)
    {
        //changes the size of the health bar
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize *  value);
    }
}
