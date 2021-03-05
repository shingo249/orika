using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win_Lose_Script : MonoBehaviour
{
    [SerializeField] Text WinorLose;
    [SerializeField] Image back;
    [SerializeField] Sprite Win, Lose;

    public static Win_Lose_Script instance;
    public void Awake(){
        if(instance == null) instance = this;
    }
    public void Init(bool Player, bool Enemy)
    {
        if(Player&&!Enemy)
        {
            WinorLose.text = "You Win!!";
            back.sprite = Win;
        }
        else if(!Player&&Enemy)
        {
            WinorLose.text = "You Lose...";
            back.sprite = Lose;
        }
    }
}
