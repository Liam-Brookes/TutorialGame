using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;
    Camera cam;
    public float y;
    GameObject cam2;
    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        cam2 = cam.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (cam2.transform.position.y < player.transform.position.y) y = player.transform.position.y; else if (player.transform.position.y < cam2.transform.position.y && cam2.transform.position.y > 0) y = player.transform.position.y;
        if (y < 0) y = 0;
        cam2.transform.position = new Vector3(player.transform.position.x, y, 0);
    }
}
