using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

    [SerializeField] int playerLives = 3;
    [SerializeField] Text livesText;
    [SerializeField] Player player;
    [SerializeField] GameObject dialogManager;

    private void Awake() {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if(numGameSession > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }


    }
    // Use this for initialization
    void Start () {
        livesText.text = playerLives.ToString();
	}
	
    public void ProcessPlayerDeath() {
        if(playerLives > 1) {
            TakeLife();
        }
        else {
            ResetGameSession();
        }
    }

    private void TakeLife() {
        playerLives--;
        var CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex);
        livesText.text = playerLives.ToString();
        dialogManager.GetComponent<Dialog>().continueButton.SetActive(true);
    }

    private void ResetGameSession() {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            //Save
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            //Load
            Load();
        }
    }

    private void Save() {
        SaveObject save = new SaveObject {
            positionx = player.transform.position.x,
            positiony = player.transform.position.y,
            scene = SceneManager.GetActiveScene().buildIndex
        };
        string json = JsonUtility.ToJson(save);
        Debug.Log(json);
        File.WriteAllText(Application.dataPath + "/save.txt", json);

    }

    private void Load() {
        if(File.Exists(Application.dataPath + "/save.txt")) {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            SaveObject save = JsonUtility.FromJson<SaveObject>(saveString);
            player.transform.position = new Vector2(save.positionx, save.positiony);
        }
    }

    private class SaveObject {
        public int scene;
        public float positionx;
        public float positiony;
    }
}
