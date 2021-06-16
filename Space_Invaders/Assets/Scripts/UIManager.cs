using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject won;
    [SerializeField] private GameObject lost;

    public void LoadLevel()
    {
        if (won != null || lost != null)
        {
            won.SetActive(false);
            lost.SetActive(false);
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("(01) level", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("(00) menu", LoadSceneMode.Single);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "(01) level")
            GameEvents.Instance.onUIChange += ActivateUI;
    }

    private void ActivateUI(bool _doPlayerWon)
    {
        Time.timeScale = 0;
        if (_doPlayerWon)
            won.SetActive(true);
        else
            lost.SetActive(true);
    }
}
