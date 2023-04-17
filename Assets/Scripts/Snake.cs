using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    private enum Direction
    {
        Left,
        Right,
        Up,
        Down,
        Zero
    }
    private enum IsAlive
    {
        Alive,
        Dead
    }

    private IsAlive isAlive;
    private Direction snakeMoveDirection;
    private Vector2Int snakePosition;
    private float snakeMoveTimer;
    private float snakeMoveTimerMax;
    private LevelSnake levelSnake;
    private int snakeBodySize;
    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;
    private float speed;
    public void Setup(LevelSnake levelGrid)
    {
        this.levelSnake = levelGrid;
    }

    private void Awake()
    {

        if (Menu.level == 1)
        {
            speed = 1f;
        }
        else if (Menu.level == 2)
        {
            speed = 1.5f;
        }
        else if (Menu.level == 3)
        {
            speed = 2f;
        }
        else speed = 1f;
        snakePosition = new Vector2Int(0, 0);
        snakeMoveTimerMax = .2f * (1 / speed);
        snakeMoveTimer = snakeMoveTimerMax;
        snakeMoveDirection = Direction.Zero;

        snakeMovePositionList = new List<SnakeMovePosition>();
        snakeBodySize = 0;

        snakeBodyPartList = new List<SnakeBodyPart>();
    }
    private void Start()
    {
        isAlive = IsAlive.Alive;

    }

    private void Update()
    {
        if (isAlive == IsAlive.Alive)
        {
            HandleInput();
            HandleGridMovement();
        }
        else {
            SceneManager.LoadScene(2);
            return;
        };
        if(snakeBodySize%10 == 0)
        {
            speed++;
        }
    }
    /// <summary>
    /// nhan lenh tu ban phim
    /// </summary>
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (snakeMoveDirection != Direction.Down)
            {
                snakeMoveDirection = Direction.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (snakeMoveDirection != Direction.Up)
            {
                snakeMoveDirection = Direction.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (snakeMoveDirection != Direction.Right)
            {
                snakeMoveDirection = Direction.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (snakeMoveDirection != Direction.Left)
            {
                snakeMoveDirection = Direction.Right;
            }
        }
    }
    /// <summary>
    /// di chuyen dua vao phim da nhan
    /// </summary>
    private void HandleGridMovement()
    {
        snakeMoveTimer += Time.deltaTime;
        if (snakeMoveTimer >= snakeMoveTimerMax)
        {
            snakeMoveTimer -= snakeMoveTimerMax;

            SnakeMovePosition previousSnakeMovePosition = null;
            if(snakeMovePositionList.Count > 0)
            {
                previousSnakeMovePosition = snakeMovePositionList[0];
            }
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition,snakePosition, snakeMoveDirection);
            snakeMovePositionList.Insert(0, snakeMovePosition);

            Vector2Int snakeMoveDirectionVector;
            switch(snakeMoveDirection){
                default:
                case Direction.Right: snakeMoveDirectionVector = new Vector2Int(+1, 0); break;
                case Direction.Left: snakeMoveDirectionVector = new Vector2Int(-1, 0); break;
                case Direction.Up: snakeMoveDirectionVector = new Vector2Int(0, +1); break;
                case Direction.Down: snakeMoveDirectionVector = new Vector2Int(0, -1); break;
                case Direction.Zero: snakeMoveDirectionVector = new Vector2Int(0, 0); break;

            }
            snakePosition += snakeMoveDirectionVector;

            snakePosition = levelSnake.ValidSnakePosition(snakePosition);

            bool snakeAteFood = levelSnake.TrySnakeEatFood(snakePosition);
            if (snakeAteFood)
            {
                // Snake ate food, grow body
                snakeBodySize++;
                CreateSnakeBodyPart();
                if(View.soundBite == 1)
                {
                    Audio.instance.Bite();
                }
            }

            if (snakeMovePositionList.Count >= snakeBodySize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }
            UpdateSnakeBodyParts();
            foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList)
            {
                Vector2Int snakeBodyPartgridPosition = snakeBodyPart.GetSnakePosition();
                if (snakePosition == snakeBodyPartgridPosition)
                {
                    Debug.Log("isDead");
                    isAlive = IsAlive.Dead;
                }
            }
            transform.position = new Vector3(snakePosition.x, snakePosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(snakeMoveDirectionVector) + 90);

            
        }
    }
    /// <summary>
    /// add body vao listBody
    /// </summary>
    private void CreateSnakeBodyPart()
    {
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));
    }

    /// <summary>
    /// update lai vitri cua body moi tao
    /// </summary>
    private void UpdateSnakeBodyParts()
    {
        for (int i = 0; i < snakeBodyPartList.Count; i++)
        {
            snakeBodyPartList[i].SetGridPosition(snakeMovePositionList[i]);
        }
    }

    /// <summary>
    /// dung de doi goc cua head
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public Vector2Int GetGridPosition()
    {
        return snakePosition;
    }
    /// <summary>
    /// tra ve vi tri cua ca body + head
    /// </summary>
    /// <returns></returns>
    public List<Vector2Int> GetFullSnakeGridPositionList()
    {
        List<Vector2Int> snakePositionList = new List<Vector2Int>() { snakePosition };
        foreach(SnakeMovePosition snakeMovePosition in snakeMovePositionList) {
            snakePositionList.Add(snakeMovePosition.GetSnakePosition());
        }
        return snakePositionList;
    }




    /// <summary>
    /// set up vi tri cua body
    /// </summary>
    private class SnakeBodyPart
    {

        private SnakeMovePosition snakeMovePosition;
        private Transform transform;

        public SnakeBodyPart(int bodyIndex)
        {
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakeBodySprite;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = 1 + bodyIndex;
            transform = snakeBodyGameObject.transform;
        }

        public void SetGridPosition(SnakeMovePosition snakeMovePosition)
        {
            this.snakeMovePosition = snakeMovePosition;
            transform.position = new Vector3(snakeMovePosition.GetSnakePosition().x, snakeMovePosition.GetSnakePosition().y);

            float angle;
            switch (snakeMovePosition.GetDirection())
            {
                default:
                case Direction.Up: angle =0 ; break;
                case Direction.Down: angle = 180 ; break;
                case Direction.Left: angle = -90 ; break;
                case Direction.Right: angle = 90; break;
            }
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
        public Vector2Int GetSnakePosition()
        {
            return snakeMovePosition.GetSnakePosition();
        }

    }
    private class SnakeMovePosition
    {
        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int snakePosition;
        private Direction direction;

        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int snakePosition, Direction direction)
        {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.snakePosition = snakePosition;
            this.direction = direction;
        }
        public Vector2Int GetSnakePosition()
        {
            return snakePosition;
        }
        public Direction GetDirection()
        {
            return direction;
        }
        public Direction GetPreviousDirection()
        {
            if(previousSnakeMovePosition == null)
            {
                return Direction.Right;
            }
            else
            {
                return previousSnakeMovePosition.direction;
            }
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "Wall")
    //    {
    //        isAlive = IsAlive.Dead;
    //    }
    //}
}
