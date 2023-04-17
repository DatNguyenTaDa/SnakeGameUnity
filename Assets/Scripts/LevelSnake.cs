using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSnake : MonoBehaviour
{
    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;
    private int width;
    private int height;
    private Snake snake;
    /// <summary>
    /// set pham vi ma food co the spawn
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public LevelSnake(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void Setup(Snake snake)
    {
        this.snake = snake;

        SpawnFood();
    }

    /// <summary>
    /// spawn ra thuc an
    /// </summary>
    private void SpawnFood()
    {
        do
        {
            foodGridPosition = new Vector2Int(Random.Range(-width, width), Random.Range(-height, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);

        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    }
    /// <summary>
    /// kiem tra food da an hay chua
    /// </summary>
    /// <param name="snakeGridPosition"></param>
    /// <returns></returns>
    public bool TrySnakeEatFood(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == foodGridPosition)
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
            MainGame.AddScore();
            return true;
        }
        else
        {
            return false;
        }
    }
    public Vector2Int ValidSnakePosition(Vector2Int snakePosition)
    {
        if (snakePosition.x < -width)
        {
            snakePosition.x = width;
        }
        if(snakePosition.x > width)
        {
            snakePosition.x = -width;
        }
        if(snakePosition.y < -height)
        {
            snakePosition.y = height;
        }
        if(snakePosition.y > height)
        {
            snakePosition.y = -height;
        }
        return snakePosition;
    }

}
