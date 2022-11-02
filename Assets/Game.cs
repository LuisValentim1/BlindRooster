using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using TMPro;

//Esta classe tem um bocado de esparguete mas funciona com a estrutura atual, pode dar jeito reestruturar no futuro, força 

public class Game : MonoBehaviour
{
    public int difficulty;
    public Position[] slots;   //posições que já foram chamadas este jogo 
    public int playerTurn;     
    public int slotCounter;
    public List<Position> p1Slots;   //só posições do jogador1 
    public List<Position> p2Slots;   //só posições do jogador2
    public string playerChange; //string para guardar icon do jogador
    public CanvasGroup canvas;
    public GameObject winPanel;
    public TMP_Text winText;

    //Verificar a dificuldade do jogo e setup apropriado 
    public void StartGame()
    {
        this.transform.GetChild(difficulty).gameObject.SetActive(true);
        slots = new Position[(int)Math.Pow((difficulty+3),2)];
        slotCounter = 0;
    }

    //fazer uma jogada, associado aos butões que constituem o campo 
    public void SetPlay(string position){
        int positionX = (int)char.GetNumericValue(position[0]);
        int positionY = (int)char.GetNumericValue(position[1]);
        slots[slotCounter] = new Position(positionX, positionY, playerTurn);
        slotCounter++;
        print("player" + (playerTurn +1).ToString() + " played " + position );
        if(playerTurn == 0){
            playerChange = "X";
            p1Slots.Add(new Position(positionX, positionY, playerTurn));
            //print(p1Slots.Count);
            CheckWinner(p1Slots);
            playerTurn++;
            StartCoroutine(TimeToWaitBeforePlay());
        }
        else{
            playerChange = "O";
            p2Slots.Add(new Position(positionX, positionY, playerTurn));
            CheckWinner(p2Slots);
            playerTurn--;
            StartCoroutine(TimeToWaitBeforePlay());
        }
    }

    // Verificar quem ganhou através de funções para testar consecutividade que estão no Position.cs
    // 0 - ganhou o jogador 1
    // 1 - ganhou o jogador 2
    // 2 - empate
    // 3 - o jogo ainda não está decidido 
    public int CheckWinner(List<Position> pSlots){
        foreach(Position p in pSlots){
            //print("with:" + pSlots.Count.ToString());
            foreach(Position p2 in pSlots){
                if(p.IsConsecutive(p2)){
                    if(p.CheckWin(p2, pSlots)){
                        print("player" + (playerTurn+1).ToString() + " won!");
                        winPanel.SetActive(true);
                        winText.text = "Player" + (playerTurn + 1).ToString() + " won!";
                        return playerTurn;
                    }
                }
            }
        }
        if(slotCounter == (int)Math.Pow((difficulty+3),2)){
            winPanel.SetActive(true);
            winText.text = "Empate!";
            return 2;
        }
        return 3;
    }

    public void SetDifficulty(int dif){
        difficulty = dif;
    }

    //bloqueia o raycast durante 3 segundos para impedir que o segundo jogador jogue durante
    //o tempo que tem para memorizar a jogada anterior
    IEnumerator TimeToWaitBeforePlay()
    {
        canvas.blocksRaycasts = false;
        //Wait for 3 seconds
        yield return new WaitForSeconds(3);
        canvas.blocksRaycasts = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
