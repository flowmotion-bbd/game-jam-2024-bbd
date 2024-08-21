using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class FlameChase : MonoBehaviour
{
    public Flame flame { get; private set; }

    public Vector2 quadPrevDirection = Vector2.zero;

    public Vector2 tertPrevDirection = Vector2.zero;

    public Vector2 prevPrevDirection = Vector2.zero;

    public Vector2 prevDirection = Vector2.zero;

    public bool scatter = false;

    public void Awake()
    {
        this.flame = GetComponent<Flame>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Path path = collision.GetComponent<Path>();

        if (path != null && scatter == false) 
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            if (path.availableDirections.Count == 1) 
            { 
                direction = path.availableDirections[0];
            } else
            {
                foreach (Vector2 availableDirection in path.availableDirections)
                {
                    Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                    float distance = (this.flame.hackerman.position - newPosition).sqrMagnitude;
                    if (distance < minDistance)
                    {
                        if (!(availableDirection==prevDirection && availableDirection == tertPrevDirection && prevPrevDirection == quadPrevDirection && !ZeroChecks()))
                        {
                            minDistance = distance;
                            direction = availableDirection;
                        } else
                        {
                            direction = path.availableDirections[Random.Range(0, path.availableDirections.Count)];
                            scatter = true;
                            Invoke("Unscatter", 5f);
                            break;
                        }
                        
                    }
                }
            }
            
            quadPrevDirection = tertPrevDirection;
            tertPrevDirection = prevPrevDirection;
            prevPrevDirection = prevDirection;
            prevDirection = this.flame.movement.direction;
            this.flame.movement.SetDirection(direction);
        }
        else if (path != null && scatter == true)
        {
            int index = Random.Range(0, path.availableDirections.Count);

            if (path.availableDirections[index] == -this.flame.movement.direction && path.availableDirections.Count > 1)
            {
                index++;

                if(index >= path.availableDirections.Count){
                    index = 0;
                }
            }
            this.flame.movement.SetDirection(path.availableDirections[index]);
            quadPrevDirection = Vector2.zero;
            tertPrevDirection = Vector2.zero;
            prevPrevDirection = Vector2.zero;
            prevDirection = Vector2.zero;
        }
    }

    private bool ZeroChecks()
    {
        return prevDirection == Vector2.zero || prevPrevDirection == Vector2.zero || tertPrevDirection == Vector2.zero || quadPrevDirection == Vector2.zero;
    }

    private void Unscatter()
    {
        scatter = false;
    }
}
