using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.AI.Navigation;
using Random = UnityEngine.Random;

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

    private List<RoomAndCoord> roomList;

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
        roomList = new List<RoomAndCoord>();

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

        GenerateParts();

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
        foreach(RoomAndCoord room in roomList)
        {
            //check if up isn't connected
            if(room.z + 1 <= levelSize)
                if(array2D[room.x, room.z + 1] == 0)
                {
                    GameObject door = room.gameObj.transform.Find("DoorN").gameObject;
                    door.SetActive(true);
                }
            //check if right isn't connected
            if (room.x + 1 <= levelSize)
                if (array2D[room.x + 1, room.z] == 0)
                {
                    GameObject door = room.gameObj.transform.Find("DoorE").gameObject;
                    door.SetActive(true);
                }
            //check if down isn't connected
            if (room.z != 0)
                if (array2D[room.x, room.z - 1] == 0)
                {
                    GameObject door = room.gameObj.transform.Find("DoorS").gameObject;
                    door.SetActive(true);
                }
            //check if left isn't connected
            if (room.x != 0)
                if (array2D[room.x - 1, room.z] == 0)
                {
                    GameObject door = room.gameObj.transform.Find("DoorW").gameObject;
                    door.SetActive(true);
                }
        }
    }

    private void SpawnRoom()
    {
        int rand = Random.Range(0, rooms.Length);  
        array2D[xPos, zPos] = 1;
        RoomAndCoord newRoom = new RoomAndCoord(Instantiate(rooms[rand], transform.position, Quaternion.identity), xPos, zPos);
        roomList.Add(newRoom);
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

    private void SpawnWall()
    {
        

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

public class RoomAndCoord
{
    public GameObject gameObj;
    public int x;
    public int z;

    
    public RoomAndCoord(GameObject newGameObj, int newX, int newZ)
    {        
        gameObj = newGameObj;
        x = newX;
        z = newZ;
    }
    /*
    //This method is required by the IComparable
    //interface. 
    public int CompareTo(RoomAndCoord other)
    {
        if (other == null)
        {
            return 1;
        }

        //Return the difference in power.
        return x - other.x;
    }
    */
}
