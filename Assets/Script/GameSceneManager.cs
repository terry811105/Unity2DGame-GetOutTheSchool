using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadMiniGame(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public static void LoadKeyMiniGame()
    {
        SceneManager.LoadSceneAsync("KeyGameScene", LoadSceneMode.Additive);
    }

    public static void LoadPuzzleMiniGame()
    {
        SceneManager.LoadSceneAsync("PuzzleScene");
    }

    public static void ReturnToMainScene()
    {
        SceneManager.UnloadSceneAsync("KeyGameScene");
    }
}
