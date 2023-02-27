using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pointCounter;

    private void Awake()
    {
        //assign with action 
        ActionManager.OnSphereFallForUI += DrawCount;
    }

    //restart current level
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    //update ui text
    public void DrawCount(int points)
    {
        pointCounter.text = $"Points: {points}";
    }

    //reset progress
    public void ResetProgress()
    {
        LevelController.ResetProgress();
        DrawCount(LevelController.points);
    }

    //load main menu scene
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    //load first level scene
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }
    
    //load second level scene
    public void LoadSecondLevel()
    {
        SceneManager.LoadScene(2);
    }
    
    //load third level scene
    public void LoadThirdLevel()
    {
        SceneManager.LoadScene(3);
    }
}
