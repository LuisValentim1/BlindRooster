using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Game : MonoBehaviour
{
    public int difficulty;
    public Position[] slots;
    public int playerTurn;
    public int slotCounter;
    public List<Position> p1Slots;
    public List<Position> p2Slots;

    //public Position playedPositions;
    // Start is called before the first frame update
    
    public void StartGame()
    {
        this.transform.GetChild(difficulty).gameObject.SetActive(true);
        slots = new Position[(int)Math.Pow((difficulty+3),2)];
        slotCounter = 0;
    }

    public void SetPlay(string position){
        int positionX = (int)char.GetNumericValue(position[0]);
        int positionY = (int)char.GetNumericValue(position[1]);
        slots[slotCounter] = new Position(positionX, positionY, playerTurn);
        slotCounter++;
        print("player" + (playerTurn +1).ToString() + " played " + position );
        if(playerTurn == 0){
            p1Slots.Add(new Position(positionX, positionY, playerTurn));
            print(p1Slots.Count);
            CheckWinner(p1Slots);
            playerTurn++;
        }
        else{
            p2Slots.Add(new Position(positionX, positionY, playerTurn));
            CheckWinner(p2Slots);
            playerTurn--;
        }
    }

    public int CheckWinner(List<Position> pSlots){
        foreach(Position p in pSlots){
            //print("with:" + pSlots.Count.ToString());
            foreach(Position p2 in pSlots){
                if(p.IsConsecutive(p2)){
                    if(p.CheckWin(p2, pSlots)){
                        print("player" + (playerTurn+1).ToString() + " won!");
                        return playerTurn;
                    }
                }
            }
        }
        if(slotCounter == (int)Math.Pow((difficulty+3),2)){
            return 2;
        }
        return 3;
    }

    public void SetDifficulty(int dif){
        difficulty = dif;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
