
using UnityEngine;

//
// Maze Craze v2020.08.31
//
// v2022.02.01
//


public class LevelController : MonoBehaviour
{
    public static LevelController _levelControllerInstance;

    public MazeSprite exitPrefab;

    public Sprite exitSprite;

    public const string EXIT_TAG = "Exit";

    [HideInInspector] public int exitPositionX;
    [HideInInspector] public int exitPositionY;

    [HideInInspector] public int level;



    private void Awake()
    {
        _levelControllerInstance = this;
    }


    public void InitialiseLevel()
    {
        switch (level)
        {
            #region DEMO MODE

            case GameController.DEMO_MODE:

                DrawMaze();

                break;

            #endregion


            #region LEVEL 1

            // level 1 -    get to the exit
            case 1:

                DrawMaze();

                SetExitPosition();

                PlaceExit();

                break;

            #endregion
        }
    }


    private void SetExitPosition()
    {
        int exitTile;

        exitPositionX = MazeGenerator.MAZE_WIDTH;

        do
        {
            int exitX = MazeGenerator.MAZE_WIDTH - 1;

            exitPositionY = Random.Range(1, MazeGenerator.MAZE_HEIGHT);

            exitTile = MazeGenerator.mazeGenerator.ReadMazeTile(exitX, exitPositionY);
        }

        while (exitTile == MazeGenerator.WALL);

        MazeGenerator.mazeGenerator.SetMazeTile(exitPositionX, exitPositionY, MazeGenerator.EXIT);
    }


    private void PlaceExit()
    {
        int sortingOrder = 1;

        MazeSprite exit = Instantiate(exitPrefab, new Vector3(exitPositionX, exitPositionY), Quaternion.identity);

        exit.transform.SetParent(transform);

        //exit.gameObject.name = EXIT_TAG;

        //exit.gameObject.tag = EXIT_TAG;

        exit.SetSprite(exitSprite, sortingOrder);

        exit.SetSpriteColour(255, 129, 30);
    }


    private void DrawMaze()
    {
        for (int tileY = 0; tileY < MazeGenerator.mazeGenerator.mazeHeight; tileY++)
        {
            for (int tileX = 0; tileX < MazeGenerator.mazeGenerator.mazeWidth; tileX++)
            {
                if (MazeGenerator.mazeGenerator.ReadMazeTile(tileX, tileY) == MazeGenerator.PATH)
                {
                    MazeGenerator.mazeGenerator.pathArray[tileX + tileY * MazeGenerator.mazeGenerator.mazeWidth].SetSpriteColour(255, 129, 30);

                    MazeGenerator.mazeGenerator.pathArray[tileX + tileY * MazeGenerator.mazeGenerator.mazeWidth].gameObject.name = MazeGenerator.PATH_TAG;

                    MazeGenerator.mazeGenerator.pathArray[tileX + tileY * MazeGenerator.mazeGenerator.mazeWidth].gameObject.tag = MazeGenerator.PATH_TAG;
                }
            }
        }

        MazeGenerator.mazeGenerator.path.gameObject.SetActive(true);
    }


} // end of class
