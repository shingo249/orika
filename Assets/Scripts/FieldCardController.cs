using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class FieldCardController : MonoBehaviour
{
    public FieldCardView Fieldview; // カードの見た目の処理
    public FieldCardModel Fieldmodel; // カードのデータを処理
    public int ID;
    public bool is_PlayerCard = false;
    public static FieldCardController instance;
    private void Awake()
    {
        if(instance == null) instance = this;
        Fieldview = GetComponent<FieldCardView>();     
    }
 
    public void FieldInit(int cardID) // カードを生成した時に呼ばれる関数
    {
        Fieldmodel = new FieldCardModel(cardID); // カードデータを生成
        Fieldview.FieldShow(Fieldmodel); // 表示
        ID = Fieldmodel.FieldcardID;
    }

    public void DestroyCard(FieldCardController card)
    {
        if(is_PlayerCard) GameManager.instance.PlayerGraveyard.Add(ID);
        Destroy(card.gameObject);
    }

    public void Damage(int d){
        Fieldmodel.Fieldhp-=d;
        if(Fieldmodel.Fieldhp<=0)
        {
            DestroyCard(this);
        }
        else
        {
            Fieldview.FieldShow(Fieldmodel); // 表示
        }
    }
}