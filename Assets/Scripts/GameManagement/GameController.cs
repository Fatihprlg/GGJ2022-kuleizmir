using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Key[] keys;
    public GameObject[] doors;
    Animator positiveDoorAnimator;
    Animator negativeDoorAnimator;

    public static bool negativeIsFinished = false;
    public static bool positiveIsFinished = false;


    private void Awake()
    {
        positiveDoorAnimator = doors[1].GetComponent<Animator>();
        negativeDoorAnimator = doors[0].GetComponent<Animator>();

    }

    private void Update()
    {
        if (keys[1].plugged && keys[0].plugged)
        {
            positiveDoorAnimator.SetBool("OpenDoor", true);
            negativeDoorAnimator.SetBool("OpenDoor", true);
        }
        if (negativeIsFinished && positiveIsFinished) Finish();

    }


    public void Finish()
    {
        negativeDoorAnimator.SetBool("CloseDoor", true);
        positiveDoorAnimator.SetBool("CloseDoor", true);
        
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
