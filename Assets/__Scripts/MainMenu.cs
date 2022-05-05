using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using Vibrationtype = Thalmic.Myo.VibrationType;

public class MainMenu : MonoBehaviour
{
    public GameObject myo = null;
    private Pose lastPose = Pose.Unknown;

    private void Update()
    {
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

        if (thalmicMyo.pose != lastPose)
        {
            // wave out to start game
            if (thalmicMyo.pose == Pose.WaveOut)
            {
                PlayGame();
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

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Application!");
        Application.Quit();
    }

   void ExtendUnlock (ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard)
        {
            myo.Unlock(UnlockType.Timed);
        }

        myo.NotifyUserAction();
    }

}
