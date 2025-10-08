using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject applePrefab;
    public GameObject bombPrefab;
    
    //�A�C�e�����o���Ԋu
    float span = 1.0f;

    //�o�ߎ���
    float delta = 0;

    //���e���o���m��
    int ratio = 2;

    //�A�C�e���̗������x
    float speed = -0.03f;

    GameDirector director;

    //�A�C�e�����o���Ԋu�A�������x�A���e���o���m����ݒ肷��֐�
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
        //stopFlag��true�Ȃ珈�����Ȃ�
        if (this.director.stopFlag == true)
        {
            return;
        }

        //�o�ߎ��Ԃ��X�V����
        this.delta += Time.deltaTime;

        //���Ԋu�ŃA�C�e���𐶐�����
        if(this.delta > this.span)
        {
            this.delta = 0;
            GameObject item;

            //1�`10�̃����_���Ȑ������o��
            int dice = Random.Range(1, 11);

            //���e�𐶐�
            if(dice <= this.ratio)
            {
                //���e�̃C���X�^���X�𐶐�
                item = Instantiate(bombPrefab);
            }
            else
            {
                //�����S�̃C���X�^���X�𐶐�
                item = Instantiate(applePrefab);
            }
            
            //�A�C�e���̏o���ʒu�������_���ɐݒ肷��
            float x = Random.Range(-1, 2);
            float z = Random.Range(-1, 2);
            item.transform.position = new Vector3(x, 4, z);
            item.GetComponent<ItemController>().dropSpeed = this.speed;
        }
        
    }
}
