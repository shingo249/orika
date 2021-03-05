using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(fileName = "CardEntity", menuName = "Create CardEntity")]
 
public class CardEntity : ScriptableObject
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
    public bool has_Effect;
    public GameObject EffectMethod;
}