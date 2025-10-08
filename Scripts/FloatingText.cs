using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    // 上昇速度
    public float floatSpeed = 1.0f;
    // フェードアウト時間
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
        // 上昇させる
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        // 徐々に透明にする
        timer += Time.deltaTime;
        //Mathf.LerpはoriginalColorから0までtimer / fadeDurationの割合で進む値を返す関数
        float alpha = Mathf.Lerp(originalColor.a, 0, timer / fadeDuration);
        //色を変えずに透明度だけ変更
        AppleTextPrefab.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

        // 完全に透明になったら削除
        if (alpha <= 0.01f)
        {
            Destroy(gameObject);
        }
    }
}
