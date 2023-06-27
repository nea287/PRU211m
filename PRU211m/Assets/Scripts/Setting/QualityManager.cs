using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using System;

//0: LOW
//1: MEDIUM
//2: HIGH

public class QualityManager : MonoBehaviour
{
    public TMP_Dropdown qualityDropDown;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Quality"))
        {
            PlayerPrefs.SetInt("Quality", QualitySettings.GetQualityLevel());
            Load();
        }
        else
        {
            Load();
        }
    }
    
    public void ChangeQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Save();
    }

    private void Load()
    {
        qualityDropDown.value = PlayerPrefs.GetInt("Quality");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("Quality", qualityDropDown.value);
    }
}
