using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ColorType
{
    Orange, Blue, Green, Yellow, Red, Rainbow 
}

public class ColorPicker : MonoBehaviour
{
    public static ColorPicker instance;

    [SerializeField]
    private Color orange; //new Color32(255, 134, 79, 255);
    [SerializeField]
    private Color blue; //Color32(255, 132, 78, 255);
    [SerializeField]
    private Color green; //new Color32(140, 241, 140, 255);
    [SerializeField]
    private Color yellow; //new Color32(255, 34, 129, 255);
    [SerializeField]
    private Color purple; //new Color32(152, 106, 237, 255);
    [SerializeField]
    private Color red; //new Color32(255, 85, 94, 255);

    

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
       
    }

    public Color GetColorRGBValue(ColorType colorType)
    {
        switch (colorType)
        {
            case ColorType.Orange:
                return orange;
            case ColorType.Blue:
                return blue;
            case ColorType.Green:
                return green;
            case ColorType.Yellow:
                return yellow;         
            case ColorType.Red:
                return red;
            default:
                return orange;
        }
    }

}
