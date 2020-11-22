using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
机构：
作者：
最后修改时间：
**/
public class GameController : MonoBehaviour
{
    private int shandianbianNumber;

    // Start is called before the first frame update
    public const int WAITING_STATUS = 0;
    public const int START_STATUS = 1;
    public const int MA_MOVE_FORWARD_STATUS = 2;
    public const int MA_ANSWER_STATUS = 3;
    public const int MA_ATTACK_STATUS = 4;
    public const int MA_MOVE_BACK_STATUS = 5;
    public const int AD_MOVE_FORWARD_STATUS = 6;
    public const int AD_ATTACK_STATUS = 7;
    public const int AD_MOVE_BACK_STATUS = 8;

    public GameObject maTeacherGameObject;
    public MaTeacher maTeacherScript;
    public GameObject answerMaskObject;
    public AnswerController answerControllerScript;

    public GameObject gameOverPanel;
    public Text gameOverTip;

    public AudioSource maSpeaker;
    public AudioClip [] beforeAttackClips;
    public AudioClip [] afterAttackedClips;
    public AudioClip fullFiveBianClip; //打满五鞭子;
    public AudioClip attackingClip; // 打闪电鞭跺脚音效
    public AudioClip victoryClip; //胜利音效
    public AudioClip defeatClip; //失败音效

    public int maHp = 3;
    public int stuHp = 15;

    public Text maHpText;
    public Text stuHpText;



    public Student studentScript;

    public bool isAnswering;
    public bool isOver;

    public int status;
    public bool isMaFree;
    void Start()
    {
        ResetGame();

    }

    public void ResetGame()
    {
        status = WAITING_STATUS;
        maTeacherScript = maTeacherGameObject.GetComponent<MaTeacher>();
        isAnswering = false;
        isOver = false;
        answerMaskObject.SetActive(false);
        gameOverPanel.SetActive(false);
    }    

    public void StartBattle()
    {
        status = START_STATUS;
    }

    public void setMaFree()
    {
        isMaFree = true;
    }

    public void StartAnswerQuestion()
    {
        isAnswering = true;
        answerControllerScript.StartAnswer();
        answerMaskObject.SetActive(true);
    }

    public void OnAnswered(int correctNumber)
    {
        if(status != MA_ANSWER_STATUS || !maTeacherScript.isFree())
        {
            return;
        }
        isAnswering = false;
        status = MA_ATTACK_STATUS;
        answerMaskObject.SetActive(false);
        shandianbianNumber = correctNumber;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        //先判输赢
        if(maHp <= 0 && !isOver)
        {
            maSpeaker.clip = defeatClip;
            maSpeaker.Play(0);

            status = WAITING_STATUS;
            maTeacherScript.Lose();
            gameOverPanel.SetActive(true);
            gameOverTip.text = "你给传统功夫抹黑了！";
            isOver = true;
        }
        if (stuHp <= 0 && !isOver)
        {
            maSpeaker.clip = victoryClip;
            maSpeaker.Play(0);

            status = WAITING_STATUS;
            gameOverPanel.SetActive(true);
            studentScript.Lose();
            gameOverTip.text = "你弘扬了传统功夫！";
            isOver = true;
        }

        if (!maTeacherScript.isFree() || !studentScript.IsFree() || isAnswering)
        {
            return;
        }
        switch (status)
        {
            case WAITING_STATUS:
                return;
            case START_STATUS:
                status = MA_MOVE_FORWARD_STATUS;
                break;
            case MA_MOVE_FORWARD_STATUS:
                status = MA_ANSWER_STATUS;
                maTeacherScript.MoveToCenter();
                break;
            case MA_ANSWER_STATUS:
                maSpeaker.clip = beforeAttackClips[Random.Range(0, beforeAttackClips.Length)];
                maSpeaker.Play(0);
                StartAnswerQuestion();                
                break;
            case MA_ATTACK_STATUS:
                //跺脚音效
                maSpeaker.clip = attackingClip;
                maSpeaker.Play(0);
                maTeacherScript.PlayShandianbian(shandianbianNumber);
                status = MA_MOVE_BACK_STATUS;
                break;
            case MA_MOVE_BACK_STATUS:
                maSpeaker.clip = null;
                maSpeaker.Play(0);
                maTeacherScript.MoveBack();
                status = AD_MOVE_FORWARD_STATUS;
                stuHp -= shandianbianNumber;
                stuHpText.text = "Hp：" + stuHp;
                break;
            case AD_MOVE_FORWARD_STATUS:
                if(shandianbianNumber >= 5)
                {
                    maSpeaker.clip = fullFiveBianClip;
                    maSpeaker.Play(0);
                }
                else
                {
                    maSpeaker.clip = null;
                    maSpeaker.Play(0);
                }
                studentScript.MoveToCenter();
                status = AD_ATTACK_STATUS;
                break;
            case AD_ATTACK_STATUS:
                studentScript.Attack();
                //选择一个被攻击语音
                maSpeaker.clip = afterAttackedClips[Random.Range(0, afterAttackedClips.Length)];
                maSpeaker.Play(0);
                status = AD_MOVE_BACK_STATUS;
                break;
            case AD_MOVE_BACK_STATUS:
                maHp--;
                maHpText.text = "Hp：" + maHp;
                studentScript.MoveBack();
                status = MA_MOVE_FORWARD_STATUS;
                break;

        }
    }
}
