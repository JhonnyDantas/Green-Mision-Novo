using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;


    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, 0.20f * Time.deltaTime);
    }
}
