using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMusic : MonoBehaviour
{
    public GameObject SoundManager;
    public string activeScene;
    // Start is called before the first frame update
    void Start()
    {
        activeScene = SceneManager.GetActiveScene().name;

        if (activeScene == "GameOver")
        {
            AudioManager.Instance.PlaySFX("LoseMusic");
        }
        
        if (activeScene == "Credits")
        {
            AudioManager.Instance.PlayMusic("MainMusic");
        }
        if (activeScene == "Menu")
        {
            AudioManager.Instance.PlayMusic("MainMusic");
        }
        if (activeScene == "Cutscene")
        {
            AudioManager.Instance.PlayMusic("MainMusic");
        }
        if (activeScene == "Win")
        {
            AudioManager.Instance.PlaySFX("WinMusic");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
