using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] rooms;
    [SerializeField] private int moveAmount = 1;
    [SerializeField] private int roomAmount = 1;
    [Range(5,8)][SerializeField] private int levelSize = 1;

    [SerializeField] private GameObject roomMid;
    [SerializeField] private GameObject roomEdge;
    [SerializeField] private GameObject roomCorner;
    [SerializeField] private GameObject roomDoor;

    private int xLimit, zLimit;
    private int xPos, zPos;

    private int triesToDeadend = 0;
    private bool deadend = false;

    public static int[,] array2D;

    // Start is called before the first frame update
    void Start()
    {
        array2D = new int[levelSize * 3, levelSize * 3];

        xPos = Random.Range(Mathf.RoundToInt(levelSize * 3 / 2), Mathf.RoundToInt(levelSize * 3 / 2) + 3);
        zPos = Random.Range(Mathf.RoundToInt(levelSize * 3 / 2), Mathf.RoundToInt(levelSize * 3 / 2) + 3);
        
        for (int i = 0; i < roomAmount; i++)
        {
            if(!deadend) SpawnRoom();
        }

        DebugArray();

        GenerateParts();
    }

    private void DebugArray()
    {
        string gridString = "";

        for (int x = 0; x < levelSize * 3; x++)
        {
            for (int z = 0; z < levelSize * 3; z++)
            {
                gridString += array2D[x, z];
                gridString += " ";
            }
            gridString += "\n";
        }

        Debug.Log(gridString);
    }

    private void GenerateParts()
    {
        for (int x = 0; x < levelSize * 3; x++)
        {
            for (int z = 0; z < levelSize * 3; z++)
            {
                transform.position = new Vector3(x, 0, z);
                switch (array2D[x, z])
                {
                    case 1:
                        Instantiate(roomMid, transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(roomEdge, transform.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(roomCorner, transform.position, Quaternion.identity);
                        break;
                }
            }
        }
    }

    private void SpawnRoom()
    {
        int rand = Random.Range(0, rooms.Length);

        array2D[xPos, zPos] = 1;
        triesToDeadend = 0;
        Move();
    }

    private void Move()
    {
        //This decides the walk direction of the level generation
        triesToDeadend++;
        if (triesToDeadend >= 20) return;
        //0 is forward 1 is right etc clockwise
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                if(array2D[xPos, zPos + 3] < 1)
                {
                    Move();
                }
                else
                {
                    array2D[xPos, zPos] = 1;
                    CheckSurround(xPos, zPos);
                    zPos += 3;
                }                
                break;

            case 1:
                if (array2D[xPos + 3, zPos] < 1)
                {
                    Move();
                }
                else
                {
                    array2D[xPos, zPos] = 1;
                    CheckSurround(xPos, zPos);
                    xPos += 3;
                }                
                break;

            case 2:
                if (array2D[xPos, zPos - 3] < 1)
                {
                    Move();
                }
                else
                {
                    array2D[xPos, zPos] = 1;
                    CheckSurround(xPos, zPos);
                    zPos -= 3;
                }
                break;

            case 3:
                if (array2D[xPos - 3, zPos] < 1)
                {
                    Move();
                }
                else
                {
                    array2D[xPos, zPos] = 1;
                    CheckSurround(xPos, zPos);
                    xPos -= 3;
                }
                break;
        }
    }

    private void SpawnCorners()
    {
        transform.position += Vector3.forward * moveAmount / 3;
        transform.position += Vector3.right * moveAmount / 3;
        Instantiate(roomCorner, transform.position, Quaternion.LookRotation(Vector3.right, Vector3.up));

        transform.position += Vector3.back * 2 * moveAmount / 3;
        Instantiate(roomCorner, transform.position, Quaternion.LookRotation(Vector3.back, Vector3.up));

        transform.position += Vector3.left * 2 * moveAmount / 3;
        Instantiate(roomCorner, transform.position, Quaternion.LookRotation(Vector3.left, Vector3.up));

        transform.position += Vector3.forward * 2 * moveAmount / 3;
        Instantiate(roomCorner, transform.position, Quaternion.LookRotation(Vector3.forward, Vector3.up));

        transform.position += Vector3.right * moveAmount / 3;
        transform.position += Vector3.back * moveAmount / 3;
    }

    private void SpawnWall(Vector3 moveDir)
    {
        //flip forwards and right axis
        int tempX = (int) -moveDir.z;
        int tempZ = (int) moveDir.x;

        Vector3 crossDir = new Vector3(tempX, 0, tempZ);

        //check grids left side from direction   

        transform.position += crossDir * (moveAmount / 3);

        if (array2D[xPos + (int)crossDir.x, zPos + (int)crossDir.z] == 0) 
            Instantiate(roomEdge, transform.position, Quaternion.LookRotation(moveDir, Vector3.up));
        else
        {
            transform.position -= crossDir * (moveAmount / 3);
            transform.position -= moveDir * (moveAmount / 3);

            Instantiate(roomEdge, transform.position, Quaternion.LookRotation(crossDir, Vector3.up));

            transform.position += crossDir * (moveAmount / 3);
            transform.position += moveDir * (moveAmount / 3);
        }

        transform.position -= crossDir * (2 * moveAmount / 3);

        if (array2D[xPos - (int)crossDir.x, zPos - (int)crossDir.z] == 0) 
            Instantiate(roomEdge, transform.position, Quaternion.LookRotation(-moveDir, Vector3.up));
        else
        {
            transform.position += crossDir * (moveAmount / 3);
            transform.position += moveDir * (moveAmount / 3);

            Instantiate(roomEdge, transform.position, Quaternion.LookRotation(-crossDir, Vector3.up));

            transform.position -= crossDir * (moveAmount / 3);
            transform.position -= moveDir * (moveAmount / 3);
        }

        transform.position += crossDir * (moveAmount / 3);

    }

    private void SpawnDoor(Vector3 moveDir)
    {
        transform.position += moveDir * (moveAmount / 3);
        Instantiate(roomDoor, transform.position, Quaternion.LookRotation(moveDir, Vector3.up));
        transform.position += moveDir * (moveAmount / 3);
        Instantiate(roomDoor, transform.position, Quaternion.LookRotation(-moveDir, Vector3.up));
        transform.position -= moveDir * (2 * moveAmount / 3);
    }


    private void CheckSurround(int PosX, int PosZ)
    {
        int[,] surroundingPos = new int[5, 5];
        int tempX = PosX - 2;
        int tempZ = PosZ - 2;

        for (int x = 0; x < 5; x++)
        {
            for (int z = 0; z < 5; z++)
            {
                if (surroundingPos[tempX + x,tempZ + z] == 0) surroundingPos[tempX + x, tempZ + z] = 2;
                if (surroundingPos[tempX + x, tempZ + z] % 2 != 0 && surroundingPos[tempX + x, tempZ + z] != 1) surroundingPos[tempX + x, tempZ + z] = 3;
            }
        }
    }
}
