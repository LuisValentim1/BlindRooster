using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GeneralManager : MonoBehaviour
{

    public GameObject startMenuUI;
    public GameObject gameUI;
    public GameObject roomNumberDisplay;
    public GameObject joinCodeInbox;
    public GameObject difficultySlider;
    public int difficulty;
    public int roomCode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMenuUIOff(){
        startMenuUI.gameObject.SetActive(false);
    }

    public void StartMenuUIOn(){
        startMenuUI.gameObject.SetActive(true);
    }

    public void GameUIOn(){
        Text textBox = roomNumberDisplay.GetComponent<Text>();
        textBox.text = roomCode.ToString();
        gameUI.gameObject.SetActive(true);
    }

    public void GameUIOff(){
        gameUI.gameObject.SetActive(false);
    }

    public int GetCode(){
        return Int16.Parse(joinCodeInbox.GetComponent<Text>().text);
    }

    public void CreateRoom(){
        roomCode = UnityEngine.Random.Range(0,1000);
        difficulty = (int)difficultySlider.GetComponent<Slider>().value;
        StartMenuUIOff();
        GameUIOn();
        print(roomCode);
    }

    public void JoinRoom(){
        roomCode = GetCode();
        StartMenuUIOff();
        GameUIOn();
        print(roomCode);
    }
}
