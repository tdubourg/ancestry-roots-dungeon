using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 
    UI_Health : MonoBehaviour
{

    public List<GameObject> HealthContainer;


    // Start is called before the first frame update
    void Start()
    {
        var offset = -8.83f;
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        foreach (var Thing in HealthContainer)
        {
            var stuff = Instantiate(Thing, new Vector3(offset, -4.44f, 0), Quaternion.identity);
            offset += 1;
            stuff.transform.SetParent(this.transform);
            //Debug.Log(cam.orthographicSize);

        }
        //HealthContainer = new List<GameObject>();
        //var s = new GameObject();
        //s.sprite
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
