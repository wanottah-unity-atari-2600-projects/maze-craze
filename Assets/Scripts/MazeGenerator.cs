
using System.Collections.Generic;
using UnityEngine;

//
// Maze Craze, Atari 1978, v2019.02.24
//
// v2022.02.01
//


public class MazeGenerator : MonoBehaviour
{
    public static MazeGenerator mazeGenerator;

    // reference to maze sprite script
    public MazeSprite mazeSpritePrefab;

    // maze sprite
    public Sprite mazeSprite;

    public Transform path;


    // maze
    public const int MAZE_WIDTH = 38;
    public const int MAZE_HEIGHT = 24;

    private const int LEFT = -1;
    private const int RIGHT = 1;
    private const int UP = 1;
    private const int DOWN = -1;

    private const int PATH_START_X = 1;
    private const int PATH_START_Y = 1;

    public const int WALL = 0;
    public const int PATH = 1;
    public const int EXIT = 2;

    private const string WALL_TAG = "Wall";
    public const string PATH_TAG = "Path";


    [HideInInspector] public int mazeWidth;
    [HideInInspector] public int mazeHeight;

    private int[] mazeArray;

    [HideInInspector] public List<MazeSprite> pathArray;

    private int nodes;



    private void Awake()
    {
        mazeGenerator = this;

        path.gameObject.SetActive(false);

        Initialise();

        InitialiseMaze();   
    }


    public void BuildMaze()
    {
        ResetMaze();

        GenerateMaze();

        LevelController._levelControllerInstance.InitialiseLevel();
    }


    private void Initialise()
    {
        mazeWidth = MAZE_WIDTH;

        mazeHeight = MAZE_HEIGHT;

        if (mazeWidth % 2 == 0)
        {
            mazeWidth += 1;
        }

        if (mazeHeight % 2 == 0)
        {
            mazeHeight += 1;
        }

        mazeArray = new int[mazeWidth * mazeHeight];

        pathArray = new List<MazeSprite>(mazeWidth * mazeHeight);

        nodes = (( mazeWidth * mazeHeight ) / 4) - 1;
    }


    private void InitialiseMaze()
    {
        for (int tileY = 0; tileY < mazeHeight; tileY++)
        {
            for (int tileX = 0; tileX < mazeWidth; tileX++)
            {
                Vector3 tilePosition;

                if (tileX > 0 || tileY > 0)
                {
                    tilePosition = 
                        
                        new Vector3(tileX, tileY);
                }

                else
                {
                    tilePosition = new Vector3(tileX, tileY);
                }

                SetMazeTile(tileX, tileY, WALL);

                MazeSprite mazeWall = Instantiate(mazeSpritePrefab, tilePosition, Quaternion.identity);

                pathArray.Add(mazeWall);

                mazeWall.name = WALL_TAG;

                mazeWall.tag = WALL_TAG;

                mazeWall.SetSprite(mazeSprite, 0);

                mazeWall.SetSpriteColour(8, 136, 23);

                mazeWall.transform.SetParent(transform);
            }
        }
    }


    private void ResetMaze()
    {
        for (int tileY = 0; tileY < mazeHeight; tileY++)
        {
            for (int tileX = 0; tileX < mazeWidth; tileX++)
            {
                if (ReadMazeTile(tileX, tileY) == PATH)
                {
                    pathArray[tileX + tileY * mazeWidth].SetSpriteColour(8, 136, 23);

                    pathArray[tileX + tileY * mazeWidth].name = WALL_TAG;

                    pathArray[tileX + tileY * mazeWidth].tag = WALL_TAG;
                }

                SetMazeTile(tileX, tileY, WALL);
            }
        }
    }


    private void GenerateMaze()
    {
        int mazePathX = PATH_START_X;
        int mazePathY = PATH_START_Y;

        int tileType;

        int pathLength = 0;

        int pathDirections;
        int randomPathDirection;

        int randomPathCellX;
        int randomPathCellY;

        int[] newPathDirectionX = new int[nodes];
        int[] newPathDirectionY = new int[nodes];

        int[] pathTraceX = new int[nodes];
        int[] pathTraceY = new int[nodes];



        // repeat until pathCount = 0
        do
        {

            // repeat until vectorCount = 0
            do
            {
                SetMazeTile(mazePathX, mazePathY, PATH);

                // check & store possible path directions in pathVector()
                pathDirections = 0;


                // > - 2
                if (mazePathY > 2)
                {
                    tileType = ReadMazeTile(mazePathX, mazePathY - 2);

                    if (tileType == WALL)
                    {
                        newPathDirectionX[pathDirections] = 0;

                        newPathDirectionY[pathDirections] = DOWN;

                        pathDirections++;
                    }

                }


                if (mazePathX < mazeWidth - 2)
                {
                    tileType = ReadMazeTile(mazePathX + 2, mazePathY);

                    if (tileType == WALL)
                    {
                        newPathDirectionX[pathDirections] = RIGHT;

                        newPathDirectionY[pathDirections] = 0;

                        pathDirections++;
                    }
                }


                // < 2
                if (mazePathY < mazeHeight - 2)
                {
                    tileType = ReadMazeTile(mazePathX, mazePathY + 2);

                    if (tileType == WALL)
                    {
                        newPathDirectionX[pathDirections] = 0;

                        newPathDirectionY[pathDirections] = UP;

                        pathDirections++;
                    }

                }


                if (mazePathX > 2)
                {
                    tileType = ReadMazeTile(mazePathX - 2, mazePathY);

                    if (tileType == WALL)
                    {
                        newPathDirectionX[pathDirections] = LEFT;

                        newPathDirectionY[pathDirections] = 0;

                        pathDirections++;
                    }

                }


                // if no possible path directions find and remove start position from mPathStart()
                if (pathDirections == 0)
                {

                    if (pathLength > 0)
                    {

                        for (int pathSearch = pathLength - 1; pathSearch > 0; pathSearch--)
                        {

                            if (pathTraceX[pathSearch] == mazePathX && pathTraceY[pathSearch] == mazePathY)
                            {
                                pathTraceX[pathSearch] = 0; // array delete element mPathStart(),mPSearch

                                pathTraceY[pathSearch] = 0;
                            }

                        }

                        pathLength--;
                    }
                }

                else
                {
                    // if possible path directions, randomly pick one & set array to path
                    randomPathDirection = Random.Range(0, pathDirections);

                    randomPathCellX = mazePathX + newPathDirectionX[randomPathDirection];

                    randomPathCellY = mazePathY + newPathDirectionY[randomPathDirection];

                    SetMazeTile(randomPathCellX, randomPathCellY, PATH);

                    // move to new location
                    mazePathX += (newPathDirectionX[randomPathDirection] * 2);

                    mazePathY += (newPathDirectionY[randomPathDirection] * 2);

                    // add valid path start position to mPathStart() array
                    pathTraceX[pathLength] = mazePathX;

                    pathTraceY[pathLength] = mazePathY;

                    pathLength++;
                }
            }

            // repeat building path until we can't move - vectorCount = 0
            while (pathDirections != 0);


            // find a new path start position from current paths
            if (pathLength > 0)
            {
                mazePathX = pathTraceX[pathLength - 1];

                mazePathY = pathTraceY[pathLength - 1];
            }
        }

        // repeat until no more possible paths - until pathCount = 0
        while (pathLength != 0);
    }


    public void SetMazeTile(int mazeTileX, int mazeTileY, int mazeTile)
    {
        mazeArray[mazeTileX + mazeTileY * mazeWidth] = mazeTile;
    }


    public int ReadMazeTile(int mazeTileX, int mazeTileY)
    {
        return mazeArray[mazeTileX + mazeTileY * mazeWidth];
    }


} // end of class
