using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] rooms;
    [SerializeField] private int centerDistance = 1;
    [SerializeField] private int roomAmount = 1;
    [Range(5,16)][SerializeField] private int levelSize = 1;

    [SerializeField] private GameObject roomMid;
    [SerializeField] private GameObject roomEdge;
    [SerializeField] private GameObject roomCorner;
    [SerializeField] private GameObject roomDoor;

    [SerializeField] private Transform worldGeometry;
    [SerializeField] private GameObject player;

    private NavMeshSurface surface;


    private int xLimit, zLimit;
    private int xPos, zPos;

    private int triesToDeadend = 0;
    private bool deadend = false;

    public static int[,] array2D;

    // Start is called before the first frame update
    private void Awake()
    {
        array2D = new int[levelSize, levelSize];       

        xPos = 2;
        zPos = 2;       

        array2D[xPos, zPos] = 1;
        //CheckSurround(xPos, zPos);

        //DebugArray();

        for (int i = 0; i < roomAmount; i++)
        {
            if(!deadend) SpawnRoom();
        }

        //DebugArray();

        //GenerateParts();

        GenerateNavMesh();
    }

    private void GenerateNavMesh()
    {
        if (worldGeometry == null || worldGeometry.GetComponent<NavMeshSurface>() == null)
        {
            Debug.Log("World geometry of Floor Manager must be assigned and must have at least one NavMeshSurface");
            gameObject.SetActive(false);
            return;
        }

        surface = worldGeometry.GetComponentInChildren<NavMeshSurface>();

        surface.BuildNavMesh();
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
                transform.position = new Vector3(x * centerDistance/3, 0, z * centerDistance/3);
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
        Instantiate(rooms[rand], transform.position, Quaternion.identity);
        array2D[xPos, zPos] = 1;
        triesToDeadend = 0;
        Move();
    }

    private void Move()
    {
        //This decides the walk direction of the level generation
        triesToDeadend++;
        if (triesToDeadend >= 20) { deadend = true; return; }
        //0 is forward 1 is right etc clockwise
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                if(zPos >= levelSize - 1 || array2D[xPos, zPos + 1] == 1)
                {
                    Move();
                }
                else
                {                   
                    zPos += 1;                    
                    transform.position += Vector3.forward * centerDistance;
                }                
                break;

            case 1:
                if (xPos >= levelSize - 1 || array2D[xPos + 1, zPos] == 1)
                {
                    Move();
                }
                else
                {
                    xPos += 1;
                    transform.position += Vector3.right * centerDistance;
                }                
                break;

            case 2:
                if (zPos <= 0 || array2D[xPos, zPos - 1] == 1)
                {
                    Move();
                }                    
                else
                {
                    zPos -= 1;
                    transform.position += Vector3.back * centerDistance;
                }
                break;

            case 3:
                if (xPos <= 0 || array2D[xPos - 1, zPos] == 1)
                {
                    Move();
                }
                else
                {
                    xPos -= 1;
                    transform.position += Vector3.left * centerDistance;
                }
                break;
        }
    }

    private void SpawnCorners()
    {
        transform.position += Vector3.forward * centerDistance / 3;
        transform.position += Vector3.right * centerDistance / 3;
        Instantiate(roomCorner, transform.position, Quaternion.LookRotation(Vector3.right, Vector3.up));

        transform.position += Vector3.back * 2 * centerDistance / 3;
        Instantiate(roomCorner, transform.position, Quaternion.LookRotation(Vector3.back, Vector3.up));

        transform.position += Vector3.left * 2 * centerDistance / 3;
        Instantiate(roomCorner, transform.position, Quaternion.LookRotation(Vector3.left, Vector3.up));

        transform.position += Vector3.forward * 2 * centerDistance / 3;
        Instantiate(roomCorner, transform.position, Quaternion.LookRotation(Vector3.forward, Vector3.up));

        transform.position += Vector3.right * centerDistance / 3;
        transform.position += Vector3.back * centerDistance / 3;
    }

    private void SpawnWall(Vector3 moveDir)
    {
        //flip forwards and right axis
        int tempX = (int) -moveDir.z;
        int tempZ = (int) moveDir.x;

        Vector3 crossDir = new Vector3(tempX, 0, tempZ);

        //check grids left side from direction   

        transform.position += crossDir * (centerDistance / 3);

        if (array2D[xPos + (int)crossDir.x, zPos + (int)crossDir.z] == 0) 
            Instantiate(roomEdge, transform.position, Quaternion.LookRotation(moveDir, Vector3.up));
        else
        {
            transform.position -= crossDir * (centerDistance / 3);
            transform.position -= moveDir * (centerDistance / 3);

            Instantiate(roomEdge, transform.position, Quaternion.LookRotation(crossDir, Vector3.up));

            transform.position += crossDir * (centerDistance / 3);
            transform.position += moveDir * (centerDistance / 3);
        }

        transform.position -= crossDir * (2 * centerDistance / 3);

        if (array2D[xPos - (int)crossDir.x, zPos - (int)crossDir.z] == 0) 
            Instantiate(roomEdge, transform.position, Quaternion.LookRotation(-moveDir, Vector3.up));
        else
        {
            transform.position += crossDir * (centerDistance / 3);
            transform.position += moveDir * (centerDistance / 3);

            Instantiate(roomEdge, transform.position, Quaternion.LookRotation(-crossDir, Vector3.up));

            transform.position -= crossDir * (centerDistance / 3);
            transform.position -= moveDir * (centerDistance / 3);
        }

        transform.position += crossDir * (centerDistance / 3);

    }

    private void SpawnDoor(Vector3 moveDir)
    {
        transform.position += moveDir * (centerDistance / 3);
        Instantiate(roomDoor, transform.position, Quaternion.LookRotation(moveDir, Vector3.up));
        transform.position += moveDir * (centerDistance / 3);
        Instantiate(roomDoor, transform.position, Quaternion.LookRotation(-moveDir, Vector3.up));
        transform.position -= moveDir * (2 * centerDistance / 3);
    }


    private void CheckSurround(int PosX, int PosZ)
    {
        int tempX = PosX - 1;
        int tempZ = PosZ - 1;

        //Debug.Log(PosX + " " + PosZ);

        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                if (array2D[tempX + x,tempZ + z] == 0) array2D[tempX + x, tempZ + z] = 2;
                //if (array2D[tempX + x, tempZ + z] % 2 != 0 && array2D[tempX + x, tempZ + z] != 1) array2D[tempX + x, tempZ + z] = 3;
            }
        }
    }
}
