using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 
public class CardModel
{
    public int cardID;
    public new string name;
    //色のコード：0=黒, 1=赤, 2=青, 3=緑, 4=白, 5=紫
    public int colorcode;
    public int cost;
    public int atk;
    public int hp;
    public Sprite icon;
    public Sprite Fieldicon;
    public Sprite cardFrame;
    public new string effect;
    
 
    public CardModel(int cardID)
    {
        CardEntity cardEntity = Resources.Load<CardEntity>("CardEntityList/Card" + cardID);
 
        cardID = cardEntity.cardID;
        name = cardEntity.name;
        colorcode = cardEntity.colorcode;
        cost = cardEntity.cost;
        atk = cardEntity.atk;
        hp = cardEntity.hp;
        icon = cardEntity.icon;
        Fieldicon = cardEntity.Fieldicon;
        cardFrame = cardEntity.cardFrame;
        effect = cardEntity.effect;
    }
}