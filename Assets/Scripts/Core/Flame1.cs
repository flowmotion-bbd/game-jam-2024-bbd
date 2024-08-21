using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flame1 : MonoBehaviour
{
    public Movement movement { get; private set; }
    public FlameChase chase { get; private set; }
    public Transform hackerman;
    public NavMeshAgent agent;
    public void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.chase = GetComponent<FlameChase>();
        this.agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(hackerman.position);
        //InvokeRepeating("SetDestination", 2f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        agent.SetDestination(hackerman.position);
        if (collision.gameObject.layer == LayerMask.NameToLayer("HackerMan"))
        {
            FindObjectOfType<FirewallMinigameManager>().GameOver(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Path path = collision.GetComponent<Path>();

        if (path != null)
        {
            agent.SetDestination(hackerman.position);
        }
    }
    private void SetDestination()
    {
        agent.SetDestination(hackerman.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance < 0.3)
        {
            agent.SetDestination(hackerman.position);
        }
    }
}
