using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Levels
{
    SplashScreen,
    GameIntro,
    TrainingGround1,
    TrainingGround2,
    QuestRealStart,
    INVALID,
}

class LevelTransitioner
{
    static private LevelTransitioner instance;
    private Levels currentLevel = Levels.SplashScreen;

    public Levels getCurrentLevel() {
        return currentLevel;
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

    static public LevelTransitioner GetInstance()
    {
        if (null == instance)
        {
            instance = new LevelTransitioner();
        }
        return instance;
    }

    public void GoToLevel(Levels target)
    {
        SceneManager.LoadScene(getSceneFileNameFromEnum(target));
    }
}