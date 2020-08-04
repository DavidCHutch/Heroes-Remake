using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 velocity;
    private Vector3 direction;
    private bool hasMoved;

    public Tilemap fogTileMap;
    [SerializeField] private int vision = 3;

    private void Update()
    {
        if (velocity.x == 0)
        {
            hasMoved = false;
        }
        else if (velocity.x != 0 && !hasMoved)
        {
            hasMoved = true;
            MoveByDirection();
        }

        velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void MoveByDirection()
    {
        if (velocity.x < 0) //Move left
        {
            if (velocity.y > 0) //Move upper left
            {
                direction = new Vector3(-0.5f, 0.5f);
            }
            else if (velocity.y < 0) //Move bottom left
            {
                direction = new Vector3(-0.5f, -0.5f);
            }
            else //Move left
            {
                direction = new Vector3(-1f, 0f);
            }
        }
        else if (velocity.x > 0) //Move right
        {
            if (velocity.y > 0) //Move upper left
            {
                direction = new Vector3(0.5f, 0.5f);
            }
            else if (velocity.y < 0) //Move bottom left
            {
                direction = new Vector3(0.5f, -0.5f);
            }
            else //Move left
            {
                direction = new Vector3(1f, 0f);
            }
        }

        transform.position += direction;
        UpdateFog();
    }

    //MARKER Once we attach an obstacle (contains Collider2D component)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position -= direction;
    }

    private void UpdateFog()
    {
        Vector3Int currentPlayerPos = fogTileMap.WorldToCell(transform.position); //Cell position converts to World position

        for(int i = -3; i <= 3; i++)
        {
            for(int j = -3; j <= 3; j++)
            {
                fogTileMap.SetTile(currentPlayerPos + new Vector3Int(i, j, 0), null);
            }
        }
    }
}
