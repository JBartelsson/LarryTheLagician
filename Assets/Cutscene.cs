using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public List<Image> images;
    public List<AudioSource> audioSources;
    int index = 0;
    float fadeDuration = .6f;

    private void Start()
    {
        foreach (var item in images)
        {
            item.DOFade(0f, 0f);
        }
        images[0].DOFade(1f, fadeDuration);
        audioSources[0].Play();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            index++;
            if (index == images.Count)
            {
                SceneManager.LoadScene("GameScene");
                return;
            } 
            images[index].DOFade(1f, fadeDuration);
            if (index < audioSources.Count)
            {
                audioSources.ForEach((x) => x.Stop());
                audioSources[index].Play();
            }
            
        }

    }
}
