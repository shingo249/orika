using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
 
// フィールドにアタッチするクラス
public class DropPlace : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject cardPrefab;
    [SerializeField] int Fieldnum;

    public void OnDrop(PointerEventData eventData) // ドロップされた時に行う処理
    {
        CardMovement card = eventData.pointerDrag.GetComponent<CardMovement>(); // ドラッグしてきた情報からCardMovementを取得
        CardController cardC = eventData.pointerDrag.GetComponent<CardController>(); 
        if (card != null&&GameManager.instance.PlayerFieldCardList[Fieldnum]==null) // もしカードがあれば、
        {
            int cardID = cardC.ID;
            card.cardParent = this.transform; // カードの親要素を自分（アタッチされてるオブジェクト）にする
            cardC.DestroyCard(cardC);
            FieldScript.instance.Init(cardID,Fieldnum);
            GameManager.instance.CountCheckTurn();
        }
    }
}