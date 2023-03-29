using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FieldTile : MonoBehaviour
{
    [Header("Crop")]
    private Crop curCrop;

    [Header("components")]
    public GameObject cropPrefab;
    private SpriteRenderer sr;

    [Header("States")]
    public bool isWatered;
    public bool isTilled;
    public bool hasCrop;
    public Sprite[] sprites;
    public int spriteIndex = 0;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        spriteIndex = 0;
        sr.sprite = sprites[spriteIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onNewDay()
    {
        if (curCrop = null)
        {
            isTilled= false;
            spriteIndex = 0;

        }
        else if(curCrop != null)
        {
            spriteIndex = 1;
            curCrop.newDayCheck();
        }
        updateSpite();
    }

    public void till()
    {
        spriteIndex = 1;
        isTilled = true;
        
    }

    public void water()
    {
        spriteIndex = 2;
        isWatered = true;
        if(hasCrop)
        {
            curCrop.onWater(); 
        }
       
    }
    public void plantNewCrop(CropData crop)
    {
        if(!isTilled)
        {
            return;
        }
        hasCrop = true;
        curCrop = Instantiate(cropPrefab, transform).GetComponent<Crop>();
        curCrop.plant(crop);
        GameManager.instance.onNewDayEvent += onNewDay;
    }

    public void updateSpite()
    {
        sr.sprite = sprites[spriteIndex];
    }
    public void interact()
    {

        if (!isTilled)
        {
            till();
        }
        else if (!hasCrop && GameManager.instance.canPlantCrop())
        {
            plantNewCrop(GameManager.instance.getSelectedCrop());
        }
        else if (hasCrop && curCrop.canHarvast())
        {
           hasCrop = false;
           curCrop.harvest();
        }
        else
        {
            water();

        }

        updateSpite();

    }
}
