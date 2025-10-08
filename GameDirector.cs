using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    GameObject timerText;
    GameObject pointText;

    //制限時間
    float time = 30.0f;

    //スコア
    int point = 0;

    GameObject generator;

    //ReturnButtonを宣言
    public Button ReturnButton;

    //爆弾エフェクト
    public GameObject BombEffectPrefab;

    //リンゴ獲得テキスト
    public GameObject AppleTextPrefab;

    // 連続で獲得したリンゴの数
    private int appleCount = 0;
    private int bombCount = 0;

    public GameObject GameOver;
    //ゲームを止めるフラグ
    public bool stopFlag;

    //リンゴを獲得したときに呼ばれる関数
    public void GetApple(Vector3 position)
    {
        //スコアを加算する
        this.point += 100;

        if (AppleTextPrefab != null)
        {
            //instantiateメソッドはAppleTextPrefabのクローンを生成する
            //Quaternion.identityは向きや回転なしを意味する
            Instantiate(AppleTextPrefab, position, Quaternion.Euler(90, 0, 0));
        }
            appleCount++;
        
        //3回連続でリンゴを取得するとスコアが3倍になる
       if(appleCount == 3)
        {
            this.point *= 3;

            //カウント初期化
            appleCount = 0;
        }

        bombCount = 0;
    }

    //爆弾を取得したときに呼ばれる関数
    public void GetBomb(Vector3 position)
    {
        //スコアを半分にする
        this.point /= 2;

        bombCount++;
        
        if(BombEffectPrefab != null)
        {
            //instantiateメソッドはBombEffectprefabのクローンを生成する
            //Quaternion.identityは向きや回転なしを意味する
           GameObject Bombeffect = Instantiate(BombEffectPrefab, position, Quaternion.identity);
            float Duration = Bombeffect.GetComponent<ParticleSystem>().main.duration;
            Destroy(Bombeffect,Duration);
        }

        //2回連続で取得するとゲームオーバー
        if (bombCount == 2)
        {
            //ゲームオーバー表示
            GameOver.SetActive(true);
            stopFlag = true;
            ReturnButton.gameObject.SetActive(true);
        }

        appleCount = 0;
    }
    
    void Start()
    {
        this.timerText = GameObject.Find("Time");
        this.pointText = GameObject.Find("Point");
        this.generator = GameObject.Find("ItemGenerator");
        //ヒエラルキーからBombオブジェクトを取得する
        //this.bomb = GameObject.Find("bomb");
        //ReturnButtonを非表示にする
        ReturnButton.gameObject.SetActive(false);
        ReturnButton.onClick.AddListener(ReturnGame);
        GameOver.SetActive(false);
    }

    
    void Update()
    {
        //stopFlagがtrueになると処理しない
        if (stopFlag == true)
        {
            this.timerText.GetComponent<TextMeshProUGUI>().text = this.time.ToString("F1");
            this.pointText.GetComponent<TextMeshProUGUI>().text = this.point.ToString() + " point";
            return;
        }
       
        //制限時間を減らす
        this.time -= Time.deltaTime;

        //制限時間が0以下になったら0にする
        if(this.time < 0)
        {
            this.time = 0;
            this.generator.GetComponent<ItemGenerator>().SetParameter(10000.0f, 0, 1);
            //時間が0になったらReturnButtonを表示する
            ReturnButton.gameObject.SetActive(true);
           
            
        }

        //時間によってアイテムの生成パラメータを変える
        else if (0 < this.time && this.time < 4)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.3f, -0.06f, 0);
        }
        else if (4 <= this.time && this.time < 12)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.5f, -0.05f, 6);
        }
        else if (12 <= this.time && this.time < 23)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.8f, -0.04f, 4);
               
        }
        else if (23 <= this.time && this.time < 30)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(1.0f, -0.03f, 2);
        }
        
        //テキストを更新する
        this.timerText.GetComponent<TextMeshProUGUI>().text = this.time.ToString("F1");
        this.pointText.GetComponent<TextMeshProUGUI>().text = this.point.ToString()  + " point";

    }

    void ReturnGame()
    {
        //タイトルシーンへ
        SceneManager.LoadScene("TitleScene");
    }
}
