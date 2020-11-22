using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
机构：
作者：
最后修改时间：
**/
public class AnswerButton : MonoBehaviour
{
    public KeyCode thisKeyCode;
    public AnswerController answerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AnswerQuestionByClick()
    {
        answerController.AnswerQuestionByClick(thisKeyCode);
    }
}
