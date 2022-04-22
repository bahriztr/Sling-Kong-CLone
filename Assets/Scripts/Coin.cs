using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private DragAndShoot player;
    private UIManager _uıManager;
    private void Start()
    {
        _uıManager = UIManager.Instance;
        player = DragAndShoot.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        player.OnTakeCoin();
        _uıManager.UpdateCoin(player.coin);
        gameObject.SetActive(false);
    }
}
