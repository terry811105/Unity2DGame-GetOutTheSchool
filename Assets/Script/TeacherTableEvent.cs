using TMPro;
using UnityEngine;

public class TeacherTableEvent : MonoBehaviour
{
    public GameObject promptUI; 
    public GameObject dialogUI; // 對話框UI物件
    public TextMeshProUGUI textUI;
    public TextAsset txtFiled;
    
    bool isPlayerInRange = false;
    bool isDialogShown = false;

    int displayIndex = 0;
    bool isTextFinish = false;

    string[] lineData;
    
    private TypewriterEffect typewriterEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
