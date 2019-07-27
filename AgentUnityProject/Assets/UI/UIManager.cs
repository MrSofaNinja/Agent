using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text PlayerNameText;
    [SerializeField] private GameObject Menu;

    private void Awake()
    {
        Assert.IsNotNull(PlayerNameText);
        Assert.IsNotNull(Menu);     
    }

    public void SetPlayerNameText(string Name)
    {
        PlayerNameText.text = name;
    }
    
    public void ToggleMenu()
    {
        Menu.SetActive(!Menu.activeSelf);
    }
}

