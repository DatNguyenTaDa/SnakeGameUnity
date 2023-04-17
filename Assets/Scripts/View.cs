using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    public static int soundTheme = 1;
    public static int soundBite = 1;
    [SerializeField] private RectTransform panel;
    [SerializeField] private Button onTheme;
    [SerializeField] private Button offTheme;
    [SerializeField] private Button onBite;
    [SerializeField] private Button offBite;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Update()
    {
        if(soundTheme == 1)
        {
            onTheme.gameObject.SetActive(true);
            offTheme.gameObject.SetActive(false);
        }
        else if(soundTheme == 0)
        {
            onTheme.gameObject.SetActive(false);
            offTheme.gameObject.SetActive(true);
        }
        if(soundBite == 1)
        {
            onBite.gameObject.SetActive(true);
            offBite.gameObject.SetActive(false);
        } else if(soundBite == 0)
        {
            onBite.gameObject.SetActive(false);
            offBite.gameObject.SetActive(true);
        }
    }
    public void OnTheme()
    {
        soundTheme = 0;
    }
    public void OffTheme()
    {
        soundTheme = 1;
    }
    public void OnBite()
    {
        soundBite= 0;
    }
    public void OffBite()
    {
        soundBite= 1;
    }
    public void Pause()
    {
        panel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void Remuse()
    {
        panel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }
}
