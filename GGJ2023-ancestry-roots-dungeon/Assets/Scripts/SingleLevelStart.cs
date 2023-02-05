using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class SingleLevelStart : MonoBehaviour {

    // Set this from the scene in order to tell the monobehavior what level this scene is about
    public Levels level;
    static private SingleLevelStart instance;
    static public SingleLevelStart GetInstance() {
        if (null == instance) {
            Debug.Log("SingleLevelStart.GetInstance() called before init");
        }
        return instance;
    }

    void Awake() {
        if (null != instance) {
            DestroyImmediate(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {
        var levelT = LevelTransitioner.GetInstance();
        levelT.CurrentLevel = level;
        if (!levelT.IsLevelCleared(this.level)) {
            levelT.TriggerLevelIntro();
        }
    }


}
