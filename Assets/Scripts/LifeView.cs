using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeView : MonoBehaviour
{
   [SerializeField] Text PlayerLife, EnemyLife;
   
   public static LifeView instance;
    private void Awake()
    {
        if(instance == null) instance = this;
    }
   public void showLife()
   {
       PlayerLife.text = GameManager.instance.PlayerLife.ToString();
       EnemyLife.text = GameManager.instance.EnemyLife.ToString();
       Debug.Log(GameManager.instance.EnemyLife.ToString());
   }
}
