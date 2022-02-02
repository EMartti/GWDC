using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] rooms;
    [SerializeField] private int moveAmount = 1;
    [SerializeField] private int roomAmount = 1;
    [Range(5,8)][SerializeField] private int levelSize = 1;

    private int xLimit, zLimit;
    private int xPos, zPos;

    private bool deadend = true;

    // Start is called before the first frame update
    void Start()
    {
        int[,] array2D = new int[levelSize, levelSize];

        xPos = Random.Range(Mathf.RoundToInt(levelSize / 2), Mathf.RoundToInt(levelSize / 2) + 1);
        zPos = Random.Range(Mathf.RoundToInt(levelSize / 2), Mathf.RoundToInt(levelSize / 2) + 1);

        for (int i = 0; i < roomAmount; i++)
        {
            if(!deadend) SpawnRoom(array2D);
        }
        
    }

    private void SpawnRoom(int[,] array2D)
    {
        int rand = Random.Range(0, rooms.Length);
        Instantiate(rooms[rand], transform.position, Quaternion.identity);

        array2D[xPos, zPos] = 1;
        
        Move(array2D);
    }

    private void Move(int[,] array2D)
    {
        //0 is forward 1 is right etc clockwise
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                if(zPos >= levelSize - 1)
                {
                    Move(array2D);
                }
                else
                {
                    transform.position += Vector3.forward * moveAmount;
                    zPos++;

                }                
                break;

            case 1:
                if (xPos >= levelSize - 1 && array2D[xPos + 1, zPos] == 1)
                {
                    Move(array2D);
                }
                else
                {
                    transform.position += Vector3.right * moveAmount;
                    xPos++;
                }                
                break;

            case 2:
                if (zPos <= 0 && array2D[xPos, zPos - 1] == 1)
                {
                    Move(array2D);
                }
                else
                {
                    transform.position += Vector3.back * moveAmount;
                    zPos--;
                }
                break;

            case 3:
                if (xPos <= 0 && array2D[xPos - 1, zPos] == 1)
                {
                    Move(array2D);
                }
                else
                {
                    transform.position += Vector3.left * moveAmount;
                    xPos--;
                }
                break;
        }
    }

    private void CheckSurround()
    {
        for (int i = 0; i < 8; i++)
        {

        }
    }
}
