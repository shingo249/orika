using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class CardView : MonoBehaviour
{
    [SerializeField] Text nameText, costText, atkText, hpText;
    [SerializeField] Image iconImage, cardFrameImage;
 
    public void Show(CardModel cardModel) // cardModelのデータ取得と反映
    {
        nameText.text = cardModel.name;
        atkText.text = cardModel.atk.ToString();
        hpText.text = cardModel.hp.ToString();
        costText.text = cardModel.cost.ToString();
        iconImage.sprite = cardModel.icon;
        cardFrameImage.sprite = cardModel.cardFrame;
    }
}