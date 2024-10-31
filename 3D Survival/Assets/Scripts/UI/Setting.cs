using UnityEngine;

public class Setting : MonoBehaviour
{
    private PlayerController playerController;
    public GameObject settingWindow;
    private void Start()
    {
        playerController = CharacterManager.Instance.player.controller;
        playerController.setting += Toggle;
        settingWindow.SetActive(false);
    }

    public void Toggle()
    {
        if (IsOpen())
        {
            settingWindow.SetActive(false);
        }
        else
        {
            settingWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return settingWindow.activeInHierarchy;
    }
}