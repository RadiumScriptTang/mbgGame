using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
机构：
作者：
最后修改时间：
**/
public class QuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
