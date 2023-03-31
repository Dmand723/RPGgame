using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEditor.AssetImporters;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public int curDay;
    public float money;
    public CropData selectedCrop;
    public CropData[] cropOptions;
    public int cropInventory;

    public PlayerController playerController;

    public Light2D globalLight;


    public event UnityAction onNewDayEvent;



    public static GameManager instance;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI seedText;
    public TMP_Dropdown cropSelection;



    public int hour;
    public int minute;
    public int updateCounter = 0;
    public int scaleValue = 60;

    [Header("Options")]
    public bool hourFormat12;
    private void Awake()
    {
        
        print(cropSelection.value);
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
        cropInventory = playerController.seedInventory[cropSelection.value];
        
        setNextDay();
        updateUI();
    }

    private void FixedUpdate()
    {
        updateCounter++;
        if (updateCounter >= scaleValue)
        {
            minute++;
            updateCounter = 0;
        }
        if(minute >= 60)
        {
            minute = 00;
            hour++;
        }
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
        playerController.seedInventory[cropSelection.value]--;
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
      seedText.text = playerController.seedInventory[cropSelection.value].ToString();

        string HourText = hour.ToString();
        string MinuteText = minute.ToString();
        if (hourFormat12 && hour > 12)
        {
            HourText = (hour -= 12).ToString();
        }
        else
        {
            HourText = hour.ToString(); 
        }
        if (minute <= 9)
        {
            timeText.text = HourText + ":0" + MinuteText;
        }
        else
        {
            timeText.text = HourText + ":" + MinuteText;
        }
        



        dayText.text = curDay.ToString();



        PlayerPrefs.SetFloat("money", money);
        PlayerPrefs.SetInt("cropInventory", cropInventory);
        PlayerPrefs.SetInt("curDay", curDay);
    }
    
    public void setNextDay()
    {
        curDay ++;
        hour = 4;
        minute = 30;
        globalLight.intensity = .05f;
        onNewDayEvent?.Invoke();
        updateUI();
    }

    public void purchaseCrops(CropData crop)
    {
        money -= crop.purchasePrice;
        playerController.seedInventory[cropSelection.value]++;
        updateUI();
    }
    public bool canPlantCrop()
    {
        return true;
    }

    public void onBuyCropButton()
    {
        if(money >= selectedCrop.purchasePrice)
        {
            purchaseCrops(selectedCrop);
        }
    }

    public void onSwitchCropButton()
    {
        selectedCrop = cropOptions[cropSelection.value];
        cropInventory = playerController.seedInventory[cropSelection.value];
        updateUI();
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
