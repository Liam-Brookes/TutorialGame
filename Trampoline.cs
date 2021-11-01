using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 vec = new Vector3(collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.y);
        rb = collision.gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(vec * 500);
    }
}
