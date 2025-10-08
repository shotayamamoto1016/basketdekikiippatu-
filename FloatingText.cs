using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    // �㏸���x
    public float floatSpeed = 1.0f;
    // �t�F�[�h�A�E�g����
    public float fadeDuration = 1.0f; 

    private TextMeshPro AppleTextPrefab;
    private Color originalColor;
    private float timer = 0f;

    void Start()
    {
        AppleTextPrefab = GetComponent<TextMeshPro>();
        originalColor = AppleTextPrefab.color;
    }

    void Update()
    {
        // �㏸������
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        // ���X�ɓ����ɂ���
        timer += Time.deltaTime;
        //Mathf.Lerp��originalColor����0�܂�timer / fadeDuration�̊����Ői�ޒl��Ԃ��֐�
        float alpha = Mathf.Lerp(originalColor.a, 0, timer / fadeDuration);
        //�F��ς����ɓ����x�����ύX
        AppleTextPrefab.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

        // ���S�ɓ����ɂȂ�����폜
        if (alpha <= 0.01f)
        {
            Destroy(gameObject);
        }
    }
}
