using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    [SerializeField] float transitionTime = 1;
    void OnEnable()
    {
        DialogController.OnSceneChanged += LoadNextScene;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    bool checkScene(String scene)
    {
        int buildIndex = SceneUtility.GetBuildIndexByScenePath(scene);
        if (buildIndex == -1) return false;
        return true;
    }
    void LoadNextScene(String scene)
    {
        if (!checkScene(scene))
        {
            Debug.Log($"Scene {scene} is not found!");
            return;
        }
        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(String scene)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(scene);

    }

}
