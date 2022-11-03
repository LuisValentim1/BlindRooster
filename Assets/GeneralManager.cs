using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


//GeneralManager está a gerir tudo, UI, salas, jogo, mesmo à campeão, se quiserem separar em sistemas otimo mas acho que para um projeto deste tamanho não se justifica 
//Provavelmente deveria existir uma classe Room que herdava parte da informação que está aqui, para isto funcionar em multiplayer.
//As funções CreateRoom e JoinRoom estão ligadas a butões no Unity e iniciam o loop de jogo, acho que o resto do que está aqui é self explanatory mas qualquer dúvida perguntem 
public class GeneralManager : MonoBehaviour
{

    public GameObject startMenuUI;
    public GameObject gameUI;
    public GameObject roomNumberDisplay;
    public GameObject joinCodeInbox;
    public GameObject difficultySlider;
    public GameObject game;
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

    public void StartGame(){
        game.GetComponent<Game>().SetDifficulty(difficulty);
        game.GetComponent<Game>().StartGame();
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
        SetDifficulty(difficulty);
        StartMenuUIOff();
        GameUIOn();
        StartGame();
        print(roomCode);
    }

    public void JoinRoom(){
        roomCode = GetCode();
        difficulty = 0;
        StartMenuUIOff();
        GameUIOn();
        StartGame();
        print(roomCode);
    }

    public void SetDifficulty(int difficultyLevel)
    {
        difficulty = difficultyLevel;
    }
}
