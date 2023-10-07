using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public Camera cameraToFollow;
    public Vector2 offset;

    void Update()
    {
        // Update the position of the background object
        transform.position = new Vector3(cameraToFollow.transform.position.x + offset.x, cameraToFollow.transform.position.y + offset.y, transform.position.z);
    }
}

