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
        //print("xy" + x.ToString() + y.ToString() + "pxy" + p.x.ToString() + p.y.ToString());
        //print(!( (p.x == x) && (p.y ==y) ));
        if(ConseqX(p.x) && ConseqY(p.y) && !( (p.x == x) && (p.y ==y) )){ //
            return true;
        }
        return false;
    }

    public string ToString(){
        return x.ToString() + y.ToString();
    }

    public bool CheckWin(Position p2, List<Position> pList){
        //print("we checkin");
        foreach(Position p3 in pList){
            if( (p3.x-p2.x) == (p2.x-x) && (p3.y-p2.y) == (p2.y-y)){ //&& !((p3.x == x && p3.y ==y) || (p3.x == p2.x && p3.y == p2.y)
                return true;
            }
        }
    return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
