using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;


public enum Levels
{
    SplashScreen,
    GameIntro,
    TrainingGround1,
    TrainingGround2,
    QuestRealStart,
    INVALID,
}

class LevelTransitioner: MonoBehaviour
{
    static private LevelTransitioner instance;

    public GameObject UIRoot;
    
    public Levels CurrentLevel = Levels.SplashScreen;

    private HashSet<Levels> clearedLevels = new HashSet<Levels>();

    public void SetHasClearedLevel(Levels level) {
        clearedLevels.Add(level);
    }

    public bool IsLevelCleared(Levels level) {
        return clearedLevels.Contains(level);
    }

    private LevelTransitioner()
    {
    }

    private string getSceneFileNameFromEnum(Levels level)
    {
        switch (level)
        {

            case Levels.GameIntro:
                return "GameIntro";
            case Levels.TrainingGround1:
                return "TrainingGround1";
            case Levels.TrainingGround2:
                return "TrainingGround2";
            case Levels.QuestRealStart:
                return "QuestRealStart";
            case Levels.SplashScreen:
            default:
                return "SplashScreen";
        }
    }

    
    void Awake()
    {
        instance = this;
    }

    static public LevelTransitioner GetInstance()
    {
        if (null == instance)
        {
            Debug.Log("LevelTransitioner.GetInstance() called before init");

        }
        return instance;
    }

    public void GoToLevel(Levels target)
    {
        SceneManager.LoadScene(getSceneFileNameFromEnum(target));
    }

    public void TriggerLevelIntro() {

    }
}