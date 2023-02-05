using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsMapManager : MonoBehaviour
{

    public List<GameObject> LevelPoints;
    public List<Sprite> States;//0 = Off & 1 = On
    LineRenderer LineAnimator;
    public float lineDrawSpeed = .000006f;

    private Transform origin;
    private Transform destination;
    private float dist = 0;
    private float counter;

    public void SetLevelCompletion(int currentLevel)
    {
        origin = LevelPoints[currentLevel].transform;
        if (LevelPoints.Count > currentLevel + 1)
        {
            destination = LevelPoints[currentLevel + 1].transform;

            dist = Vector3.Distance(origin.position, destination.position);
        }
        
        for (var i = 0; i <= currentLevel; i++)
        {
            if (currentLevel - i - 1 >= 0)
            {
                DrawLineBetweenTwoPoints(LevelPoints[currentLevel - i - 1].transform.position, LevelPoints[currentLevel - i].transform.position, i);
            }
            var mySprite = LevelPoints[currentLevel-i].GetComponent<SpriteRenderer>();
            if (mySprite != null)
            {
                mySprite.sprite = States[1];
            }
        }
    }


    void DrawLineBetweenTwoPoints(Vector3 sp, Vector3 ep, int i)
    {
        GameObject childOb = new GameObject("ChildLienRenderer" + i.ToString());
        childOb.transform.SetParent(this.transform);
        var Line = childOb.AddComponent<LineRenderer>();
        Line.SetPosition(0, sp);
        Line.SetPosition(1, ep);
    }

    // Start is called before the first frame update
    void Start()
    {
        LineAnimator = gameObject.AddComponent<LineRenderer>();
        SetLevelCompletion(5);
    }

    // Update is called once per frame
    void Update()
    {
        if(counter < dist)
        {
            counter += .1f / lineDrawSpeed;

            float x = Mathf.Lerp(0, dist, counter);

            Vector3 pointA = origin.position;
            Vector3 pointB = destination.position;

            Vector3 pointAlongline = x * Vector3.Normalize(pointB - pointA) + pointA;
            LineAnimator.SetPosition(0, origin.position);
            LineAnimator.SetPosition(1, pointAlongline);


        }
    }
}
