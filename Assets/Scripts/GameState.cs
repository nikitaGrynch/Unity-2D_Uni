using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public static int pipesPassed { get; set; }
    public static float vitality { get; set; }
    public static bool isPipeHitted { get; set; }

    public static bool isWkeyEnabled { get; set; }
    public static float pipeSpawnPeriod { get; set; }
    public static float vitalityPeriod { get; set; }

    public static String ToJson()
    {
        return JsonUtility.ToJson(new SavedSettings());
    }

    public static void FromJson(String json)
    {
        var setting = JsonUtility.FromJson<SavedSettings>(json);
        isWkeyEnabled = setting.isWkeyEnabled;
        pipeSpawnPeriod = setting.pipeSpawnPeriod;
        vitalityPeriod = setting.vitalityPeriod;
    }
}


[Serializable]
public class SavedSettings
{
    public bool isWkeyEnabled;
    public float pipeSpawnPeriod;
    public float vitalityPeriod;

    public SavedSettings()
    {
        isWkeyEnabled = GameState.isWkeyEnabled;
        pipeSpawnPeriod = GameState.pipeSpawnPeriod;
        vitalityPeriod = GameState.vitalityPeriod;
    }
}