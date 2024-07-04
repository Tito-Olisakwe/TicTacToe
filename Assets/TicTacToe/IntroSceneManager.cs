using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    public void StartGameTwoPlayers()
    {
        PlayerPrefs.SetString("GameMode", "TwoPlayers");
        SceneManager.LoadScene("TICTACTOE");
    }

    public void StartGameAIAsX()
    {
        PlayerPrefs.SetString("GameMode", "AIAsX");
        SceneManager.LoadScene("TICTACTOE");
    }

    public void StartGameAIAsO()
    {
        PlayerPrefs.SetString("GameMode", "AIAsO");
        SceneManager.LoadScene("TICTACTOE");
    }
}
