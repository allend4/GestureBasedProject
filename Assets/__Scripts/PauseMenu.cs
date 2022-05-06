using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using Vibrationtype = Thalmic.Myo.VibrationType;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject pauseGestureUI;

    public GameObject myo = null;
    private Pose lastPose = Pose.Unknown;

    void Update()
    {
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

        if (Input.GetKeyDown(KeyCode.Escape) || thalmicMyo.pose == Pose.FingersSpread)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (thalmicMyo.pose != lastPose)
        {
            // wave out to resume game
            if (thalmicMyo.pose == Pose.WaveOut)
            {
                Resume();
                Debug.Log("Wave Out");
            }
            // wave in to quit game
            else if (thalmicMyo.pose == Pose.WaveIn)
            {
                QuitGame();
                Debug.Log("Wave In");
            }
        }
    }

    // Game resumes with timescale 1
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseGestureUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // Game stops with time scale 0
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseGestureUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // Returns to Main menu + reverts game back to normal speed (unpaused)
    public void LoadMenu()
    {
        Debug.Log("Loading Menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

}
