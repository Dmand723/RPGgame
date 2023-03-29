using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEditor.AssetImporters;

public class GameManager : MonoBehaviour
{
    public int curDay;
    public float money;
    public CropData selectedCrop;
    public int cropInventory;



    public event UnityAction onNewDayEvent;



    public static GameManager instance;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI seedText;


    private void Awake()
    {
        if (instance ! == null && instance ! == this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        curDay = PlayerPrefs.GetInt("curDay", 0);
        money = PlayerPrefs.GetFloat("money", 100);
        cropInventory = PlayerPrefs.GetInt("cropInventory", 0);
        setNextDay();
        updateUI();
    }


    private void OnEnable()
    {
        // listening for events 
        Crop.onPlantCropEvent += onPlantCrop;
        Crop.onHarvestCropEvent += onHarvestCrop;
    }
    private void OnDisable()
    {
        // stop listen for events 
        Crop.onPlantCropEvent -= onPlantCrop;
        Crop.onHarvestCropEvent -= onHarvestCrop;
    }

    public void onPlantCrop(CropData crop)
    {
        cropInventory--;
        updateUI();
    }
    public void onHarvestCrop(CropData crop)
    {
        money += crop.sellPrice;
        updateUI();
    }
    public void updateUI()
    {
        // Change money text
        moneyText.text = this.money.ToString();
      // change the seed count
      seedText.text = this.cropInventory.ToString();
        timeText.text = "12:00";
        dayText.text = curDay.ToString();



        PlayerPrefs.SetFloat("money", money);
        PlayerPrefs.SetInt("cropInventory", cropInventory);
        PlayerPrefs.SetInt("curDay", curDay);
    }
    
    public void setNextDay()
    {
        curDay ++;
        onNewDayEvent?.Invoke();
        updateUI();
    }

    public void purchaseCrops(CropData crop)
    {
        money -= crop.purchasePrice;
        cropInventory++;

        updateUI();
    }
    public bool canPlantCrop()
    {
        return true;
    }

    public void onBuyCropButton(CropData crop)
    {
        if(money >= crop.purchasePrice)
        {
            purchaseCrops(crop);
        }
    }

    public void onSwitchCropButton()
    {

    }

    public int getCurDay()
    {
        return this.curDay;
    }

    public CropData getSelectedCrop()
    {
        return this.selectedCrop;
    }
    
}
