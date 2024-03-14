using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private int totalItems = 5;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject deathScreen;

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PlayerDied()
    {
        FreezeTime();
        deathScreen.SetActive(true);
        PlayerInputManager.FreeAndShowCursor();
    }

    public static void ReloadLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public static void FreezeTime()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public static void ResumeTime()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
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