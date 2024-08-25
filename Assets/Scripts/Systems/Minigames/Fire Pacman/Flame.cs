using UnityEngine;
using UnityEngine.AI;

public class Flame : MonoBehaviour
{
    public Movement movement { get; private set; }
    private Transform hackerman;
    private NavMeshAgent agent;

    private FirewallMinigameManager firewallMinigameManager;

    public void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.agent = GetComponent<NavMeshAgent>();
        this.hackerman = FindAnyObjectByType<Hackerman>().transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(hackerman.position);
        //InvokeRepeating("SetDestination", 2f, 1f);
        firewallMinigameManager = FindAnyObjectByType<FirewallMinigameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Path path = collision.GetComponent<Path>();

        if (path != null)
        {
            agent.SetDestination(hackerman.position);
        }
    }

    void FixedUpdate()
    {
        if (firewallMinigameManager.MinigameInProgess)
        {
            agent.isStopped = false;
            if (agent.remainingDistance < 0.3)
            {
                agent.SetDestination(hackerman.position);
            }
        } else
        {
            agent.isStopped = true;
        }
    }
}
