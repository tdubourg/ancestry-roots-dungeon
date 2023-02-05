using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.
using System;


public class CameraFollowPlayer : MonoBehaviour {
    public Transform PlayerTransform;
    static private CameraFollowPlayer instance;
    static public CameraFollowPlayer GetInstance() {
        if (null == instance) {
            Debug.Log("CameraFollowPlayer.GetInstance() called before init");
        }
        return instance;
    }

    void Awake() {
        instance = this;
    }

    void Start() {
    }


    void Update() {
        this.gameObject.transform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, this.gameObject.transform.position.z);
    }

}