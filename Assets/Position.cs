using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe para posição, não está associado a nenhum game object é só para poder guardar informação das jogadas numa estrutura com tudo o que é necessário 
// Tem uma coordenada x, uma coordenada y, quem jogou e quais as coordenadas adjacentes
// As coordenadas adjacentes servem para calcular sequencias de posições, para verificar vitórias 

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

    //Não está a ser utilizado
    public bool Equals(Position p){
        if(this.x == p.x && this.y == p.y){
            return true;
        }
        else{
    	    return false;
        } 
    }

    //Verificar se uma coordenada é adjacente no eixo do x
    public bool ConseqX(int x){
        foreach(int i in consecutiveXs){
            if(x==i){
                return true;
            }
        }
        return false;
    }

    //Verificar se uma coordenada é adjacente no eixo do y
    public bool ConseqY(int y){
        foreach(int i in consecutiveYs){
            if(y==i){
                return true;
            }
        }
        return false;
    }

    //Verificar se uma posição é verdadeiramente consecutiva a outro ponto 
    public bool IsConsecutive(Position p){
        //print("xy" + x.ToString() + y.ToString() + "pxy" + p.x.ToString() + p.y.ToString());
        //print(!( (p.x == x) && (p.y ==y) ));
        if(ConseqX(p.x) && ConseqY(p.y) && !( (p.x == x) && (p.y ==y) )){ //
            return true;
        }
        return false;
    }

    // Debug tool
    public string ToString(){
        return x.ToString() + y.ToString();
    }

    // Verificar se houve uma vitória através de duas posições e uma lista de posições, se existir na lista uma posição
    // que possui o mesmo declive em relação à segunda, que a segunda tem em relação à primeira, então são uma sequencia de 3
    // Utilizado para verificar vitória 
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
