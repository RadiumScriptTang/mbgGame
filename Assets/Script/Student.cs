using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
机构：
作者：
最后修改时间：
**/
public class Student : MonoBehaviour
{
    public Sprite studentStanding;
    public Sprite studentAttacking;
    public Sprite studentLose;

    public GameObject thisObject;
    public SpriteRenderer thisSpriteRender;
    public float originX;
    public int attackTimer;
    public int status;
    public float moveSpeed;

    public const int STANDING_STATUS = 1;
    public const int ATTACKING_STATUS = 2;
    public const int MOVING_TO_CENTER = 3;
    public const int MOVING_BACK = 4;
    public const int LOSE_STATUS = 5;


    public void Lose()
    {
        status = LOSE_STATUS;
        thisSpriteRender.sprite = studentLose;
        this.transform.position = new Vector3(4.07f, -3.69f, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        originX = thisObject.transform.position.x;
        thisSpriteRender = thisObject.GetComponent<SpriteRenderer>();
        status = STANDING_STATUS;
        attackTimer = 0;
        moveSpeed = 0.05f;
    }

    public bool IsFree()
    {
        return status == STANDING_STATUS;
    }

    public void MoveToCenter()
    {
        status = MOVING_TO_CENTER;
    }

    public void MoveBack()
    {
        status = MOVING_BACK;
    }

    public void Attack()
    {
        status = ATTACKING_STATUS;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        switch (status)
        {
            case STANDING_STATUS:
                thisSpriteRender.sprite = studentStanding;
                break;
            case ATTACKING_STATUS:
                thisSpriteRender.sprite = studentAttacking;
                if(attackTimer > 50)
                {
                    attackTimer = 0;
                    status = STANDING_STATUS;
                    thisSpriteRender.sprite = studentStanding;
                }
                else
                {
                    attackTimer++;
                }
                break;
            case MOVING_TO_CENTER:
                if (this.transform.position.x <= 0)
                {
                    status = STANDING_STATUS;
                    break;
                }
                this.transform.Translate(Vector3.left * moveSpeed);
                break;
            case MOVING_BACK:
                if (this.transform.position.x >= originX)
                {
                    status = STANDING_STATUS;
                    break;
                }
                this.transform.Translate(Vector3.right * moveSpeed);
                break;
            case LOSE_STATUS:
                thisSpriteRender.sprite = studentLose;
                break;

        }
    }
}
