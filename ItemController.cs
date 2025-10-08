using UnityEngine;

public class ItemController : MonoBehaviour
{
    //アイテムの落下速度
    public float dropSpeed = -0.03f;
    

   
    void Update()
    {
        //アイテムを落下させる
        transform.Translate(0, this.dropSpeed, 0);

        //アイテムが画面外から出たら消去する
        if (transform.position.y < -1.0f)
        {
            Destroy(gameObject);
        }
    }
}
