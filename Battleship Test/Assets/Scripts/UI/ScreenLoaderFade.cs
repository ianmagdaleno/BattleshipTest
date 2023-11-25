using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenLoaderFade : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTimer = 1.3f;

    public void TransitionNextScreen(string sceneName)
    {
        StartCoroutine(Fade(sceneName));
    }

    IEnumerator Fade(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTimer);

        SceneManager.LoadScene(sceneName);
    }
}
