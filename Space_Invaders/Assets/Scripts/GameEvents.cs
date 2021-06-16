using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    private void Awake()
    {
        /* Check if instance already exist */
        if (Instance == null) // if not, set this one as instance
            Instance = this;
        else if (Instance != null) // if yes, destroy unused object
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public event Action onPlayerTakingDamage;
    public void PlayerTakeDamage()
    {
        if (onPlayerTakingDamage != null)
        {
            onPlayerTakingDamage();
        }
    }

    public event Action<bool> onUIChange;
    public void ChangeUI(bool _doPlayerWon)
    {
        if (onUIChange != null)
        {
            onUIChange(_doPlayerWon);
        }
    }

    public event Action onEnemyTakingDamage;
    public void Killed()
    {
        if (onEnemyTakingDamage != null)
        {
            onEnemyTakingDamage();
        }
    }
}
