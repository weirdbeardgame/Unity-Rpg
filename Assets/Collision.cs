using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private CollisionMessage _Collided;
    private int _ID;

    public void Initalize(int ID)
    {
        _ID = ID;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision ID: " + _ID);
            _Collided = ScriptableObject.CreateInstance<CollisionMessage>();
            _Collided.construct(_ID);
        }
    }

}
