using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] CardController cardPrefab;
    [SerializeField] FieldCardController FieldCardPrefab;
    [SerializeField] Transform playerHand;
    [SerializeField] Transform PlayerField_0, PlayerField_1, PlayerField_2, PlayerField_3, PlayerField_4, EnemyField_0, EnemyField_1, EnemyField_2, EnemyField_3, EnemyField_4;
    [SerializeField] GameObject Win_Lose_Panel;

    bool isPlayerTurn = true; //ターン制御
    int MaxPlay = 1;
    int PlayAuthCount = 0;//行動権カウンター
    public int[] Mana = {0,0,0,0,0};//残存マナリスト
    List<int> deck = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11};  //デッキリスト
    bool[] ColorBool = {false,false,false,false,false};//デッキ内の色判定
    List <int> Colornum = new List<int>();//デッキ内の色コードを格納

    //フィールドの配列（カードコードを格納）
    public FieldCardController[] PlayerFieldCardList = new FieldCardController[5];
    public FieldCardController[] EnemyFieldCardList = new FieldCardController[5];

    //ライフ
    public int PlayerLife = 0;
    public int EnemyLife = 0;

    //勝ち負け判定関係
    public bool PlayerWin = false;
    public bool EnemyWin = false;

    //墓地関連
    public List <int> PlayerGraveyard = new List<int>();

    //インスタンス化
    public static GameManager instance;
    public void Awake(){
        if(instance == null) instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerLife<=0){
            EnemyWin = true;
        }
        if(EnemyLife<=0){
            PlayerWin = true;
        }
        if(PlayerWin&&!EnemyWin)
        {
            Debug.Log("You Win!!");
            Win_Lose_Panel.SetActive(true);
            Win_Lose_Script.instance.Init(PlayerWin, EnemyWin);
            PlayerWin = false;
        } 
        else if(!PlayerWin&&EnemyWin)
        {
            Debug.Log("You Lose...");
            Win_Lose_Panel.SetActive(true);
            Win_Lose_Script.instance.Init(PlayerWin, EnemyWin);
            EnemyWin = false;
        } 
    }

    void DelayMethod()
    {
        Debug.Log("Delay call");
    }

    void CreateCard(int i, Transform place){
        CardController card = Instantiate(cardPrefab, playerHand);
        card.Init(i);
    }

    public void CreateFieldCard(int cardID, int Fieldnum)
    {
        Transform Field = PlayerField_0;
        if(Fieldnum == 0) Field = PlayerField_0;
        else if(Fieldnum == 1) Field = PlayerField_1;
        else if(Fieldnum == 2) Field = PlayerField_2;
        else if(Fieldnum == 3) Field = PlayerField_3;
        else if(Fieldnum == 4) Field = PlayerField_4;
        FieldCardController card = Instantiate(FieldCardPrefab, Field);
        card.is_PlayerCard = true;
        card.FieldInit(cardID);
        PlayerFieldCardList[Fieldnum] = card;
    }

    public void EnemyCreateFieldCard()//NPC用
    {
        int vacant = -1;
        for(int i = 0; i < 5; i++){
            if(EnemyFieldCardList[i]==null)
            {
                vacant = i;
                break;
            }
        }
        if(vacant!=-1){
            Transform Field = EnemyField_0;
            if(vacant == 0) Field = EnemyField_0;
            else if(vacant == 1) Field = EnemyField_1;
            else if(vacant == 2) Field = EnemyField_2;
            else if(vacant == 3) Field = EnemyField_3;
            else if(vacant == 4) Field = EnemyField_4;
            FieldCardController card = Instantiate(FieldCardPrefab, Field);
            //敵のクリーチャーをランダムで決定
            //int ID = Random.Range(1,12);
            //card.FieldInit(ID);
            //実験用に敵側のカードを指定する
            card.FieldInit(1);
            card.is_PlayerCard = false;
            EnemyFieldCardList[vacant]=card;
        }
    }

    void StartGame() // 初期値の設定 
    {
        // 初期手札を配る
        SetDeck();
        //ここでデッキ内カードの色の種類数をメモ
        for(int i = 0; i < 5; i++)
        {
            ColorBool[i]=true;
            Colornum.Add(i);
        }
        //初期マナ充填
        GetMana();
        GetMana();
        GetMana();
        //ライフ表示
        PlayerLife = 20;
        EnemyLife = 40;
        LifeView.instance.showLife();
        // ターンの決定
        TurnCalc();
    }

    void AddCard(Transform hand) // カードを追加
    {
        // デッキがないなら引かない
        if (deck.Count == 0)
        {
            return;
        }
 
        // デッキの一番上のカードを抜き取り、手札に加える
        int cardID = deck[0];
        deck.RemoveAt(0);
        CreateCard(cardID, hand);
    }

    void SetDeck() // 手札を3枚配る
    {
        for (int i = 0; i < 11; i++)
        {
            AddCard(playerHand);
        }
        PlayAuthCount=MaxPlay;
        Debug.Log("行動権："+PlayAuthCount);
    }

    void TurnCalc() // ターンを管理する
    {
        if (isPlayerTurn)
        {
            PlayAuthCount=MaxPlay;//行動権回復
            PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }
    }

     public void CountCheckTurn() // ターン終了するか否か処理
    {
        PlayAuthCount--;
        if(isPlayerTurn) Debug.Log("行動権："+PlayAuthCount);
        if(PlayAuthCount<=0){
            //ターン終了前にバトル
            for(int i = 0; i < 5; i++){
                if(isPlayerTurn)
                {
                    if(PlayerFieldCardList[i]!=null&&EnemyFieldCardList[i]!=null)
                    {
                        CardBattle(PlayerFieldCardList[i], EnemyFieldCardList[i]);
                        if(EnemyFieldCardList[i]=null)Debug.Log("倒した！");
                    }
                    else if(PlayerFieldCardList[i]!=null&&EnemyFieldCardList[i]==null)
                    {
                        Debug.Log("ダイレクト！");
                        FieldCardController AttackCard = PlayerFieldCardList[i];
                        int damage = AttackCard.Fieldmodel.Fieldatk;
                        EnemyLife-=damage;
                        LifeView.instance.showLife();
                    }
                }
            }
            isPlayerTurn = !isPlayerTurn; // ターンを逆にする
            //Invoke("DelayMethod", 3.5f);
            TurnCalc(); // ターンを相手に回す
        }
    }

    public void LevelUp()
    {
        if(MaxPlay<3)
        {
            PlayerLife+=3;
            MaxPlay++;
            GetMana();
            GetMana();
            LifeView.instance.showLife();
            CountCheckTurn();
        }
    }
    public void GetMana()
    {
        int l = Colornum.Count;
        int add = Random.Range(0,l);//供給するマナの色をランダムに指定
        Mana[add]++;
        ManaView.instance.showMana();
    }

    public void GetManaButton()//マナボタンにアタッチ
    {
        GetMana();
        CountCheckTurn();
    }
 
    void PlayerTurn()
    {
        Debug.Log("Playerのターン");
        Debug.Log("行動権："+PlayAuthCount);
    }
 
    void EnemyTurn()
    {
        Debug.Log("Enemyのターン");
        EnemyCreateFieldCard();
        if(!isPlayerTurn)
        {
            for(int i = 0; i < 5; i++){
                if(PlayerFieldCardList[i]!=null&&EnemyFieldCardList[i]!=null)
                {
                    Debug.Log("バトル2");
                    CardBattle(EnemyFieldCardList[i], PlayerFieldCardList[i]);
                }
                else if(PlayerFieldCardList[i]==null&&EnemyFieldCardList[i]!=null)
                {
                    Debug.Log("ダイレクト！");
                    FieldCardController AttackCard = EnemyFieldCardList[i];
                    int damage = AttackCard.Fieldmodel.Fieldatk;
                    PlayerLife-=damage;
                    LifeView.instance.showLife();
                }
            }
        }
        isPlayerTurn = !isPlayerTurn; // ターンを逆にする
        //Invoke("DelayMethod", 3.5f);
        TurnCalc(); // ターンを相手に回す
    }
    void CardBattle(FieldCardController Attack, FieldCardController Attacked)
    {
        int damage = Attack.Fieldmodel.Fieldatk;      
        Attacked.Damage(damage);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    //墓地回収ボタン
    public void GraveButton()
    {
        if(PlayerGraveyard.Count>0)
        {
            int l = PlayerGraveyard.Count;
            for(int i = 0; i < l; i++){
                int ID = PlayerGraveyard[0];
                PlayerGraveyard.RemoveAt(0);
                CreateCard(ID, playerHand);
                if(PlayerLife-1>0) PlayerLife--;
                else PlayerLife=1;
                LifeView.instance.showLife();
                CountCheckTurn();
            }
        }
    }
}
