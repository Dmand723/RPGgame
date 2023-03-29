using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Crop Data", menuName = "New Crop Data")]
public class CropData : ScriptableObject
{
    [Header("Crop Info")]
    public int daysToGrow;
    public Sprite[] growProgress_Sprites;
    public Sprite harvest_Sprite;
    public int noWaterTime;

    [Header("Econ")]
    public float purchasePrice;
    public float sellPrice;

    public int getNoWatterTime()
    {
        return noWaterTime;
    }
    public int getDaysToGrow()
    {
        return daysToGrow;
    }
    public Sprite getCropSprite(int num)
    {
        if (num < daysToGrow)
        {
            return growProgress_Sprites[num];
        }
        else
        {
            return harvest_Sprite;
        }
    }
}
