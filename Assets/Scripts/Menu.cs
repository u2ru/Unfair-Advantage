using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour
{

    [SerializeField]
    private GameObject menu1;

    [SerializeField]
    private GameObject menu2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Levels()
    {
        menu1.SetActive(false);
        menu2.SetActive(true);
    }

    public void BackToMainMenu()
    {
        menu2.SetActive(false);
        menu1.SetActive(true);
    }

    public void Exit()
    {
    #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
