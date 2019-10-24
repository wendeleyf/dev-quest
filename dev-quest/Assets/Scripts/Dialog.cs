﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour {
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
   
    public GameObject continueButton;
    public PlayerEnhanced player;
    private AudioSource source;

    void Awake() {
        textDisplay = GameObject.Find("Dialog Text").GetComponent<TextMeshProUGUI>();
        continueButton = GameObject.Find("CONTINUAR");
        continueButton.SetActive(false);
        //print(continueButton);
    }

    void Start() {
        player = GameObject.Find("Player").GetComponent<PlayerEnhanced>();
        source = GetComponent<AudioSource>();
        StartCoroutine(Type());   
    }

    void Update() {
        if (textDisplay.text == sentences[index]) {
            player.enabled = false;
            continueButton.SetActive(true);
            
        }
    }

    IEnumerator Type() {
        
        foreach (char letter in sentences[index].ToCharArray()) {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
       
    }

    public void NextSentence() {
        source.Play();
        continueButton.SetActive(false);
        if (index < sentences.Length - 1) {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else {
            textDisplay.text = "";
            continueButton.SetActive(false);
            player.enabled = true;
        }
    }
}
