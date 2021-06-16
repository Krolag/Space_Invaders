using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health variables")]
    [SerializeField] private int health;
    [SerializeField] private Color lostLifeColor;
    [SerializeField] private GameObject[] lifes;
    private int cHealth;

    private void Start()
    {
        GameEvents.Instance.onPlayerTakingDamage += OnDamageTaken;
        cHealth = health;
    }

    private void OnDamageTaken()
    {
        cHealth--;
        lifes[cHealth].GetComponent<Image>().color = lostLifeColor;

        if (cHealth <= 0)
        {
            GameEvents.Instance.ChangeUI(false);
        }
    }
}
