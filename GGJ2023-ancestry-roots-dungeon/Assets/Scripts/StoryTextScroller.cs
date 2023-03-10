using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryTextScroller : MonoBehaviour {
    static private StoryTextScroller instance;
    public TMP_Text TextMeshPro;

    public bool UseTreeMap = false;

    public TextAsset Text;

    public int PageSizeNumWords = 30;

    public Levels NextScene = Levels.INVALID;

    public bool GoToNextSceneOnceEnded = false;

    public bool VanishOnceEnded = false;

    private int currentPage = 0;

    public bool ExtraEndPage = false;

    private ArrayList pages;
    static public StoryTextScroller GetInstance() {
        if (null == instance) {
            Debug.Log("StoryTextScroller.GetInstance() called before init");
        }
        return instance;
    }

    void Awake() {
        if (null != instance) {
            return;
        }
        instance = this;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode) {
        init();
    }

    void OnEnable() {
        init();
    }

    void init() {
        try {
            if (!gameObject) {
            return;
        }} catch (Exception _e) {
            return;
        }
        Debug.Log("scroller init()");
        gameObject.SetActive(true);
        pages = new ArrayList();
        currentPage = 0;
        var words = Text.text.Split(" ");
        var pageNum = 0;
        pages.Add("");
        // Hacky, unoptimized, but quick to write
        for (int i = 0; i < words.Length; i++) {
            pages[pageNum] += words[i] + " ";
            if (0 == ((i + 1) % PageSizeNumWords)) {
                pageNum += 1;
                pages.Add("");
            }
        }
        if (ExtraEndPage) {
            pages.Add("...");
        }
        TextMeshPro.text = pages[0].ToString();

    }


    void Update() {
        if (Time.timeSinceLevelLoad > 1 && Input.anyKeyDown) {
            Debug.Log("Next page of text");
            NextPage();
        }
    }

    public void NextPage() {
        if (currentPage == pages.Count - 1) {
            TextMeshPro.text = "...";
            if (GoToNextSceneOnceEnded && Levels.INVALID != NextScene) {
                if (UseTreeMap) {
                    LevelTransitioner.GetInstance().GoToLevelViaMap(NextScene);
                } else {
                    LevelTransitioner.GetInstance().GoToLevel(NextScene);
                }
            } else if (VanishOnceEnded) {
                gameObject.SetActive(false);
            }
        } else {
            currentPage += 1;
            // Debug.Log(currentPage + " " + pages.Count + " " + pages[currentPage]);
            TextMeshPro.text = pages[currentPage].ToString();
        }
    }

}