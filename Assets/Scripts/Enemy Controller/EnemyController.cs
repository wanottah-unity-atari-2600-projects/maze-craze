
using System.Collections.Generic;
using UnityEngine;

//
// created 10-03-2020
//
// v2022.02.01
//

public class EnemyController : MonoBehaviour
{
    public static EnemyController _enemyInstance;



    public Sprite enemySprite;

    // reference to game object's sprite renderer component
    private SpriteRenderer spriteRenderer;



    // movement
    private const int LEFT = 1;
    private const int DOWN = 2;
    private const int RIGHT = 3;
    private const int UP = 4;

    private const int POSSIBLE_DIRECTIONS = 4;

    // patrol mode
    private const int ALPHA = 0;
    private const int BRAVO = 1;

    private List<string> patrolModes;


    private int enemyMoveDirection;
    private int patrolMode;

    private int directionCount = 0;

    private int enemyX;
    private int enemyY;

    private int enemyTargetX;
    private int enemyTargetY;

    private int enemyMoveSpeed;
    private float enemyRotationSpeed;

    private float currentAngle;
    private float lastAngle;
    private float newAngle;



    private void Awake()
    {
        _enemyInstance = this;

        // set reference to the sprite renderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        Initialise();
    }


    // Update is called once per frame
    void Update()
    {
        RoundPosition();

        MoveEnemy();
    }



    public void Initialise()
    {
        enemyX = GameController.gameController.enemyPositionX;
        enemyY = GameController.gameController.enemyPositionY;

        enemyTargetX = enemyX;
        enemyTargetY = enemyY;

        enemyMoveSpeed = 3; // 6; // 12; // 24;

        enemyRotationSpeed = 12;

        enemyMoveDirection = LEFT;

        patrolModes = new List<string> { "ALPHA", "BRAVO" };

        patrolMode = Random.Range(0, patrolModes.Count);

        directionCount = 0;
    }


    private void RoundPosition()
    {
        enemyX = Mathf.FloorToInt(transform.position.x);
        enemyY = Mathf.FloorToInt(transform.position.y);

        StateController();
    }


    private void StateController()
    {
        switch (enemyMoveDirection)
        {
            case LEFT:

                MoveLeft();

                break;


            case DOWN:

                MoveDown();

                break;


            case RIGHT:

                MoveRight();

                break;


            case UP:

                MoveUp();

                break;
        }
    }


    private void MoveLeft()
    {
        if (CenteredOnTile())
        {
            switch (patrolMode)
            {
                case ALPHA:

                    CheckNewPosition(enemyX - 1, enemyY, DOWN, UP);

                    break;


                case BRAVO:

                    CheckNewPosition(enemyX - 1, enemyY, UP, DOWN);

                    break;
            }
        }
    }


    private void MoveDown()
    {
        if (CenteredOnTile())
        {
            switch (patrolMode)
            {
                case ALPHA:

                    CheckNewPosition(enemyX, enemyY - 1, RIGHT, LEFT);

                    break;


                case BRAVO:

                    CheckNewPosition(enemyX, enemyY - 1, LEFT, RIGHT);

                    break;
            }
        }
    }


    private void MoveRight()
    {
        if (CenteredOnTile())
        {
            switch (patrolMode)
            {
                case ALPHA:

                    CheckNewPosition(enemyX + 1, enemyY, UP, DOWN);

                    break;


                case BRAVO:

                    CheckNewPosition(enemyX + 1, enemyY, DOWN, UP);

                    break;
            }
        }
    }


    private void MoveUp()
    {
        if (CenteredOnTile())
        {
            switch (patrolMode)
            {
                case ALPHA:

                    CheckNewPosition(enemyX, enemyY + 1, LEFT, RIGHT);

                    break;


                case BRAVO:

                    CheckNewPosition(enemyX, enemyY + 1, RIGHT, LEFT);

                    break;
            }
        }
    }


    private bool CenteredOnTile()
    {
        return transform.position.x == enemyTargetX && transform.position.y == enemyTargetY;
    }


    private void CheckNewPosition(int newPositionX, int newPositionY, int currentDirection, int newDirection)
    {
        int mazeTile = MazeGenerator.mazeGenerator.ReadMazeTile(newPositionX, newPositionY);


        if (mazeTile == MazeGenerator.PATH)
        {
            enemyTargetX = newPositionX;

            enemyTargetY = newPositionY;

            enemyMoveDirection = currentDirection;

            directionCount = 0;
        }

        else
        {
            enemyMoveDirection = newDirection;

            directionCount++;

            bool inCulDeSac = (POSSIBLE_DIRECTIONS - directionCount) == 1;


            if (inCulDeSac)
            {
                patrolMode = Random.Range(0, patrolModes.Count);
            }


            switch (enemyMoveDirection)
            {
                case LEFT:

                    newAngle = 90;

                    break;


                case DOWN:

                    newAngle = 180;

                    break;


                case RIGHT:

                    newAngle = 270;

                    break;


                case UP:

                    newAngle = 0;

                    break;
            }
        }
    }


    private void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(enemyTargetX, enemyTargetY), enemyMoveSpeed * Time.deltaTime);
    }


    // sets the required sprite and sprite's sorting order to the game object
    public void SetSprite(Sprite sprite, int sortingOrder)
    {
        // set the sprite
        spriteRenderer.sprite = sprite;

        // set the sorting order for the sprite
        spriteRenderer.sortingOrder = sortingOrder;
    }


    public void SetSpriteColour(float r, float g, float b)
    {
        // set the sprite colour
        r /= 255;
        g /= 255;
        b /= 255;

        spriteRenderer.color = new Color(r, g, b);
    }


} // end of class
