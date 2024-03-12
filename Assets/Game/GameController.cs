using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private int totalItems = 5;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject pauseScreen;

    public static void FreezeTime()
    {
        Time.timeScale = 0;
    }

    public static void ResumeTime()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        FreezeTime();
        pauseScreen.SetActive(true);
        PlayerInputManager.FreeAndShowCursor();
    }

    public void ResumeGame()
    {
        ResumeTime();
        pauseScreen.SetActive(false);
        PlayerInputManager.LockAndHideCursor();
    }

    private void ItemCollected()
    {
        if (playerInventory.ItemCount < totalItems) return;
        
        FreezeTime();
        winScreen.gameObject.SetActive(true);
        PlayerInputManager.FreeAndShowCursor();
    }

    private void Awake()
    {
        playerInventory.ItemAdded?.AddListener(ItemCollected);
    }

    private void Start()
    {
        ResumeGame();
    }
}
