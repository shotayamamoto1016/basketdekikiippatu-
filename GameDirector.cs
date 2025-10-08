using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    GameObject timerText;
    GameObject pointText;

    //��������
    float time = 30.0f;

    //�X�R�A
    int point = 0;

    GameObject generator;

    //ReturnButton��錾
    public Button ReturnButton;

    //���e�G�t�F�N�g
    public GameObject BombEffectPrefab;

    //�����S�l���e�L�X�g
    public GameObject AppleTextPrefab;

    // �A���Ŋl�����������S�̐�
    private int appleCount = 0;
    private int bombCount = 0;

    public GameObject GameOver;
    //�Q�[�����~�߂�t���O
    public bool stopFlag;

    //�����S���l�������Ƃ��ɌĂ΂��֐�
    public void GetApple(Vector3 position)
    {
        //�X�R�A�����Z����
        this.point += 100;

        if (AppleTextPrefab != null)
        {
            //instantiate���\�b�h��AppleTextPrefab�̃N���[���𐶐�����
            //Quaternion.identity�͌������]�Ȃ����Ӗ�����
            Instantiate(AppleTextPrefab, position, Quaternion.Euler(90, 0, 0));
        }
            appleCount++;
        
        //3��A���Ń����S���擾����ƃX�R�A��3�{�ɂȂ�
       if(appleCount == 3)
        {
            this.point *= 3;

            //�J�E���g������
            appleCount = 0;
        }

        bombCount = 0;
    }

    //���e���擾�����Ƃ��ɌĂ΂��֐�
    public void GetBomb(Vector3 position)
    {
        //�X�R�A�𔼕��ɂ���
        this.point /= 2;

        bombCount++;
        
        if(BombEffectPrefab != null)
        {
            //instantiate���\�b�h��BombEffectprefab�̃N���[���𐶐�����
            //Quaternion.identity�͌������]�Ȃ����Ӗ�����
           GameObject Bombeffect = Instantiate(BombEffectPrefab, position, Quaternion.identity);
            float Duration = Bombeffect.GetComponent<ParticleSystem>().main.duration;
            Destroy(Bombeffect,Duration);
        }

        //2��A���Ŏ擾����ƃQ�[���I�[�o�[
        if (bombCount == 2)
        {
            //�Q�[���I�[�o�[�\��
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
        //�q�G�����L�[����Bomb�I�u�W�F�N�g���擾����
        //this.bomb = GameObject.Find("bomb");
        //ReturnButton���\���ɂ���
        ReturnButton.gameObject.SetActive(false);
        ReturnButton.onClick.AddListener(ReturnGame);
        GameOver.SetActive(false);
    }

    
    void Update()
    {
        //stopFlag��true�ɂȂ�Ə������Ȃ�
        if (stopFlag == true)
        {
            this.timerText.GetComponent<TextMeshProUGUI>().text = this.time.ToString("F1");
            this.pointText.GetComponent<TextMeshProUGUI>().text = this.point.ToString() + " point";
            return;
        }
       
        //�������Ԃ����炷
        this.time -= Time.deltaTime;

        //�������Ԃ�0�ȉ��ɂȂ�����0�ɂ���
        if(this.time < 0)
        {
            this.time = 0;
            this.generator.GetComponent<ItemGenerator>().SetParameter(10000.0f, 0, 1);
            //���Ԃ�0�ɂȂ�����ReturnButton��\������
            ReturnButton.gameObject.SetActive(true);
           
            
        }

        //���Ԃɂ���ăA�C�e���̐����p�����[�^��ς���
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
        
        //�e�L�X�g���X�V����
        this.timerText.GetComponent<TextMeshProUGUI>().text = this.time.ToString("F1");
        this.pointText.GetComponent<TextMeshProUGUI>().text = this.point.ToString()  + " point";

    }

    void ReturnGame()
    {
        //�^�C�g���V�[����
        SceneManager.LoadScene("TitleScene");
    }
}
