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

    [SerializeField] private GameObject roomStart;
    [SerializeField] private GameObject roomEnd;
    [SerializeField] private GameObject roomCorner;
    [SerializeField] private GameObject roomDoor;

    [SerializeField] private GameObject roomI;
    [SerializeField] private GameObject roomL;
    [SerializeField] private GameObject roomT;
    [SerializeField] private GameObject roomX;

    [SerializeField] private Transform worldGeometry;
    [SerializeField] private GameObject player;

    private List<RoomAndCoord> roomList;

    private NavMeshSurface surface;


    private int xLimit, zLimit;
    private int xPos, zPos;
    private int xOffset, zOffset;

    private int triesToDeadend = 0;
    private bool deadend = false;

    public static int[,] array2D;

    private enum MoveDir { Up, Down, Left, Right }
    private MoveDir prevMove;
    private MoveDir nextMove;

    private RoomType roomType;
    private GameObject newRoom;


    // Start is called before the first frame update
    private void Awake()
    {
        array2D = new int[levelSize, levelSize];
        roomList = new List<RoomAndCoord>();

        xPos = Mathf.RoundToInt(levelSize / 2);
        zPos = Mathf.RoundToInt(levelSize / 2);

        xOffset = xPos * centerDistance;
        zOffset = zPos * centerDistance;

        array2D[xPos, zPos] = 1;

        for (int i = 0; i < roomAmount; i++)
        {
            if(!deadend) SpawnPath(i);
        }

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
            if (room.roomType == RoomType.Start) newRoom = roomStart;
            if (room.roomType == RoomType.End) newRoom = roomEnd;
            if (room.roomType == RoomType.I) newRoom = roomI;
            if (room.roomType == RoomType.L) newRoom = roomL;
            Instantiate(newRoom, new Vector3(room.x * centerDistance -xOffset , 0, room.z * centerDistance - zOffset), room.rotation);
        }
    }

    private void SpawnPath(int index)
    {
        Quaternion startRot = Quaternion.identity;
        array2D[xPos, zPos] = 1;
        RoomAndCoord newRoom;
        prevMove = nextMove;        

        if (index == 0)

        {
            Move();
            if (nextMove == MoveDir.Up) startRot = Quaternion.Euler(0f, 180f, 0f);
            if (nextMove == MoveDir.Down) startRot = Quaternion.Euler(0f, 180f, 0f);
            if (nextMove == MoveDir.Right) startRot = Quaternion.Euler(0f, 270f, 0f);
            if (nextMove == MoveDir.Left) startRot = Quaternion.Euler(0f, 90f, 0f);
            newRoom = new RoomAndCoord(xPos, zPos, RoomType.Start, startRot);
            if (nextMove == MoveDir.Up) zPos += 1;
            if (nextMove == MoveDir.Down) zPos -= 1;
            if (nextMove == MoveDir.Right) xPos += 1;
            if (nextMove == MoveDir.Left) xPos -= 1;

        }
        else
        {
            prevMove = nextMove;
            Move();
            newRoom = GetRoomType(prevMove, nextMove);
            if (nextMove == MoveDir.Up) zPos += 1;
            if (nextMove == MoveDir.Down) zPos -= 1;
            if (nextMove == MoveDir.Right) xPos += 1;
            if (nextMove == MoveDir.Left) xPos -= 1;
        }
        roomList.Add(newRoom);
        triesToDeadend = 0;
        
    }

    private RoomAndCoord GetRoomType(MoveDir lastMove, MoveDir nextMove)
    {
        Quaternion rotation = Quaternion.identity;
        roomType = RoomType.Start;
        // Straight piece roomI
        if ((lastMove == MoveDir.Up && nextMove == MoveDir.Up) || (lastMove == MoveDir.Down && nextMove == MoveDir.Down))
        {
            roomType = RoomType.I;
        }
        if ((lastMove == MoveDir.Left && nextMove == MoveDir.Left) || (lastMove == MoveDir.Right && nextMove == MoveDir.Right))
        {
            roomType = RoomType.I;
            rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        // Turn piece roomL
        if ((lastMove == MoveDir.Up && nextMove == MoveDir.Right) || (lastMove == MoveDir.Left && nextMove == MoveDir.Down))
        {
            roomType = RoomType.L;
            rotation = Quaternion.Euler(0f, -90f, 0f);
        }
        if ((lastMove == MoveDir.Right && nextMove == MoveDir.Down) || (lastMove == MoveDir.Up && nextMove == MoveDir.Left))
        {
            roomType = RoomType.L;
            rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        if ((lastMove == MoveDir.Down && nextMove == MoveDir.Left) || (lastMove == MoveDir.Right && nextMove == MoveDir.Up))
        {
            roomType = RoomType.L;
            rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        if ((lastMove == MoveDir.Left && nextMove == MoveDir.Up) || (lastMove == MoveDir.Down && nextMove == MoveDir.Right))
        {
            roomType = RoomType.L;
            rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        return new RoomAndCoord(xPos, zPos, roomType, rotation);
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
                    transform.position += Vector3.forward * centerDistance;
                    nextMove = MoveDir.Up;
                }                
                break;

            case 1:
                if (xPos >= levelSize - 1 || array2D[xPos + 1, zPos] == 1)
                {
                    Move();
                }
                else
                {
                    transform.position += Vector3.right * centerDistance;
                    nextMove = MoveDir.Right;
                }                
                break;

            case 2:
                if (zPos <= 0 || array2D[xPos, zPos - 1] == 1)
                {
                    Move();
                }                    
                else
                {
                    transform.position += Vector3.back * centerDistance;
                    nextMove = MoveDir.Down;
                }
                break;

            case 3:
                if (xPos <= 0 || array2D[xPos - 1, zPos] == 1)
                {
                    Move();
                }
                else
                {
                    transform.position += Vector3.left * centerDistance;
                    nextMove = MoveDir.Left;
                }
                break;
        }
    }
}

public class RoomAndCoord
{
    public int x;
    public int z;
    public RoomType roomType;
    public Quaternion rotation;
    
    public RoomAndCoord(int newX, int newZ, RoomType newRoomType, Quaternion newRotation)
    {        
        x = newX;
        z = newZ;
        roomType = newRoomType;
        rotation = newRotation;
    }
}

public enum RoomType { Start, End, I, L, T, X }
