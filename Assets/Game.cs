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
    
    public void Start()
    {
        this.transform.GetChild(difficulty).gameObject.SetActive(true);
        slots = new Position[(int)Math.Pow((difficulty+3),2)];
        slotCounter = 0;
    }

    public void Play(int positionX, int positionY){
        slots[slotCounter] = new Position(positionX, positionY, playerTurn);
        slotCounter++;
        if(playerTurn == 0){
            p1Slots.Add(new Position(positionX, positionY, playerTurn));
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
            List<Position> pList = new List<Position>{p};
            List<Position> withoutP1 = p1Slots.Except(pList).ToList();
            if(withoutP1.Count > 0){
                foreach(Position p2 in withoutP1){
                    if(p.IsConsecutive(p2)){
                        List<Position> p2List = new List<Position>{p2};
                        List<Position> withoutP2 = p1Slots.Except(p2List).ToList();
                        if(withoutP2.Count >0){
                            foreach(Position p3 in withoutP2){
                                if(p2.IsConsecutive(p3)){
                                    return playerTurn;
                                }
                            }
                        }
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
