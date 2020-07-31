using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public enum MoveDirections { UP, DOWN, LEFT, RIGHT, STOP, RESET }

public class NPCMovement : MonoBehaviour
{
    Vector3 direction;

    float movementSpeed;

    int moveToX;
    int moveToY;

    // The valid movespace
    int boxX;
    int boxY;

    float CurrentTime;

    int MovementIndex = 0;

    [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
    List<MoveDirections> Directions;

    private void Start()
    {
        Directions = new List<MoveDirections>();
    }


    public void Initalize(List<MoveDirections> Dir)
    {
        Directions = new List<MoveDirections>();
        Directions = Dir;
        Move();

    }

    public int GetCount()
    {
        return Directions.Count;
    }

    // What I need is to concatanate the same command over and over again. 
    // So If there's multiple UP they should be put into one slot with a 
    // Multiplier that dictates the amount of times that command occurs? 

    //You Dumb Bitch you forgot DeltaTime!

    public void Move()
    {
        for (int i = 0; i < Directions.Count; i += 1)
        {
            switch (Directions[i])
            {
                case MoveDirections.DOWN:
                    movementSpeed = 2;
                    direction.y = -1;
                    GetComponent<RectTransform>().position += direction * movementSpeed;
                    break;
                case MoveDirections.UP:
                    movementSpeed = 2;
                    direction.y = 1;
                    GetComponent<RectTransform>().position += direction * movementSpeed;
                    break;
                case MoveDirections.LEFT:
                    movementSpeed = 2;
                    direction.x = -1;
                    GetComponent<RectTransform>().position += direction * movementSpeed;
                    break;
                case MoveDirections.RIGHT:
                    movementSpeed = 2;
                    direction.x = 1;
                    GetComponent<RectTransform>().position += direction * movementSpeed;
                    break;
                case MoveDirections.STOP:
                    direction = Vector2.zero;
                    movementSpeed = 0;
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    break;
            }

            if (i == Directions.Count)
            {
                i = 0;
                Move();
            }

        }
    }

    public List<MoveDirections> SetDirections(MoveDirections Move)
    {
        if (Directions != null)
        {
            Directions.Add(Move);
            return Directions;
        }

        return null;
    }


    void Update()
    {
        Move();
    }
}

