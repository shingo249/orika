using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class FieldCardView : MonoBehaviour
{
    [SerializeField] Text atkText, hpText;
    [SerializeField] Image iconImage;
 
    public void FieldShow(FieldCardModel FieldCardModel) // cardModelのデータ取得と反映
    {
        atkText.text = FieldCardModel.Fieldatk.ToString();
        hpText.text = FieldCardModel.Fieldhp.ToString();
        iconImage.sprite = FieldCardModel.Fieldicon;
    }

}