using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static string sceneName;

    public static int currentScore;
    public static int highScore;

    public static int currentLevel;
    public static int unlockedLevel;
    
    public static void CompleteLevel()
    {
        print("Completed level " + currentLevel);
        if (currentLevel < 1)
        {
            currentLevel += 1;
            //SceneManager.LoadScene(sceneName);
            Application.LoadLevel(currentLevel);
        }
        else
        {
            print("You win!");
        }
    }
}
