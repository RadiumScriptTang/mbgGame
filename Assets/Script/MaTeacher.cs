using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaTeacher : MonoBehaviour
{
    public float moveSpeed;
    public GameObject shanObject;
    public SpriteRenderer thisSpriteRender;
    public Sprite maTeacherBaoquan;
    public Sprite maTeacherLose;
    public Sprite [] maTeacherShandianbian;
    public int shandianbianIndex;
    public int status;
    public float originX;

    public const int STANDING_STATUS = 1;
    public const int SHANDIANBIAN_STATUS = 2;
    public const int MOVING_TO_CENTER = 3;
    public const int MOVING_BACK = 4;
    public const int LOSE_STATUS = 5;

    private int shandianbianToPlayNumber;

    public void Lose()
    {
        status = LOSE_STATUS;
        thisSpriteRender.sprite = maTeacherLose;
        this.transform.position = new Vector3(-4.37f, -3.28f, 0);
    }
    private void Awake()
    {
    }
    // Start is called before the first frame update
    private void Start()
    {
        thisSpriteRender = this.GetComponent<SpriteRenderer>();
        shandianbianIndex = 0;
        status = STANDING_STATUS;
        moveSpeed = 0.05f;
        originX = this.transform.position.x;
    }

    public bool PlayShandianbian(int shandianbianNumber)
    {
        shandianbianToPlayNumber = shandianbianNumber * 30;
        if(this.status == SHANDIANBIAN_STATUS)
        {
            return false;
        }
        this.status = SHANDIANBIAN_STATUS;
        return true;
    }
    public bool MoveToCenter()
    {
        if (this.status == MOVING_TO_CENTER)
        {
            return false;
        }
        this.status = MOVING_TO_CENTER;
        return true;
    }

    public bool MoveBack()
    {
        if (this.status == MOVING_BACK)
        {
            return false;
        }
        this.status = MOVING_BACK;
        return true;
    }

    public bool isFree()
    {
        return status == STANDING_STATUS;
    }

    // Update is called once per frame
    // 时间固定，不受渲染影响 
    private void FixedUpdate()
    {
        switch (status)
        {
            case STANDING_STATUS:
                thisSpriteRender.sprite = maTeacherBaoquan;
                break;
            case SHANDIANBIAN_STATUS:
                shandianbianIndex++;
                thisSpriteRender.sprite = maTeacherShandianbian[shandianbianIndex / 10 % maTeacherShandianbian.Length];
                if(shandianbianIndex > shandianbianToPlayNumber)
                {
                    shandianbianIndex = 0;
                    status = STANDING_STATUS;
                    thisSpriteRender.sprite = maTeacherBaoquan;
                }
                break;
            case MOVING_TO_CENTER:
                if(this.transform.position.x >= 0)
                {
                    status = STANDING_STATUS;
                    break;
                }
                this.transform.Translate(Vector3.right * moveSpeed);
                break;
            case MOVING_BACK:
                if(this.transform.position.x <= originX)
                {
                    status = STANDING_STATUS;
                    break;
                }
                this.transform.Translate(Vector3.left * moveSpeed);
                break;
            case LOSE_STATUS:
                thisSpriteRender.sprite = maTeacherLose;
                break;
        }

    }
}
