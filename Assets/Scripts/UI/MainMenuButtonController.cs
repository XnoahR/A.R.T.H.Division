using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class MainMenuButtonController : MonoBehaviour
{
    public Button[] buttonList;
    [SerializeField] Sprite activeSprite;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite clickedSprite;
    private int currentButtonIndex;

    void Start()
    {
        UpdateButtonVisual();
    }

    void UpdateButtonVisual()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            Image img = buttonList[i].image;
            img.sprite = (i == currentButtonIndex) ? activeSprite : normalSprite;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentButtonIndex = currentButtonIndex == 0 ? buttonList.Length - 1 : currentButtonIndex - 1;
            UpdateButtonVisual();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentButtonIndex = currentButtonIndex == buttonList.Length - 1 ? 0 : currentButtonIndex + 1;
            UpdateButtonVisual();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            
            buttonList[currentButtonIndex].onClick.Invoke();
        }
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void SettingsButton()
    {
        //
    }

    public void CreditButton()
    {
        // Text Credit
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
