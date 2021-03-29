using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneHandler : MonoBehaviour
{
    public Transform respawnPoint;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        other.transform.position = respawnPoint.position;
    }
}
