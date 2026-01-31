using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    
    [Header("Scene Settings")]
    [SerializeField] private string gameSceneName = "GameScene"; // Change to your game scene name
    
    void Start()
    {
        // Add listeners to buttons
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }
        
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }
    
    void StartGame()
    {
        // Load the game scene
        SceneManager.LoadScene(gameSceneName);
    }
    
    void QuitGame()
    {
        // Quit the application
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
        
        Debug.Log("Game Quit!");
    }
    
    void OnDestroy()
    {
        // Clean up listeners
        if (startButton != null)
        {
            startButton.onClick.RemoveListener(StartGame);
        }
        
        if (quitButton != null)
        {
            quitButton.onClick.RemoveListener(QuitGame);
        }
    }
}