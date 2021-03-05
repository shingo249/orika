using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour
{
    [SerializeField] CardController cardPrefab;
    [SerializeField] FieldCardController FieldcardPrefab;
    // Start is called before the first frame update

    public static FieldScript instance;
    public void Awake(){
        if(instance == null) instance = this;
    }
    public void Init(int cardID, int Fieldnum) {
        GameManager.instance.CreateFieldCard(cardID, Fieldnum);
    }
    
}
