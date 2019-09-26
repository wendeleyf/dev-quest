using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour {

    public float waitTime;
    [SerializeField]
    bool atPlatform = false;

    private PlatformEffector2D effector;
	// Use this for initialization
	void Start () {
        effector = GetComponent<PlatformEffector2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            waitTime = 0.5f;
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            if(waitTime <= 0) {
                effector.rotationalOffset = 180f;
                waitTime = 0.5f;
            }
            else {
                waitTime -= Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            effector.rotationalOffset = 0;
        }
    }
}
