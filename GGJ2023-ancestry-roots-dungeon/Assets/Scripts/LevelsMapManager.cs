using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsMapManager : MonoBehaviour {

    static Dictionary<Levels, int> LEVEL_TO_INT_MAP = new Dictionary<Levels, int>() {
        {Levels.TrainingGround1, 1},
        {Levels.TrainingGround2, 3},
        {Levels.QuestRealStart, 4},
        {Levels.GameIntro, 0},
    };

    public List<GameObject> LevelPoints;
    public List<Sprite> States;//0 = Off & 1 = On
    LineRenderer LineAnimator;
    public float LineDrawSpeed = .006f;
    public Material LineMaterial;
    public float lineSize = 0.05f;
    public Color lineColor = new Color(125f, 21f, 17f);
    private Transform origin;
    private Transform destination;
    private float dist = 1000f;
    private float counter;
    private float nextActionTime = 0.0f;
    private float maxActionTime = 2.0f;
    public float period = 0.1f;
    private bool IsBlinking = false;
    int LevelIndex;
    static private LevelsMapManager instance;

    static public LevelsMapManager GetInstance() {
        if (null == instance) {
            Debug.Log("LevelsMapManager.GetInstance() called before init");

        }
        return instance;
    }
    void Awake() {
        if (null != instance) {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }

    public void SetLevelCompletion(int currentLevel) {
        //if (currentLevel < 0)
        //{

        //    IsBlinking = true;
        //}
        //else
        //{
        LevelIndex = currentLevel;
        if (LevelIndex != LevelPoints.Count) {
            LevelIndex++;
        }

        origin = LevelPoints[currentLevel].transform;
        if (LevelPoints.Count > currentLevel + 1) {
            destination = LevelPoints[currentLevel + 1].transform;

            dist = Vector3.Distance(origin.position, destination.position);
        }

        for (var i = 0; i <= currentLevel; i++) {
            if (currentLevel - i - 1 >= 0) {
                DrawLineBetweenTwoPoints(LevelPoints[currentLevel - i - 1].transform.position, LevelPoints[currentLevel - i].transform.position, i);
            }
            var mySprite = LevelPoints[currentLevel - i].GetComponent<SpriteRenderer>();
            if (mySprite != null) {
                mySprite.sprite = States[1];
            }
        }
        //}
    }


    void DrawLineBetweenTwoPoints(Vector3 sp, Vector3 ep, int i) {
        GameObject childOb = new GameObject("ChildLienRenderer" + i.ToString());
        childOb.transform.SetParent(this.transform);
        var Line = childOb.AddComponent<LineRenderer>();
        Line.material = LineMaterial;
        Line.startColor = lineColor;
        Line.endColor = lineColor;
        Line.startWidth = lineSize;
        Line.endWidth = lineSize;
        Line.SetPosition(0, sp);
        Line.SetPosition(1, ep);
    }

    // Start is called before the first frame update
    void Start() {
        LineAnimator = gameObject.AddComponent<LineRenderer>();
        LineAnimator.startColor = lineColor;
        LineAnimator.endColor = lineColor;
        LineAnimator.startWidth = lineSize;
        LineAnimator.endWidth = lineSize;
        LineAnimator.material = LineMaterial;
        if (null != LevelTransitioner.GetInstance()) {
            SetLevelCompletion(LEVEL_TO_INT_MAP[LevelTransitioner.GetInstance().PreviousLevel]);
        } else { // for standalone debugging
            SetLevelCompletion(1);
        }
        //SetLevelCompletion(1);
    }

    // Update is called once per frame
    void Update() {
        if (Time.time > nextActionTime && Time.time <= maxActionTime) {

            nextActionTime += period;
            LevelPoints[LevelIndex].GetComponent<SpriteRenderer>().enabled = !LevelPoints[LevelIndex].GetComponent<SpriteRenderer>().enabled;
        }

        if (counter < dist) {
            Debug.Log("counter " + counter);
            Debug.Log("dist " + dist);
            Debug.Log("bool " + (counter < dist));
            counter += .1f * LineDrawSpeed;

            float x = Mathf.Lerp(0, dist, counter);

            Vector3 pointA = origin.position;
            Vector3 pointB = destination.position;

            Vector3 pointAlongline = x * Vector3.Normalize(pointB - pointA) + pointA;
            LineAnimator.SetPosition(0, origin.position);
            LineAnimator.SetPosition(1, pointAlongline);
        }
        
        if (Time.timeSinceLevelLoad > 1 && Input.anyKeyDown) {
            LevelTransitioner.GetInstance().ContinueAfterTreeMap();
        }
    }
}
