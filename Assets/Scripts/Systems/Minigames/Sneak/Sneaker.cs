using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Sneaker : MonoBehaviour
{
    private SneakMinigameManager sneakMinigameManager;

    public float dimTime = 10.0f;
    public float undimTime = 3.0f;
    public bool dim = false;
    private Light2D playerLight;

    private float maxLightRadius;

    private void Awake()
    {
        playerLight = GetComponent<Light2D>();
        sneakMinigameManager = FindAnyObjectByType<SneakMinigameManager>();
        maxLightRadius = playerLight.pointLightOuterRadius;
        Invoke("DimLight", dimTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            sneakMinigameManager.GameOver(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            sneakMinigameManager.GameOver(true);
        }
    }

    private void FixedUpdate()
    {
        if (dim && playerLight.pointLightOuterRadius > 1)
        {
            playerLight.pointLightOuterRadius -= 1;
        } else if (!dim && playerLight.pointLightOuterRadius < maxLightRadius)
        {
            playerLight.pointLightOuterRadius += 1;
        }
    }

    void DimLight()
    {
        dim = true;
        Invoke("UndimLight", undimTime);
    }

    void UndimLight()
    {
        dim = false;
        Invoke("DimLight", dimTime);
    }
}
