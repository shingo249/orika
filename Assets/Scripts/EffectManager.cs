using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] GameObject ExpandedCard;

   public static EffectManager instance;
    public void Awake(){
        if(instance == null) instance = this;
    }

   public void ChooseMethod(FieldCardController triggerring)
   {
       Debug.Log("Accessed!");
       switch(triggerring.ID)
       {
           case 1:
            Effect1.instance.Method(triggerring);
            break;
       }
   }
}
