using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    public static int points;
    private int _sphereCount = 8;

    private void Awake()
    {
        //load point from PlayerPrefs
        points = LoadProgress();
        
        //draw it on UI
        _uiManager.DrawCount(points);
        
        //assign action
        ActionManager.OnSphereFall += FallSphere;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_sphereCount == 0 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            SaveProgress(points);
            _uiManager.RestartLevel();
        }
    }

    private static void SaveProgress(int points)
    {
        PlayerPrefs.SetInt("Points", points);
    }

    private static int LoadProgress()
    {
        return PlayerPrefs.GetInt("Points", 0);
    }

    public static void ResetProgress()
    {
        points = 0;
        PlayerPrefs.SetInt("Points", 0);
    }

    private void FallSphere()
    {
        SaveProgress(points);
        _sphereCount--;
    }
}
