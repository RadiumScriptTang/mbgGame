using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
机构：
作者：
最后修改时间：
**/
public class Shan : MonoBehaviour
{
    public float SHAN_CD;
    public float currentCd;
    // Start is called before the first frame update
    void Start()
    {
        SHAN_CD = 3;
        currentCd = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentCd > 0)
        {
            currentCd -= 0.02f;
            if(currentCd <= 0)
            {
                currentCd = 0;
                this.GetComponent<Renderer>().material.color = Color.white;
            }        

        }
    }

    public bool TriggerShan()
    {
        if(currentCd > 0)
        {
            return false;
        }
        this.GetComponent<Renderer>().material.color = Color.gray;
        currentCd = SHAN_CD;
        return true;
    }
}
