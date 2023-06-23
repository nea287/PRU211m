using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    // sound play when pick up new check point
    private Transform currentCheckPoint;
    private Heart playerHeart;

    private void Awake()
    {
        playerHeart = GetComponent<Heart>();
    }

    public void Reset()
    {
        transform.position = currentCheckPoint.position;
        // move player to checkpoint position
        // restore player health and reset animation
        playerHeart.Respawn();
        // Move camera to checkpoint room

    }
}
