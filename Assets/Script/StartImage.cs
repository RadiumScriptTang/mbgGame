using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
机构：
作者：
最后修改时间：
**/
public class StartImage : MonoBehaviour
{
    public int stableTime = 50 * 1;
    public GameObject thisObject;
    public GameController gameController;
   
    // Update is called once per frame
    void Update()
    {
        if(stableTime <= 0)
        {
            thisObject.transform.Translate(Vector3.right * 0.1f);
        }
        else
        {
            stableTime--;
        }
        if(thisObject.transform.position.x > 20)
        {
            GameObject.Destroy(this);
            gameController.StartBattle();
        }
    }
}
