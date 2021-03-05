using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CardController : MonoBehaviour
{

    public CardView view; // カードの見た目の処理
    public CardModel model; // カードのデータを処理
    public int ID = 0;

    public static CardController instance;
    private void Awake()
    {
        if(instance == null) instance = this;
        view = GetComponent<CardView>();
    }
 
    public void Init(int cardID) // カードを生成した時に呼ばれる関数
    {
        ID = cardID;
        model = new CardModel(cardID); // カードデータを生成
        view.Show(model); // 表示
    }
    
    public void DestroyCard(CardController card)
    {
        Destroy(card.gameObject);
    }
}