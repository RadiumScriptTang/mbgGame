﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
机构：
作者：
最后修改时间：
**/
public class BackButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("StartScene");

    }
}
