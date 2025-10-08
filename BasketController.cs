using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;


public class BasketController : MonoBehaviour
{
    //SE
    public AudioClip appleSE;
    public AudioClip bombSE;

    //AudioSource�R���|�[�l���g������ϐ�
    AudioSource aud;
    GameDirector director;
   
    void Start()
    {

        Application.targetFrameRate = 60;

        //AudioSource�R���|�[�l���g���擾
        this.aud = GetComponent<AudioSource>();

        this.director = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //�����S�ɐG�ꂽ��
        if (other.gameObject.tag == "Apple")
        {
            //�����SSE��炷
            this.aud.PlayOneShot(this.appleSE);
            
            //GameDirector��GetApple���\�b�h���Ăяo��
            this.director.GetComponent<GameDirector>().GetApple(other.transform.position);
            
            
           
        }

        //���e�ɐG�ꂽ��
        else if (other.gameObject.tag == "Bomb")
        {
            //���eSE��炷
            this.aud.PlayOneShot(this.bombSE);

            //GameDirector��GetBomb���\�b�h���Ăяo��
            this.director.GetComponent<GameDirector>().GetBomb(other.transform.position);
            
        }

        //�G�ꂽ�I�u�W�F�N�g��j�󂷂�
        Destroy(other.gameObject);
            

    }

    
    void Update()
    {
        //stopFlag��true�ɂȂ����珈�������Ȃ�
        if (this.director.stopFlag == true)
        {
            return;
        }
        
            //���N���b�N���ꂽ��
            if (Input.GetMouseButtonDown(0))
            {
                //�^�b�v���ꂽ���W(�X�N���[�����W)�����[���h���W�ɕϊ����đ������
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                //Physics.Raycast���\�b�h���g���Č�����stage�ɓ����������ǂ������ׂ�
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                �@�@
                    float x = Mathf.RoundToInt(hit.point.x);
                    float z = Mathf.RoundToInt(hit.point.z);
                    transform.position = new Vector3(x, 0, z);
                }




            }

        
       }
    }
