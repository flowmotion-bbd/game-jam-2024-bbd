using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hackerman : MonoBehaviour
{
    private Movement movement;
    private FirewallMinigameManager firewallMinigameManager;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        firewallMinigameManager = FindAnyObjectByType<FirewallMinigameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            AudioManager.Instance.PlaySFX("retro_win");
            firewallMinigameManager.GameOver(true);
        } else if (collision.gameObject.layer == LayerMask.NameToLayer("Fire"))
        {
            firewallMinigameManager.GameOver(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (firewallMinigameManager.MinigameInProgess)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                movement.SetDirection(Vector2.up);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                movement.SetDirection(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                movement.SetDirection(Vector2.down);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                movement.SetDirection(Vector2.right);
            }
        }
    }
}
