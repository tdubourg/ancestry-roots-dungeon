using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.
using System;
using TMPro;

public class StoryTextScroller : MonoBehaviour {
    static private StoryTextScroller instance;
    public TMP_Text TextMeshPro;

    public TextAsset Text;

    public int PageSizeNumWords = 30;

    public Levels NextScene = Levels.INVALID;

    public bool GoToNextSceneOnceEnded = false;

    public bool VanishOnceEnded = false;

    private int currentPage = 0;

    private ArrayList pages = new ArrayList();
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
    }

    void Start() {
        Debug.Log(Text.text);
        var words = Text.text.Split(" ");
        var pageNum = 0;
        pages.Add("");
        // Hacky, unoptimized, but quick to write
        for (int i = 0; i < words.Length; i++) {
            pages[pageNum] += words[i] + " ";
            if (0 == ((i+1) % PageSizeNumWords)) {
                Debug.Log(pages[pageNum]);
                pageNum += 1;
                pages.Add("");
            }
        }
        TextMeshPro.text = pages[0].ToString();
    }


    void Update() {
        if (Input.anyKeyDown) {
            Debug.Log("Next page of text");
            NextPage();
        }
    }

    public void NextPage() {
        Debug.Log(currentPage + " " + pages.Count);
        if (currentPage == pages.Count - 1) {
            TextMeshPro.text = "...";
            if (GoToNextSceneOnceEnded && Levels.INVALID != NextScene) {
                LevelTransitioner.GetInstance().GoToLevel(NextScene);
            } else if (VanishOnceEnded) {
                gameObject.SetActive(false);
            }
        } else {
            currentPage += 1;
            TextMeshPro.text = pages[currentPage].ToString();
        }
    }

}