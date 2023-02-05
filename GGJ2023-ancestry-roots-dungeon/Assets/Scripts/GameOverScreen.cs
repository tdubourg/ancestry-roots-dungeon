using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > 2 && Input.anyKeyDown)
        {
            Debug.Log("Restarting the game");
            LevelTransitioner.GetInstance().GoToLevel(Levels.SplashScreen);
        }
    }
}
