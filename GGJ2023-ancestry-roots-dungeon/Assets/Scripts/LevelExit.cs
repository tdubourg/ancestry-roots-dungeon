using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;
class LevelExit : MonoBehaviour {

    public Levels NextLevel;
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Debug.Log("Trigger");
        var other = otherCollider.gameObject;
        if (null == other)
        { // GO may have already been deleted
            return;
        }
        if (other.tag == AttackEntity.AttackersTags.Player) {
            LevelTransitioner.GetInstance().GoToLevelViaMap(NextLevel);
        }
    }

}