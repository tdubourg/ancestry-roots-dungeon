using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;

public enum Levels {
    SplashScreen,
    GameIntro,
    TrainingGround1,
    TrainingGround2,
    QuestRealStart,
    INVALID,
    GameOver,
}

class LevelTransitioner : MonoBehaviour {

    [Serializable]
    public struct LevelInfo {
        public Levels level;
        public Sprite ancestor;

        public TextAsset text;
    }
    public LevelInfo[] LevelsConfig;

    private Dictionary<Levels, LevelInfo> LEVELS_TO_CONFIG = new Dictionary<Levels, LevelInfo>();
    static private LevelTransitioner instance;

    public GameObject LevelIntro;
    public Image NarrativeAncestorImage;
    public StoryTextScroller NarrativeTextScroller;

    public Levels CurrentLevel = Levels.SplashScreen;

    private HashSet<Levels> clearedLevels = new HashSet<Levels>();

    public void SetHasClearedLevel(Levels level) {
        clearedLevels.Add(level);
    }

    public bool IsLevelCleared(Levels level) {
        return clearedLevels.Contains(level);
    }

    public bool KeepOnSceneChange = true;

    private LevelTransitioner() {
    }

    private string getSceneFileNameFromEnum(Levels level) {
        switch (level) {

            case Levels.GameIntro:
                return "GameIntro";
            case Levels.TrainingGround1:
                return "TrainingGround1";
            case Levels.TrainingGround2:
                return "TrainingGround2";
            case Levels.QuestRealStart:
                return "QuestRealStart";
            case Levels.GameOver:
                return "GameOver";
            case Levels.SplashScreen:
            default:
                return "SplashScreen";
        }
    }

    void Awake() {
        if (null != instance) {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        if (KeepOnSceneChange) {
            DontDestroyOnLoad(gameObject);
        }
        for (int i = 0; i < LevelsConfig.Length; i++) {
            LEVELS_TO_CONFIG[LevelsConfig[i].level] = LevelsConfig[i];
        }
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode) {
        init();
    }

    static public LevelTransitioner GetInstance() {
        if (null == instance) {
            Debug.Log("LevelTransitioner.GetInstance() called before init");

        }
        return instance;
    }

    public void GoToLevel(Levels target) {
        CurrentLevel = target;
        if (target == Levels.GameOver) {
            // Tear down everything
            Destroy(gameObject);
        }
        SceneManager.LoadScene(getSceneFileNameFromEnum(target));
    }

    public void TriggerLevelIntro() {
        Debug.Log("TriggerLevelIntro");
        if (!LEVELS_TO_CONFIG.ContainsKey(CurrentLevel)) {
            return;
        }
        var info = LEVELS_TO_CONFIG[CurrentLevel];
        NarrativeAncestorImage.sprite = info.ancestor;
        NarrativeTextScroller.Text = info.text;
        LevelIntro.SetActive(true);
    }

    private void init() {
        if (!IsLevelCleared(CurrentLevel)) {
            TriggerLevelIntro();
        }
    }

}