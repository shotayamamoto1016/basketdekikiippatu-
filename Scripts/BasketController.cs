using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;


public class BasketController : MonoBehaviour
{
    //SE
    public AudioClip appleSE;
    public AudioClip bombSE;

    //AudioSourceコンポーネントを入れる変数
    AudioSource aud;
    GameDirector director;
   
    void Start()
    {

        Application.targetFrameRate = 60;

        //AudioSourceコンポーネントを取得
        this.aud = GetComponent<AudioSource>();

        this.director = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //リンゴに触れたら
        if (other.gameObject.tag == "Apple")
        {
            //リンゴSEを鳴らす
            this.aud.PlayOneShot(this.appleSE);
            
            //GameDirectorのGetAppleメソッドを呼び出す
            this.director.GetComponent<GameDirector>().GetApple(other.transform.position);
            
            
           
        }

        //爆弾に触れたら
        else if (other.gameObject.tag == "Bomb")
        {
            //爆弾SEを鳴らす
            this.aud.PlayOneShot(this.bombSE);

            //GameDirectorのGetBombメソッドを呼び出す
            this.director.GetComponent<GameDirector>().GetBomb(other.transform.position);
            
        }

        //触れたオブジェクトを破壊する
        Destroy(other.gameObject);
            

    }

    
    void Update()
    {
        //stopFlagがtrueになったら処理をしない
        if (this.director.stopFlag == true)
        {
            return;
        }
        
            //左クリックされたら
            if (Input.GetMouseButtonDown(0))
            {
                //タップされた座標(スクリーン座標)をワールド座標に変換して代入する
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                //Physics.Raycastメソッドを使って光線がstageに当たったかどうか調べる
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                　　
                    float x = Mathf.RoundToInt(hit.point.x);
                    float z = Mathf.RoundToInt(hit.point.z);
                    transform.position = new Vector3(x, 0, z);
                }




            }

        
       }
    }
