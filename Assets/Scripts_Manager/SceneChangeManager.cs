using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    //シーンの名前
    [SerializeField] private string titleSceneName;
    [SerializeField] private string stageSceneName;

    public void OnClickGoStage()
    {
        SceneManager.LoadScene(stageSceneName);
    }

    public void OnClickGoTitle()
    {
        SceneManager.LoadScene(titleSceneName);
    }

}
