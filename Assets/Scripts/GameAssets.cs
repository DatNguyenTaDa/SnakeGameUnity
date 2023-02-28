using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets i;

    private void Awake()
    {
        i = this;
    }
    /// <summary>
    /// set hinh dang dau, than va do an
    /// </summary>
    public Sprite snakeHeadSprite;
    public Sprite snakeBodySprite;
    public Sprite foodSprite;
}
