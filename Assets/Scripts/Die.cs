using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public GameObject loosePanel;
    public static Die Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void FinishGame()
    {
        Time.timeScale = 0;
        loosePanel.SetActive(true);
    }
}
