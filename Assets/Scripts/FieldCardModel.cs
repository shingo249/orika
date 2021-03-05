using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 
public class FieldCardModel
{
    public int FieldcardID;
    public new string Fieldname;
    public int Fieldcost;
    public int Fieldcolor;
    public int Fieldatk;
    public int Fieldhp;
    public Sprite Fieldicon;
    public new string Fieldeffect;
    public bool has_Effect;
    public GameObject EffectMethod;
 
    public FieldCardModel(int cardID)
    {
        CardEntity cardEntity = Resources.Load<CardEntity>("CardEntityList/Card" + cardID);
 
        FieldcardID = cardEntity.cardID;
        Fieldname = cardEntity.name;
        Fieldcost = cardEntity.cost;
        Fieldcolor = cardEntity.colorcode; 
        Fieldatk = cardEntity.atk;
        Fieldhp = cardEntity.hp;
        Fieldicon = cardEntity.Fieldicon;
        Fieldeffect = cardEntity.effect;
        has_Effect = cardEntity.has_Effect;
        EffectMethod = cardEntity.EffectMethod;
    }
}
