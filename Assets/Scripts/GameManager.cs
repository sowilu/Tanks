using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public GameObject introCam;
    public Color player1;
    public Color player2;
    
    [HideInInspector]
    public int playerCount = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnPlayerJoined()
    {
        playerCount++;
    }
    
    public void DisableIntroCam()
    {
        introCam.SetActive(false);
    }
}
