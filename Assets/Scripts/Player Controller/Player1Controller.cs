
using UnityEngine;


// 
// Maze Craze v2019.02.24
// 
// v2022.02.01
// 


public class Player1Controller : MonoBehaviour
{    
    public static Player1Controller player1Controller;

    public Sprite playerIdleSprite;
    public Sprite playerLeftSprite;
    public Sprite playerRightSprite;
    public Sprite playerUpSprite;
    public Sprite playerDownSprite;

    private SpriteRenderer spriteRenderer;

    // player start position
    private int player1CurrentPositionX;
    private int player1CurrentPositionY;

    private int player1TargetX;
    private int player1TargetY;

    private int player1SpriteMoveSpeed;

    private int player1MoveDirection;

    private Vector2 direction;



    private void Awake()
    {
        player1Controller = this;

        spriteRenderer = GetComponent<SpriteRenderer>();

        Initialise();
    }


    void Update()
    {
        if (!GameController.gameController.player1Escaped && !GameController.gameController.player1Caught) //(!GameController.gameController.inPawzMode)
        {
            GetControllerInput();

            MovePlayer1();
        }
    }


    public void Initialise()
    {
        // direction of player
        player1MoveDirection = GameController.STOPPED;

        // reset player 1 start position
        player1CurrentPositionX = GameController.gameController.player1StartPositionX;
        player1CurrentPositionY = GameController.gameController.player1StartPositionY;

        player1TargetX = player1CurrentPositionX;
        player1TargetY = player1CurrentPositionY;

        player1SpriteMoveSpeed = 10;
    }


    private void GetControllerInput()
    {
        player1CurrentPositionX = Mathf.FloorToInt(transform.position.x);
        player1CurrentPositionY = Mathf.FloorToInt(transform.position.y);

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");


        // moving right
        if (direction.x > 0)
        {
            MovePlayer1SpriteRight();
        }


        else if (direction.x < 0)
        {
            MovePlayer1SpriteLeft();
        }


        else if (direction.y > 0)
        {
            MovePlayer1SpriteUp();
        }


        else if (direction.y < 0)
        {
            MovePlayer1SpriteDown();
        }


        else
        {
            SetPlayerSprite(playerIdleSprite);
        }
    }


    private void MovePlayer1SpriteLeft()
    {
        int mazeCell = MazeGenerator.mazeGenerator.ReadMazeTile(player1CurrentPositionX - 1, player1CurrentPositionY);


        if (mazeCell == MazeGenerator.PATH && CenteredOnTile())
        {
            player1MoveDirection = GameController.LEFT;

            player1TargetX = player1CurrentPositionX - 1;

            player1TargetY = player1CurrentPositionY;

            SetPlayerSprite(playerLeftSprite);
        }
    }


    private void MovePlayer1SpriteRight()
    {
        int mazeCell = MazeGenerator.mazeGenerator.ReadMazeTile(player1CurrentPositionX + 1, player1CurrentPositionY);


        if (mazeCell == MazeGenerator.PATH && CenteredOnTile() || mazeCell == MazeGenerator.EXIT && CenteredOnTile())
        {
            player1MoveDirection = GameController.RIGHT;

            player1TargetX = player1CurrentPositionX + 1;

            player1TargetY = player1CurrentPositionY;

            SetPlayerSprite(playerRightSprite);
        }
    }


    private void MovePlayer1SpriteUp()
    {
        int mazeCell = MazeGenerator.mazeGenerator.ReadMazeTile(player1CurrentPositionX, player1CurrentPositionY + 1);


        if (mazeCell == MazeGenerator.PATH && CenteredOnTile())
        {
            player1MoveDirection = GameController.UP;

            player1TargetX = player1CurrentPositionX;

            player1TargetY = player1CurrentPositionY + 1;

            SetPlayerSprite(playerUpSprite);
        }
    }


    private void MovePlayer1SpriteDown()
    {
        int mazeCell = MazeGenerator.mazeGenerator.ReadMazeTile(player1CurrentPositionX, player1CurrentPositionY - 1);


        if (mazeCell == MazeGenerator.PATH && CenteredOnTile())
        {
            player1MoveDirection = GameController.DOWN;

            player1TargetX = player1CurrentPositionX;

            player1TargetY = player1CurrentPositionY - 1;

            SetPlayerSprite(playerDownSprite);
        }
    }


    private bool CenteredOnTile()
    {
        return transform.position.x == player1TargetX && transform.position.y == player1TargetY;
    }


    private void SetPlayerSprite(Sprite playerSprite)
    {
        spriteRenderer.sprite = playerSprite;
    }


    private void MovePlayer1()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player1TargetX, player1TargetY), player1SpriteMoveSpeed * Time.deltaTime);
    }


    public void OnTriggerEnter2D(Collider2D collidingObject)
    {
        if (collidingObject.gameObject.CompareTag(LevelController.EXIT_TAG))
        {
            GameController.gameController.player1Escaped = true;
        }

        if (collidingObject.gameObject.CompareTag(GameController.ENEMY_TAG))
        {
            GameController.gameController.player1Caught = true;
        }
    }


} // end of class
