using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
机构：
作者：
最后修改时间：
**/
public class AnswerController : MonoBehaviour
{
    private static string[][] questionSet = new string[][]
    {
        new string[] {"袭击马老师的年轻人不讲什么","道德","师德","武德","美德","e"},
        new string[] {"马老师认为中国传统武术强于什么","MMA","NBA","NASA","MBA","q"},
        new string[] {"马老师的闪电鞭是几维的","1维","2维","3维","4维","e" },
        new string[] {"马老师挨打的时候多大了","59岁","69岁","79岁","89岁","w" },
        new string[] {"年轻人靠什么打到了马老师","左正蹬","右鞭腿","左刺拳","骗、偷袭","r" },
        new string[] {"按照传统功法的什么原则年轻人已经输了","力从地起","点到为止","以和为贵","不讲武德","w" },
        new string[] {"几百斤的英国大力士握不懂马老师的手指","一百多斤","两百多斤","二百多斤","三百多斤","e" },
        new string[] {"马老师认为要代表传统功夫必须学会什么","闪电鞭","金刚大捣锤","马甲刀法","接化发","r" },
        new string[] {"马老师是什么的掌门人","浑元形意太极门","混元行意太极门","浑元型意太极门","混元形意太极门","q" },
        new string[] {"年轻人突然袭击来打马老师的什么部位","胸","鞭","腹","脸","r" },
    };

    public AudioClip[] audioClips;
    public Button qButton;
    public Button wButton;
    public Button eButton;
    public Button rButton;
    public Text questionText;
    public GameController gameControllerScript;
    public AudioSource audioSource;

    private KeyCode nextCorrectAnswerKeyCode;
    private int questionIndex;
    private int audioIndex;
    private bool isAnswering;

    private int showAnswerTimer;
    private int answerTimer;
    private bool hasAnswered;
    private int correctNumber;
    private int questionNumber;
    private static int maxAnswerTime = 50 * 10;
    private static int maxShowAnswerTime = 50 * 2;
    // Start is called before the first frame update
    void Start()
    {
        showAnswerTimer = maxShowAnswerTime;
        answerTimer = maxAnswerTime;
        questionIndex = Random.Range(0, questionSet.Length);
    }

    public void AnswerQuestionByClick(KeyCode keyCode)
    {
        //Debug.Log("><");
        //if (hasAnswered)
        //{
        //    return;
        //}
        //hasAnswered = true;
        //answerResult = keyCode == nextCorrectAnswerKeyCode;
        //correctNumber += answerResult ? 1: 0;
        //playAudio();
        //switch (nextCorrectAnswerKeyCode)
        //{
        //    case KeyCode.E:
        //        eButton.GetComponentInChildren<Text>().color = Color.green;
        //        break;
        //}
    }

    public void playAudio()
    {
        audioSource.loop = false;
        audioSource.clip = this.audioClips[audioIndex];
        if(!audioSource.isPlaying)
            audioSource.Play(0);
    }

    public void StartAnswer()
    {
        questionNumber = 4;
        correctNumber = 0;
        isAnswering = true;
        questionIndex = Random.Range(0, questionSet.Length);
        RenderQuestion();
    }

    public bool RenderQuestion()
    {

        audioIndex = questionIndex;
        string[] questionArray = questionSet[questionIndex];
        Question q = new Question(questionArray[0], questionArray[1], questionArray[2], questionArray[3], questionArray[4], questionArray[5][0]);
        showAnswerTimer = maxShowAnswerTime;
        answerTimer = maxAnswerTime;
        hasAnswered = false;
        qButton.onClick.Invoke();
        nextCorrectAnswerKeyCode = q.correctAnswer;
        qButton.GetComponentInChildren<Text>().color = Color.black;
        wButton.GetComponentInChildren<Text>().color = Color.black;
        eButton.GetComponentInChildren<Text>().color = Color.black;
        rButton.GetComponentInChildren<Text>().color = Color.black;
        qButton.GetComponentInChildren<Text>().text = "Q:" + q.qAnswer;
        eButton.GetComponentInChildren<Text>().text = "E:" + q.eAnswer;
        rButton.GetComponentInChildren<Text>().text = "R:" + q.rAnswer;
        wButton.GetComponentInChildren<Text>().text = "W:" + q.wAnswer;
        questionText.text = q.questionText;
        questionIndex++;
        questionIndex %= questionSet.Length;
        return true;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAnswering)
        {
            return;
        }
        if (!hasAnswered)
        {
            if(answerTimer <= 0)
            {
                hasAnswered = true;
                answerTimer = maxAnswerTime;
            } 
            else
            {
                answerTimer--;
            }

        }
        else
        {

            if (showAnswerTimer <= 0)
            {
                showAnswerTimer = maxShowAnswerTime;
                if(questionNumber > 0)
                {
                    questionNumber--;
                    RenderQuestion();
                }
                else
                {
                    gameControllerScript.OnAnswered(correctNumber);
                    isAnswering = false;
                }
            }
            else
            {
                showAnswerTimer--;
            }

        }
        if (Input.anyKeyDown)
        {
            if(!(Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R)))
            {
                return;
            }

            if (Input.GetKeyDown(nextCorrectAnswerKeyCode))
            {
                correctNumber++;

            }
            else
            {

            }

            hasAnswered = true;
            playAudio();
            switch (nextCorrectAnswerKeyCode)
            {
                case KeyCode.E:
                    eButton.GetComponentInChildren<Text>().color = Color.green;
                    break;                
                case KeyCode.Q:
                    qButton.GetComponentInChildren<Text>().color = Color.green;
                    break;
                case KeyCode.W:
                    wButton.GetComponentInChildren<Text>().color = Color.green;
                    break;        
                case KeyCode.R:
                    rButton.GetComponentInChildren<Text>().color = Color.green;
                    break;

            }

        }

    }
}
