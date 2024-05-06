
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
// Maze Craze v2020.08.31
//
// v2022.02.01
//


public class GameController : MonoBehaviour
{
    public static GameController gameController;


    // reference to player controller scripts
    public Player1Controller player1Prefab;
    public EnemyController enemyPrefab;

    // enemy sprite
    public Sprite enemySprite;

    public Transform enemyContainerTransform;

    // direction of player
    public const int STOPPED = 0;
    public const int UP = 1;
    public const int DOWN = -1;
    public const int LEFT = -1;
    public const int RIGHT = 1;


    public const int PLAYER_ONE = 1;
    //public const int PLAYER_TWO = 2;
    //public const int PLAYER_THREE = 3;
    //public const int PLAYER_FOUR = 4;

    public const string ENEMY_TAG = "Enemy";


    public const int START_SCORE = 0;
    //private const int WINNING_SCORE = 11;
    private const int GAMEOVER = 0;


    public const int INSERT_COINS = 0;
    public const int ONE_PLAYER_COINS = 1;
    public const int MAXIMUM_COINS = 99;


    // console initialisation
    private const string GAME_TITLE = "MAZE CRAZE";
    //private const int TV_MODE = AtariConsoleController.COLOUR_TV;
    public const int DEMO_MODE = 0;


    // player scores
    [HideInInspector] public int player1Score;
    //[HideInInspector] public int player2Score;
    //[HideInInspector] public int player3Score;
    //[HideInInspector] public int player4Score;

    // game credits
    [HideInInspector] public int gameCredits;

    [HideInInspector] public int player1StartPositionX;
    [HideInInspector] public int player1StartPositionY;

    [HideInInspector] public int enemyPositionX;
    [HideInInspector] public int enemyPositionY;

    private int numberOfEnemies;


    // game mode
    [HideInInspector] public bool canPlay;
    [HideInInspector] public bool inPlayMode;
    [HideInInspector] public bool inDemoMode;
    [HideInInspector] public bool inPawzMode;

    [HideInInspector] public bool player1Escaped;
    [HideInInspector] public bool player1Caught;
    [HideInInspector] public bool player2Escaped;
    [HideInInspector] public bool player2Caught;




    private void Awake()
    {
        gameController = this;
    }


    void Start()
    {
        Initialise();
    }


    private void Initialise()
    {
        InitialiseGameModes();

        //InitialiseConsoleSystem();

        //audioController.PlayAudioClip("Music");

        StartCoroutine(StartDemoMode());
    }


    private void InitialiseGameModes()
    {
        gameCredits = INSERT_COINS;

        UpdateGameCreditsText();

        canPlay = false;

        inPawzMode = false;
        inDemoMode = false;
        inPlayMode = false;
    }


    private void InitialiseConsoleSystem()
    {
        //AtariConsoleController.atariConsoleController.initialisingConsoleSystem = true;

        //AtariConsoleController.atariConsoleController.InitialiseConsole(GAME_TITLE, TV_MODE);
    }


    private void SetAtariConsoleMode(int consoleMode)
    {
        //AtariConsoleController.atariConsoleController.consoleMode = consoleMode;

        //AtariConsoleController.atariConsoleController.SetConsoleMode(consoleMode);
    }


    public void SetPawzMode()
    {
        //SetGamePadControllers();

        //ballSpriteController.FreezeBall();

        //SetAtariConsoleMode(AtariConsoleController.CONSOLE_VISIBLE);
    }


    public void SetPlayMode()
    {
        //SetAtariConsoleMode(AtariConsoleController.CONSOLE_HIDDEN);

        //SetGamePadControllers();

        //ballSpriteController.ResumeBall();
    }



    /*public void SetBallColour(int tvMode, int player)
    {
        if (tvMode == AtariConsoleController.COLOUR_TV && !inDemoMode)
        {
            switch (player)
            {
                case PLAYER_ONE:

                    // red
                    ballSpriteController.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(RED, 0, 0);

                    break;

                case PLAYER_TWO:

                    // green
                    ballSpriteController.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0, GREEN, 0);

                    break;

                case PLAYER_THREE:

                    // blue
                    ballSpriteController.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0, 0, BLUE);

                    break;

                case PLAYER_FOUR:

                    // yellow
                    ballSpriteController.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(RED, GREEN, 0);

                    break;
            }
        }

        else
        {
            // white
            //ballSpriteController.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(WHITE, WHITE, WHITE);
        }
    }*/


    // start demo mode
    public IEnumerator StartDemoMode()
    {
        //gameOverText.gameObject.SetActive(true);


       //LevelController._levelControllerInstance.level = DEMO_MODE;

        //MazeGenerator._mazeGeneratorInstance.BuildMaze();

        yield return new WaitForSeconds(1f);


        // start demo mode
        inDemoMode = true;
        inPlayMode = false;

        StartOnePlayerGame();
        //AtariConsoleController.atariConsoleController.SetPawzModeSwitches();

        // show atari console
        //SetAtariConsoleMode(AtariConsoleController.CONSOLE_VISIBLE);

        // check if there are any credits
        //if (gameCredits == INSERT_COINS)
        //{
            //insertCoinsText.gameObject.SetActive(true);
        //}

        //AtariConsoleController.atariConsoleController.SetGameSelection();

        //SetGameArenaBoundaries();

        //player1SpriteController.player1IsComputer = true;

        //player2SpriteController.player2IsComputer = true;

        //player2SpriteController.isPlayer2 = false;


        //player3SpriteController.player3IsComputer = true;

        //player3SpriteController.isPlayer3 = false;


        //player4SpriteController.player4IsComputer = true;

        //player4SpriteController.isPlayer4 = false;


        // initialise paddles
        //player1SpriteController.InitialiseSprite();

        //player2SpriteController.InitialiseSprite();

        //player3SpriteController.InitialisePaddle();

        //player4SpriteController.InitialisePaddle();


        // disable dpads
        //player1Dpad.gameObject.SetActive(false);

        //player2Dpad.gameObject.SetActive(false);

        //player3Dpad.gameObject.SetActive(false);

        //player4Dpad.gameObject.SetActive(false);


        // Enable ball
        //ballSpriteController.gameObject.SetActive(true);

        // Call ball controller script
        //ballSpriteController.InitialiseBall();
    }


    // Start one player game
    public void StartOnePlayerGame()
    {
        InitialiseGameMode();
    }


    // Start two player game
    public void StartTwoPlayerGame()
    {
        //player1SpriteController.player1IsComputer = false;


        //player2SpriteController.player2IsComputer = false;

        //player2SpriteController.isPlayer2 = true;


        //player3SpriteController.player3IsComputer = true;

        //player3SpriteController.isPlayer3 = false;


        //player4SpriteController.player4IsComputer = true;

        //player4SpriteController.isPlayer4 = false;


        InitialiseGameMode();
    }


    // Initialise
    private void InitialiseGameMode()
    {
        //gameCredits -= 1;

        //UpdateGameCreditsText();

       //if (gameCredits == INSERT_COINS)
        //{
            //canPlay = false;

            //AtariConsoleController.atariConsoleController.gameNumberSelected = AtariConsoleController.NO_GAME_SELECTED;

            //AtariConsoleController.atariConsoleController.SetGameSelection();
        //}

        //gameOverText.gameObject.SetActive(false);

        //selectGameText.gameObject.SetActive(false);

        //pressStartText.gameObject.SetActive(false);


        inPlayMode = true;
        inDemoMode = false;

        //AtariConsoleController.atariConsoleController.SetPawzModeSwitches();

        // hide atari console
        //SetAtariConsoleMode(AtariConsoleController.CONSOLE_HIDDEN);

        player1StartPositionX = 1;
        player1StartPositionY = 1;

        player1Caught = false;
        player1Escaped = false;

        numberOfEnemies = 6;

        // set initial level
        LevelController._levelControllerInstance.level = 1;

        StartCoroutine(StartLevel());
    }


    private IEnumerator StartLevel()
    {
        // generate maze
        MazeGenerator.mazeGenerator.BuildMaze();

        yield return new WaitForSeconds(1f);



        //InitialiseScore();

        // initialise player
        ReadyPlayer1();

        StartCoroutine(StartEnemy());

        //player2SpriteController.InitialiseSprite();

        Debug.Log("Level: " + LevelController._levelControllerInstance.level);
    }


    private IEnumerator StartEnemy()
    {
        yield return new WaitForSeconds(1f);

        SetEnemyStartPosition();

        PlaceEnemy();
    }


    private void ReadyPlayer1()
    {
        Player1Controller player1 = Instantiate(player1Prefab, new Vector3(player1StartPositionX, player1StartPositionY), Quaternion.identity);

        player1.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }


    private void PlacePlayer2()
    {
        /*Player1Controller player1 = Instantiate(player1Prefab, new Vector3(player1StartPositionX, player1StartPositionY), Quaternion.identity);

        player1.GetComponent<SpriteRenderer>().sortingOrder = 3;*/
    }


    private void SetEnemyStartPosition()
    {
        enemyPositionX = LevelController._levelControllerInstance.exitPositionX - 1;
        enemyPositionY = LevelController._levelControllerInstance.exitPositionY;
    }


    private void PlaceEnemy()
    {
        int sortingOrder = 3;

        for (int i = 0; i < numberOfEnemies; i++)
        {
            EnemyController enemy = Instantiate(enemyPrefab, new Vector3(enemyPositionX, enemyPositionY), Quaternion.identity);

            enemy.transform.SetParent(enemyContainerTransform);

            enemy.gameObject.name = ENEMY_TAG;

            enemy.gameObject.tag = ENEMY_TAG;

            enemy.SetSprite(enemySprite, sortingOrder);

            enemy.SetSpriteColour(0, 0, 255);
        }
    }


    private void InitialiseScore()
    {
        player1Score = START_SCORE;

        //player2Score = START_SCORE;

        //player3Score = 0;

        //player4Score = 0;

        UpdateScoreText();
    }


    public void UpdatePlayer1Score()
    {
        player1Score += 1;

        UpdateScoreText();
    }


    public void IsLevelComplete()
    {

    }


    public void LevelComplete()
    {
        //LevelController._levelControllerInstance.level++;

        StartCoroutine(StartLevel());
    }


    // check if game over
    public void IsGameOver(int playerScored)
    {
        // Check to see which player has won
        /*if (player1Score == WINNING_SCORE)
        {
            GameOver(PLAYER_ONE);
        }*/

        /*else if (player2Score == WINNING_SCORE)
        {
            GameOver(PLAYER_TWO);
        }*/


        // otherwise,
        // reset ball and set colour for player scored
        switch (playerScored)
        {
            case PLAYER_ONE:

                //SetBallColour(atariConsoleController.tvMode, PLAYER_ONE);

                break;

            /*case PLAYER_TWO:

                //SetBallColour(atariConsoleController.tvMode, PLAYER_TWO);

                break;*/
        }

        //ballSpriteController.ResetBall(ballSpriteController.ballSpeed, ballSpriteController.ballSpeed);
    }


    // When the game is over
    private void GameOver(int winner)
    {
        StartDemoMode();
    }


    // Update the player's scores
    private void UpdateScoreText()
    {
        //player1ScoreText.text = player1Score.ToString();

        //player2ScoreText.text = player2Score.ToString();

        //player3ScoreText.text = player3Score.ToString();

        //player4ScoreText.text = player4Score.ToString();
    }


    public void UpdateGameCreditsText()
    {
        //coinsInsertedText.text = gameCredits.ToString("00");
    }


} // end of class
