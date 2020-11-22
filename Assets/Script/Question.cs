using UnityEngine;

/**
机构：
作者：
最后修改时间：
**/
public class Question
{
    public string questionText;
    public string qAnswer;
    public string wAnswer;
    public string eAnswer;
    public string rAnswer;
    public KeyCode correctAnswer;
    public Question(string t, string q, string w, string e, string r, char c)
    {
        this.questionText = t;
        this.qAnswer = q;
        this.wAnswer = w;
        this.eAnswer = e;
        this.rAnswer = r;
        c = char.ToLower(c);

        switch (c)
        {
            case 'q':
                correctAnswer = KeyCode.Q;
                break;
            case 'w':
                correctAnswer = KeyCode.W;
                break;
            case 'e':
                correctAnswer = KeyCode.E;
                break;
            case 'r':
                correctAnswer = KeyCode.R;
                break;
        }
    }
}
