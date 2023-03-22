using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource audioTheme;
    [SerializeField] private AudioSource audioBite;
    public static Audio instance;
    private void Awake()
    {
        instance= this;
    }
    public void Theme()
    {
        audioTheme.Play();
    }
    public void Bite()
    {
        audioBite.Play();
    }
    public void Mute()
    {
        audioTheme.mute = true;
    }
    public void DontMute()
    {
        audioTheme.mute = false;
    }
}
