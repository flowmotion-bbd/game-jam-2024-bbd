using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rigidbody { get; private set; }

    public float speed = 8.0f;
    public Vector2 initialDirection;
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }
    public LayerMask obstacleLayer;
    public float speedMultiplier = 1.0f;

    private FirewallMinigameManager firewallMinigameManager;

    // Start is called before the first frame update
    void Start()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.initialDirection;
        this.nextDirection = this.nextDirection;
        this.rigidbody.isKinematic = false;
        this.enabled = true;
        firewallMinigameManager = FindAnyObjectByType<FirewallMinigameManager>();

    }

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (this.nextDirection != Vector2.zero)
        {
            SetDirection(this.nextDirection);
        }
    }

    void FixedUpdate()
    {
        if (firewallMinigameManager.MinigameInProgess)
        {
            Vector2 position = this.rigidbody.position;
            Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
            this.rigidbody.MovePosition(position + translation);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        if (!Occupied(direction))
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        }
        else
        {
            this.nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, direction, 1f, this.obstacleLayer);
        return hit.collider != null;
    }
}
