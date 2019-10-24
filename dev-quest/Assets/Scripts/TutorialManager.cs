using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public GameObject[] popUps;
    private int popUpIndex;

    void Update() {

        for (int i = 0; i < popUps.Length; i++) {
            if(i == popUpIndex) {
                popUps[i].SetActive(true);
            }
            else {
                popUps[i].SetActive(false);
            }

        }

        if(popUpIndex == 0){
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
                popUpIndex++;
            }
        }else if(popUpIndex == 1) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                popUpIndex++;
            }
        }else if(popUpIndex == 2) {

        }
    }

}
