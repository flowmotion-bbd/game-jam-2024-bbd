using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flame : MonoBehaviour
{
    public Movement movement { get; private set; }
    public FlameChase chase { get; private set; }
    public Transform hackerman;
    public void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.chase = GetComponent<FlameChase>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("HackerMan"))
        {
            FindObjectOfType<FirewallMinigameManager>().GameOver(false);
        }
    }
}
