using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour
{
    int levelToLoad = 1;
    int levelsUnlocked = 2;
    public static MainMenuController instance = null;
    [SerializeField] LevelChanger levelChanger;
    [SerializeField] Button prevLevelButton;
    [SerializeField] Button nextLevelButton;
    [SerializeField] Text levelLabelText;
    // Use this for initialization

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        Assert.IsNotNull(levelChanger);
        Assert.IsNotNull(prevLevelButton);
        Assert.IsNotNull(nextLevelButton);
        Assert.IsNotNull(levelLabelText);
        //in the future, read from file what scenes have been unlocked!!!
        levelsUnlocked = SceneManager.sceneCountInBuildSettings - 1;
        Debug.Log("levelsUnlocked " + levelsUnlocked);
        if (levelsUnlocked > 1)
        {
            nextLevelButton.interactable = true;
        }
    }

    public void OnPrevLevel()
    {
        Debug.Log("OnPrevLevel");
        levelToLoad--;
        updateLevelButtons();
        updateLevelLabel();
    }

    public void OnNextLevel()
    {
        Debug.Log("OnNextLevel");
        levelToLoad++;
        updateLevelButtons();
        updateLevelLabel();
    }
    public void SetLevelToLoad(int arg)
    {
        levelToLoad = arg;
    }

    public void OnPlay()
    {
        levelChanger.FadoutToLevel(levelToLoad);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    void updateLevelButtons()
    {
        if (levelToLoad == 1)
        {
            prevLevelButton.interactable = false;
            Debug.Log("prevLevelButton.enabled = false;");
        }
        else
        {
            prevLevelButton.interactable = true;
            Debug.Log("prevLevelButton.enabled = true;");
        }
        if (levelToLoad < levelsUnlocked)
        {
            nextLevelButton.interactable = true;
            Debug.Log("nextLevelButton.enabled = true;");
        }
        else
        {
            nextLevelButton.interactable = false;
            Debug.Log("nextLevelButton.enabled = true;");
        }
    }

    void updateLevelLabel() {
        levelLabelText.text = "Level " + levelToLoad;
    }
}
