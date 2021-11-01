using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleElavator : MonoBehaviour
{



    /// <summary>
    /// If true it goes up if false it goes down
    /// </summary>
    public bool GoesUp;
    /// <summary>
    /// The rate at which the player goes up
    /// </summary>\
    public float velocity = 100;
    /// <summary>
    /// The Player gameobjects
    /// </summary>
    public GameObject Player;
    /// <summary>
    /// The player's rigidbody
    /// </summary>
    Rigidbody2D rb;

    /// <summary>
    /// which way the elavator goes
    /// </summary>
    Vector2 way;
    // Start is called before the first frame update
    private void Awake()
    {
    
    }
    void Start()
    {
        if (GoesUp)
        {
            way = Vector2.down;
        }
        else
        {
            way = Vector2.up;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.attachedRigidbody.AddForce(way*velocity);
    }
}
