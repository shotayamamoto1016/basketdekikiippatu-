using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject applePrefab;
    public GameObject bombPrefab;
    
    //アイテムを出す間隔
    float span = 1.0f;

    //経過時間
    float delta = 0;

    //爆弾を出す確率
    int ratio = 2;

    //アイテムの落下速度
    float speed = -0.03f;

    GameDirector director;

    //アイテムを出す間隔、落下速度、爆弾を出す確率を設定する関数
    public void SetParameter(float span, float speed, int ratio)
    {
        this.span = span;
        this.speed = speed;
        this.ratio = ratio;
    }

    private void Start()
    {
        this.director = GameObject.Find("GameDirector").GetComponent<GameDirector>();
    }

    
    void Update()
    {
        //stopFlagがtrueなら処理しない
        if (this.director.stopFlag == true)
        {
            return;
        }

        //経過時間を更新する
        this.delta += Time.deltaTime;

        //一定間隔でアイテムを生成する
        if(this.delta > this.span)
        {
            this.delta = 0;
            GameObject item;

            //1～10のランダムな数字を出す
            int dice = Random.Range(1, 11);

            //爆弾を生成
            if(dice <= this.ratio)
            {
                //爆弾のインスタンスを生成
                item = Instantiate(bombPrefab);
            }
            else
            {
                //リンゴのインスタンスを生成
                item = Instantiate(applePrefab);
            }
            
            //アイテムの出現位置をランダムに設定する
            float x = Random.Range(-1, 2);
            float z = Random.Range(-1, 2);
            item.transform.position = new Vector3(x, 4, z);
            item.GetComponent<ItemController>().dropSpeed = this.speed;
        }
        
    }
}
