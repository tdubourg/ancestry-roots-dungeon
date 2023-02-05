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
    MapTree,
}

class LevelTransitioner : MonoBehaviour {

public GameObject UIRoot;
public GameObject PlayerRoot;
    [Serializable]
    public struct LevelInfo {
        public Levels level;
        public Sprite ancestor;

        public TextAsset text;

        public Vector2 spawnCoords;
    }
    public LevelInfo[] LevelsConfig;

    private Dictionary<Levels, LevelInfo> LEVELS_TO_CONFIG = new Dictionary<Levels, LevelInfo>();
    static private LevelTransitioner instance;

    public Levels PostTreeMapLevelTarget { get; private set;}

    public Levels PreviousLevel { get; private set; }

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
            case Levels.MapTree:
                return "MapOverworld"; // TODO change
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
        PreviousLevel = Levels.INVALID;
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
        PreviousLevel = CurrentLevel;
        CurrentLevel = target;
        if (target == Levels.GameOver) {
            // Tear down everything
            Destroy(gameObject);
        }
        SceneManager.LoadScene(getSceneFileNameFromEnum(target));
    }

    public void GoToLevelViaMap(Levels target) {
        PreviousLevel = CurrentLevel;
        PostTreeMapLevelTarget = target;
        PlayerRoot.SetActive(false);
        UIRoot.SetActive(false);
        if (target == Levels.GameOver) {
            // Tear down everything
            Destroy(gameObject);
        }
        SceneManager.LoadScene(getSceneFileNameFromEnum(Levels.MapTree));
    }


    public void TriggerLevelIntro() {
        Debug.Log("TriggerLevelIntro");
        if (!LEVELS_TO_CONFIG.ContainsKey(CurrentLevel)) {
            return;
        }
        var info = LEVELS_TO_CONFIG[CurrentLevel];
        NarrativeAncestorImage.sprite = info.ancestor;
        NarrativeTextScroller.Text = info.text;
        if (PlayerRoot) {
            var playerGO = PlayerRoot.GetComponentInChildren<PlayerController>().gameObject;
            Debug.Log(playerGO.transform.position);
            Debug.Log(LEVELS_TO_CONFIG[CurrentLevel].spawnCoords);
            playerGO.transform.position = new Vector3(LEVELS_TO_CONFIG[CurrentLevel].spawnCoords.x, LEVELS_TO_CONFIG[CurrentLevel].spawnCoords.y, playerGO.transform.position.z);
        }
        LevelIntro.SetActive(true);
    }

    private void init() {
        if (!IsLevelCleared(CurrentLevel)) {
            TriggerLevelIntro();
        }
    }

}