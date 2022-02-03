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

        xPos = Random.Range(Mathf.RoundToInt(levelSize / 6), Mathf.RoundToInt(levelSize / 6) + 3);
        zPos = Random.Range(Mathf.RoundToInt(levelSize / 6), Mathf.RoundToInt(levelSize / 6) + 3);
        
        for (int i = 0; i < roomAmount; i++)
        {
            if(!deadend) SpawnRoom();
        }
        
    }

    private void SpawnRoom()
    {
        int rand = Random.Range(0, rooms.Length);
        Instantiate(rooms[rand], transform.position, Quaternion.identity);

        array2D[xPos, zPos] = 1;
        Debug.Log("spawned at: " + xPos + ", " + zPos + "value: " + array2D[xPos, zPos]);

        triesToDeadend = 0;
        Move();
    }

    private void Move()
    {
        triesToDeadend++;
        if (triesToDeadend >= 20) return;
        //0 is forward 1 is right etc clockwise
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                if(zPos >= levelSize - 1 || array2D[xPos, zPos + 3] < 1)
                {
                    Move();
                }
                else
                {
                    array2D[xPos, zPos] = 1;
                    transform.position += Vector3.forward * moveAmount;
                    zPos++;
                    movedTo();
                }                
                break;

            case 1:
                if (xPos >= levelSize - 1 || array2D[xPos + 1, zPos] < 1)
                {
                    Move();
                }
                else
                {
                    SpawnDoor(Vector3.right);
                    SpawnWall(Vector3.right);
                    SpawnCorners();
                    transform.position += Vector3.right * moveAmount;
                    xPos++;
                    movedTo();
                }                
                break;

            case 2:
                if (zPos <= 0 || array2D[xPos, zPos - 1] < 1)
                {
                    Move();
                }
                else
                {
                    SpawnDoor(Vector3.back);
                    SpawnWall(Vector3.back);
                    SpawnCorners();
                    transform.position += Vector3.back * moveAmount;
                    zPos--;
                    movedTo();
                }
                break;

            case 3:
                if (xPos <= 0 || array2D[xPos - 1, zPos] < 1)
                {
                    Move();
                }
                else
                {
                    SpawnDoor(Vector3.left);
                    SpawnWall(Vector3.left);
                    SpawnCorners();
                    transform.position += Vector3.left * moveAmount;
                    xPos--;
                    movedTo();
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

    private void movedTo()
    {
        Debug.Log("moved to: " + xPos + ", " + zPos + "value: " + array2D[xPos, zPos]);
    }

    private void CheckSurround()
    {
        for (int i = 0; i < 8; i++)
        {
            //plaah
        }
    }
}
