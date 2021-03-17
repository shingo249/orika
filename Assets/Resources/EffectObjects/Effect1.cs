using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect1 : MonoBehaviour
{
    public static Effect1 instance;
    public void Awake(){
        if(instance == null) instance = this;
    }
    
    //効果の種類判定 cip⇒is_cip, 常在型⇒is_always, エンド時⇒end_trigger, 開始時⇒start_trigger
    public void Method(FieldCardController triggerring){
        Debug.Log("発動！");
        if(triggerring.is_PlayerCard){
            int place = triggerring.Field_num;
            FieldCardController target = GameManager.EnemyFieldCardList[place];
            FieldCardController target1 = null;
            FieldCardController target2 = null;
            if(place-1>=0)target1 = GameManager.EnemyFieldCardList[place-1];
            if(place+1<=4)target1 = GameManager.EnemyFieldCardList[place+1];
            if(target!=null){
                target.DestroyCard(target);
                GameManager.EnemyFieldCardList[place]=null;
            }
            if(target1!=null)target1.Damage(3);
            if(target2!=null)target2.Damage(3);     
        }
        else{
            int place = triggerring.Field_num;
            FieldCardController target = GameManager.PlayerFieldCardList[place];
            FieldCardController target1 = null;
            FieldCardController target2 = null;
            if(place-1>=0)target1 = GameManager.PlayerFieldCardList[place-1];
            if(place+1<=4)target1 = GameManager.PlayerFieldCardList[place+1];
            if(target!=null)
            {
                target.DestroyCard(target);
                GameManager.PlayerFieldCardList[place]=null;
            }
            if(target1!=null)target1.Damage(3);
            if(target2!=null)target2.Damage(3); 
        }
    }
}
