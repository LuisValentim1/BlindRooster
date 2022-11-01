using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{

    public int x;
    public int y;
    public int playerHolding;
    public int[] consecutiveXs;
    public int[] consecutiveYs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Position(int xi, int yi, int player){
        x=xi;
        y=yi;
        playerHolding=player;
        consecutiveXs = new int[3]{x-1, x, x+1};
        consecutiveYs = new int[3]{y-1, y, y+1};


    }

    public bool Equals(Position p){
        if(this.x == p.x && this.y == p.y){
            return true;
        }
        else{
    	    return false;
        } 
    }

    public bool ConseqX(int x){
        foreach(int i in consecutiveXs){
            if(x==i){
                return true;
            }
        }
        return false;
    }

    public bool ConseqY(int y){
        foreach(int i in consecutiveYs){
            if(y==i){
                return true;
            }
        }
        return false;
    }

    public bool IsConsecutive(Position p){
        if(ConseqX(p.x) && ConseqY(p.y)){
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
