using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    public static int level = 0;
    public static Menu instance;
    [SerializeField] private RectTransform panel;
    private void Awake()
    {
        panel.gameObject.SetActive(false);   
        instance = this;
    }
    
    // Start is called before the first frame update
    public void StartGame()
    {
        panel.gameObject.SetActive(true);
    }
    public void Easy()
    {
        level = 1;
        SceneManager.LoadScene(1);
    }
    public void Normal()
    {
        level = 2;
        SceneManager.LoadScene(1);
    }
    public void Hard()
    {
        level = 3;
        SceneManager.LoadScene(1);
    }

}
