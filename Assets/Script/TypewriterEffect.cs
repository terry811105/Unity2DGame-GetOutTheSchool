using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // 用于显示文字的Text组件
    public float typeSpeed = 0.05f; // 每个字符的显示速度

    private string fullText = ""; // 完整的文字内容
    private string currentText = ""; // 当前显示的文字内容

    void Start()
    {
        Coroutine coroutine = StartCoroutine(ShowText());
    }

    // Update is called once per frame
    void Update()
    {
        
    
    
    }

    public void StartTyping(string text)
    {
        fullText = text;
        currentText = "";
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1);
            dialogueText.text = currentText;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}
