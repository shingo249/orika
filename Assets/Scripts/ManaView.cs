using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaView : MonoBehaviour
{
   [SerializeField] Text Red, Blue, Green, White, Purple;
   
   public static ManaView instance;
    private void Awake()
    {
        if(instance == null) instance = this;
    }
   public void showMana()
   {
       Red.text = GameManager.instance.Mana[0].ToString();
       Blue.text = GameManager.instance.Mana[1].ToString();
       Green.text = GameManager.instance.Mana[2].ToString();
       White.text = GameManager.instance.Mana[3].ToString();
       Purple.text = GameManager.instance.Mana[4].ToString();
   }
}
