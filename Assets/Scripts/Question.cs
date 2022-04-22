using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    public bool isAnswerTrue;
    private GameObject manager;
    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isAnswerTrue)
                return;
            else
                manager.GetComponent<Die>().FinishGame();
        }
    }
}
