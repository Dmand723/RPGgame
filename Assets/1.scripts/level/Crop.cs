using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class Crop : MonoBehaviour
{

    private CropData curCrop;
    private int plantDay;
    private int daysLastWatred;
    public bool cropIsDead;

    public SpriteRenderer sr;


    public static event UnityAction<CropData> onPlantCropEvent;
    public static event UnityAction<CropData> onHarvestCropEvent;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void plant(CropData crop)
    {
        curCrop = crop;
        plantDay = GameManager.instance.getCurDay();
        daysLastWatred = 1;
        updateCropSprite();
        onPlantCropEvent?.Invoke(crop);
    }

    public void newDayCheck()
    {
        daysLastWatred++;
        if (daysLastWatred > curCrop.getNoWatterTime())
        {
            killCrop();
        }
    }
    public void updateCropSprite()
    {
        int cropProg = cropProgress();
        sr.sprite = curCrop.getCropSprite(cropProg);
        
    }

    private int cropProgress()
    {
        return GameManager.instance.getCurDay() - plantDay;
    }

    public void onWater()
    {
        daysLastWatred = 0;
    }
    public void harvest()
    {
        if (canHarvast())
        {
            onHarvestCropEvent?.Invoke(curCrop);
            
            Destroy(gameObject);
        }
    }

    public bool canHarvast()
    {
        return cropProgress() >= curCrop.getDaysToGrow();
    }





    private void killCrop()
    {
        cropIsDead = true;
        
        Destroy(gameObject);
    }
}