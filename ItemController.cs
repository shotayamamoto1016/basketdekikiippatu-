using UnityEngine;

public class ItemController : MonoBehaviour
{
    //�A�C�e���̗������x
    public float dropSpeed = -0.03f;
    

   
    void Update()
    {
        //�A�C�e���𗎉�������
        transform.Translate(0, this.dropSpeed, 0);

        //�A�C�e������ʊO����o�����������
        if (transform.position.y < -1.0f)
        {
            Destroy(gameObject);
        }
    }
}
