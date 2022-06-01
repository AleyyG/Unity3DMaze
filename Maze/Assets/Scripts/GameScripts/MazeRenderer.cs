using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeRenderer : MonoBehaviour
{
    public static MazeRenderer Current;

    [SerializeField]
    [Range(1, 50)]
    public int width = 3;

    [SerializeField]
    [Range(1, 50)]
    public int height = 3;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform floorPrefab = null;

    [SerializeField]
    public Transform ballPrefab = null;

    [SerializeField]
    private Transform holePrefab = null;

    private float maxXDistance;
    private float maxZDistance;
    private int randNumber;

    [Header("Slider Settings")]
    public Slider levelBar;
    public Image fill;
    public Gradient gradient;
    void Start()
    {
        Current = this;
        maxXDistance = (width - 1) / 2;
        maxZDistance = (height - 1) / 2;
        randNumber = Random.Range(1, 4);
        FinishPoint(randNumber);

        Transform createdBall = Instantiate(ballPrefab, this.transform);
        createdBall.SetParent(null);
        var maze = MazeGenerator.Generate(width, height);
        Draw(maze);
    }
    void Update()
    {
        var hole = GameObject.FindGameObjectWithTag("Hole");
        if(LevelController.Current.gameActive)
        {
            float distance = Vector3.Distance(hole.transform.position, BallMovement.Current.transform.position);
            levelBar.minValue = -levelBar.maxValue;
            levelBar.value = levelBar.maxValue - distance;
            fill.color = gradient.Evaluate(levelBar.value);
        }
        if (BallMovement.Current.isFinished)
            levelBar.value = levelBar.maxValue;
        
    }

    private void FinishPoint(int number)
    {
        if (height % 2 == 1 && width % 2 == 1)
        {
            if (number == 1)
            {
                var hole = Instantiate(holePrefab, new Vector3(transform.position.x + maxXDistance, transform.position.y, transform.position.z + maxZDistance), Quaternion.identity,this.transform) as Transform; //sağüst köşe
                hole.SetParent(null);
                float maxDistance = Vector3.Distance(transform.position, hole.position);
                levelBar.maxValue = maxDistance;
            }
            else if (number == 2)
            {
                var hole = Instantiate(holePrefab, new Vector3(transform.position.x + maxXDistance, transform.position.y, transform.position.z - maxZDistance), Quaternion.identity,this.transform) as Transform; // sağ alt köşe
                hole.SetParent(null);
                float maxDistance = Vector3.Distance(transform.position, hole.position);
                levelBar.maxValue = maxDistance;
            }
            else if (number == 3)
            {
                var hole = Instantiate(holePrefab, new Vector3(transform.position.x - maxXDistance, transform.position.y, transform.position.z + maxZDistance), Quaternion.identity,this.transform) as Transform; //sol üst köşe
                hole.SetParent(null);
                float maxDistance = Vector3.Distance(transform.position, hole.position);
                levelBar.maxValue = maxDistance;
            }
            else if (number == 4)
            {
                var hole = Instantiate(holePrefab, new Vector3(transform.position.x - maxXDistance, transform.position.y, transform.position.z - maxZDistance), Quaternion.identity,this.transform) as Transform ; // sol alt köşe
                hole.SetParent(null);
                float maxDistance = Vector3.Distance(transform.position, hole.position);
                levelBar.maxValue = maxDistance;
            }
                
        }
        else if (height % 2 == 0 && width % 2 == 1)
        {
            if (number == 1)
            {
                var hole = Instantiate(holePrefab, new Vector3(transform.position.x + maxXDistance, transform.position.y, transform.position.z + maxZDistance), Quaternion.identity,this.transform) as Transform; //sağ üst köşe
                hole.SetParent(null);
                float maxDistance = Vector3.Distance(transform.position, hole.position);
                levelBar.maxValue = maxDistance;
            }
               
            else if (number == 2)
            {
                var hole = Instantiate(holePrefab, new Vector3(transform.position.x + maxXDistance, transform.position.y, transform.position.z - (maxZDistance + 1)), Quaternion.identity,this.transform) as Transform; // sağ alt köşe
                hole.SetParent(null);
                float maxDistance = Vector3.Distance(transform.position, hole.position);
                levelBar.maxValue = maxDistance;
            }
               
            else if (number == 3)
            {
                var hole = Instantiate(holePrefab, new Vector3(transform.position.x - maxXDistance, transform.position.y, transform.position.z + maxZDistance), Quaternion.identity, this.transform) as Transform; // sol üst köşe
                hole.SetParent(null);
                float maxDistance = Vector3.Distance(transform.position, hole.position);
                levelBar.maxValue = maxDistance;
            }
                
            else if (number == 4)
            {
                var hole = Instantiate(holePrefab, new Vector3(transform.position.x - maxXDistance, transform.position.y, transform.position.z - (maxZDistance + 1)), Quaternion.identity, this.transform) as Transform ; // sol alt köşe
                hole.SetParent(null);
                float maxDistance = Vector3.Distance(transform.position, hole.position);
                levelBar.maxValue = maxDistance;
            }
                
        }
        else if (height % 2 == 1 && width % 2 == 0)
        {
            if (number == 1)
            {
                var hole = Instantiate(holePrefab, new Vector3(transform.position.x + maxXDistance, transform.position.y, transform.position.z + maxZDistance), Quaternion.identity, this.transform) as Transform;
                hole.SetParent(null);
                float maxDistance = Vector3.Distance(transform.position, hole.position);
                levelBar.maxValue = maxDistance;
            }
               
            else if (number == 2)
            {
                var hole = Instantiate(holePrefab, new Vector3(transform.position.x + maxXDistance, transform.position.y, transform.position.z - maxZDistance), Quaternion.identity, this.transform) as Transform;
                hole.SetParent(null);
                float maxDistance = Vector3.Distance(transform.position, hole.position);
                levelBar.maxValue = maxDistance;
            }
                
            else if (number == 3)
            {
                var hole = Instantiate(holePrefab, new Vector3(transform.position.x - (maxXDistance + 1), transform.position.y, transform.position.z + maxZDistance), Quaternion.identity, this.transform) as Transform;
                hole.SetParent(null);
                float maxDistance = Vector3.Distance(transform.position, hole.position);
                levelBar.maxValue = maxDistance;
            }
               
            else if (number == 4)
            {
                var hole = Instantiate(holePrefab, new Vector3(transform.position.x - (maxXDistance + 1), transform.position.y, transform.position.z - maxZDistance), Quaternion.identity, this.transform) as Transform;
                hole.SetParent(null);
                float maxDistance = Vector3.Distance(transform.position, hole.position);
                levelBar.maxValue = maxDistance;
            }
                
        }
        else
        {
            if (number == 1)
            {
                var hole = Instantiate(holePrefab, new Vector3(transform.position.x + maxXDistance, transform.position.y, transform.position.z + maxZDistance), Quaternion.identity, this.transform) as Transform;
                hole.SetParent(null);
                float maxDistance = Vector3.Distance(transform.position, hole.position);
                levelBar.maxValue = maxDistance;
            }
               
            else if (number == 2)
            {
                var hole = Instantiate(holePrefab, new Vector3(transform.position.x + maxXDistance, transform.position.y, transform.position.z - (maxZDistance + 1)), Quaternion.identity, this.transform) as Transform;
                hole.SetParent(null);
                float maxDistance = Vector3.Distance(transform.position, hole.position);
                levelBar.maxValue = maxDistance;
            }
               
            else if (number == 3)
            {
                var hole = Instantiate(holePrefab, new Vector3(transform.position.x - (maxXDistance + 1), transform.position.y, transform.position.z + maxZDistance), Quaternion.identity, this.transform) as Transform;
                hole.SetParent(null);
                float maxDistance = Vector3.Distance(transform.position, hole.position);
                levelBar.maxValue = maxDistance;
            }
                
            else if (number == 4)
            {
                var hole = Instantiate(holePrefab, new Vector3(transform.position.x - (maxXDistance + 1), transform.position.y, transform.position.z - (maxZDistance + 1)), Quaternion.identity, this.transform) as Transform;
                hole.SetParent(null);
                float maxDistance = Vector3.Distance(transform.position, hole.position);
                levelBar.maxValue = maxDistance;
            }
                
        }


    }
    public void Draw(WallState[,] maze)
    {
        var floor = Instantiate(floorPrefab, transform);
        floor.localScale = new Vector3(width, 1, height);

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                var cell = maze[i, j];
                var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, size / 2);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0, 0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if (i == width - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(+size / 2, 0, 0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if (j == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, 0, -size / 2);
                        bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);
                    }
                }
            }

        }

    }
}
