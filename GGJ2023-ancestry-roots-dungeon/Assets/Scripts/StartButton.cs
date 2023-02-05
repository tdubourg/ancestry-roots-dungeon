using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartButton : MonoBehaviour
{
    public void TransitionNextLevel()
    {
        LevelTransitioner.GetInstance().GoToLevel(Levels.GameIntro);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
