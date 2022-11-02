using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridSpace : MonoBehaviour
{
    public Button slotSpace;
    public TMP_Text playerSideText;
    private Game game;

    private void Start()
    {
        game = GetComponentInParent<Game>();
    }

    //Texto do botão é alterado para X ou O, consoante o jogador a jogar
    public void SetPlayerSide()
    {
        playerSideText.text = game.playerChange;
        slotSpace.interactable = false;
        StartCoroutine(Waiter());
    }

    //Tempo que a jogada aparece no ecrã
    IEnumerator Waiter()
    { 
        //Wait for 3 seconds
        yield return new WaitForSeconds(3);
        playerSideText.gameObject.SetActive(false);
    }
}
