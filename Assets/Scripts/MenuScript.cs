using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private Toggle keyWToggle;
    [SerializeField]
    private Slider pipePeriodSlider;
    [SerializeField]
    private Slider vitalityPeriodSlider;

    private bool isMenuShown;
    private const String settingsFilename = "./Assets/Files/settings.json"; 

    void Start()
    {
        if (LoadSettings())
        {
            keyWToggle.isOn = GameState.isWkeyEnabled;
            pipePeriodSlider.value = (6f - GameState.pipeSpawnPeriod) / (6f - 2f);
            vitalityPeriodSlider.value = (60f - GameState.vitalityPeriod) / (60f - 20f);
        }
        else
        {

        }
        GameState.isWkeyEnabled = keyWToggle.isOn;
        OnPipePeriodSlider(pipePeriodSlider.value);
        OnVitalityPeriodSlider(vitalityPeriodSlider.value);

        isMenuShown = content.activeInHierarchy;
        ToggleMenu(isMenuShown);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ToggleMenu(!isMenuShown);
        }
    }

    private void LateUpdate()
    {
        if (GameState.isPipeHitted)
        {
            ToggleMenu(true);
        }
    }
    private void ToggleMenu(bool isShow)
    {
        if (isShow)
        {
            Time.timeScale = 0f;
            isMenuShown = true;
            content.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            isMenuShown = false;
            content.SetActive(false);
            if (GameState.isPipeHitted)
            {
                foreach (var pipe in GameObject.FindGameObjectsWithTag("Pipe"))
                {
                    GameObject.Destroy(pipe);
                }
                foreach(var food in GameObject.FindGameObjectsWithTag("Food"))
                {
                    GameObject.Destroy(food);
                }
                GameState.isPipeHitted = false;
            }
        }
    }

    private void SaveSettings()
    {
        System.IO.File.WriteAllText(settingsFilename, GameState.ToJson());
    }

    private bool LoadSettings()
    {
        if (System.IO.File.Exists(settingsFilename))
        {
            GameState.FromJson(System.IO.File.ReadAllText(settingsFilename));
            return true;
        }
        return false;
    }


    // UI Event handlers
    public void OnCloseButtonClick()
    {
        ToggleMenu(false);
    }
    public void OnControlWkeyChanged(Boolean value)
    {
        GameState.isWkeyEnabled = value;
        SaveSettings();
    }
    public void OnPipePeriodSlider(Single value)
    {
        // value[0..1] ---> time [6..2]
        GameState.pipeSpawnPeriod = 6f - value * (6f - 2f);
        SaveSettings();
    }
    public void OnVitalityPeriodSlider(Single value)
    {
        // value[0..1] ---> time [60..20]
        GameState.vitalityPeriod = 60f - value * (60f - 20f);
        SaveSettings();

    }
}