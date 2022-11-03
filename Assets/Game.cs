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
    public List<Position> verifyPosition;
    public string playerChange; //string para guardar icon do jogador
    public CanvasGroup canvas;
    public GameObject winPanel;
    public TMP_Text winText;
    public GameObject[] grids;
   // private Position lastPosition = null;

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
        Position currentPos = new Position(positionX, positionY, playerTurn);
        CheckLossByRepeat(currentPos);
        slots[slotCounter] = currentPos;
        verifyPosition.Add(currentPos);

        slotCounter++;
        print("player" + (playerTurn +1).ToString() + " played " + position );

        if(playerTurn == 0){
            if (p1Slots.Count > 0)
            {
                Debug.Log(p1Slots.Last());
                Debug.Log(verifyPosition.Last());
            }
            playerChange = "X";
            //CheckPositionEqualinOneList(p1Slots);
            p1Slots.Add(new Position(positionX, positionY, playerTurn));
            //print(p1Slots.Count);
            //CheckPositionEqual(p1Slots, p2Slots);
            CheckWinner(p1Slots);
            playerTurn++;
            StartCoroutine(TimeToWaitBeforePlay());
        }
        else{
            playerChange = "O";
            p2Slots.Add(new Position(positionX, positionY, playerTurn));
            //CheckPositionEqual(p2Slots, p1Slots);
            //CheckPositionEqualinOneList(p2Slots);
            CheckWinner(p2Slots);
            playerTurn--;
            StartCoroutine(TimeToWaitBeforePlay());
        }
        print(slots);
    }

    //public void CheckPositionEqualinOneList(List<Position> pSlots)
    //{
    //    //if(pSlots.Count > 0)
    //    //{
    //    //    var lastPosition = pSlots[^1];
    //    //    if (pSlots.Equals(lastPosition))
    //    //    {
    //    //        winPanel.SetActive(true);
    //    //        winText.text = "Player" + (playerTurn + 1).ToString() + " Lose!";
    //    //    }
    //    //var lastPosition = pSlots[^1];
    //    foreach (Position p in pSlots)
    //        {
    //                if (p.Equals(lastPosition))
    //                {
    //                    winPanel.SetActive(true);
    //                    winText.text = "Player" + (playerTurn + 1).ToString() + " Lose!";
    //                }
    //        }
    //}

    //verificar se os jogadores jogam na mesma posição
    public void CheckPositionEqual(List<Position> pSlots, List<Position> pSlots2)
    {
        foreach (Position p in pSlots)
        {
            //print("with:" + pSlots.Count.ToString());
            foreach (Position p2 in pSlots2)
            {
                if (p.Equals(p2))
                {
                   // print("player" + (playerTurn + 1).ToString() + " won!");
                    winPanel.SetActive(true);
                    winText.text = "Player" + (playerTurn + 1).ToString() + " Lose!";
                }
            }
        }
    }

    public void CheckLossByRepeat(Position cur){
        foreach(Position p in verifyPosition){
            if(cur.Equals(p)){
                print("oi");
                if(playerTurn == 0){
                    winPanel.SetActive(true);
                    winText.text = "Player2 won!";
                }
                else{
                    winPanel.SetActive(true);
                    winText.text = "Player1 won!";
                }
            }
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
        yield return new WaitForSeconds(1);
        canvas.blocksRaycasts = true;
    }

    public void RestartGame()
    {
        p1Slots.Clear();
        p2Slots.Clear();
        verifyPosition.Clear();
        playerTurn = 0;
        for(int i=0; i < grids.Length; i++)
        {
            grids[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
