using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect1 : MonoBehaviour
{
    public static string timing = "is_cip";
    //効果の種類判定 cip⇒is_cip, 常在型⇒is_always, エンド時⇒end_trigger, 開始時⇒start_trigger
    void Start()
    {
        if(timing.Equals("is_cip"))
        {
            bool temp = GameManager.isPlayerTurn;
            //味方用
            for(int i = 0; i < 5; i++)
            {
                if(GameManager.PlayerFieldCardList[i]!=null)
                {
                    FieldCardController triggering = GameManager.PlayerFieldCardList[i];
                    //Debug.Log(triggering.is_summoned);
                    if(triggering.is_summoned&&GameManager.EnemyFieldCardList[i]!=null&&triggering.is_PlayerCard)
                    {
                        //Debug.Log("発動");
                        FieldCardController target = GameManager.EnemyFieldCardList[i];
                        int damage = target.Fieldmodel.Fieldhp; 
                        target.Damage(damage);
                    }
                    GameManager.PlayerFieldCardList[i].is_summoned = false;
                }
                if(GameManager.EnemyFieldCardList[i]!=null)
                {
                    FieldCardController triggering = GameManager.EnemyFieldCardList[i];
                    if(triggering.is_summoned&&GameManager.PlayerFieldCardList[i]!=null&&!(triggering.is_PlayerCard))
                    {
                        //Debug.Log("発動！");
                        FieldCardController target = GameManager.PlayerFieldCardList[i];
                        int damage = target.Fieldmodel.Fieldhp; 
                        target.Damage(damage);
                    }
                    GameManager.EnemyFieldCardList[i].is_summoned = false;
                }
            }
        Destroy(this.gameObject);
        }   
    }
}
