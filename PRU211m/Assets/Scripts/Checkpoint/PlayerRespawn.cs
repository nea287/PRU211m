using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    private Transform currentCheckpoint;
    //private Heart playerHealth;
    private AudioSource audioSource;
    private static int live = 3;

    private void Awake()
    {
        // playerHealth = GetComponent<Heart>();
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
    }
    public void RespawnCheck()
    {

        if (currentCheckpoint == null)
        {
            return;
        }

        transform.position = currentCheckpoint.position; //Move player to checkpoint location
        live--;
        if (live == 0)
        {
            SceneManager.LoadScene("GameOver");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("activate");
            audioSource.PlayOneShot(checkpoint);
        }
    }
}