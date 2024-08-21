using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Sneaker : MonoBehaviour
{
    public float dimTime = 10.0f;
    public float undimTime = 3.0f;
    public bool dim = false;
    public Light2D light;
    private void Awake()
    {
        light = GetComponent<Light2D>();
        Invoke("DimLight", dimTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            FindObjectOfType<SneakMinigameManager>().GameOver(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            FindObjectOfType<SneakMinigameManager>().GameOver(true);
        }
    }

    private void FixedUpdate()
    {
        if (dim && light.pointLightOuterRadius > 1)
        {
            light.pointLightOuterRadius -= 1;
        } else if (!dim && light.pointLightOuterRadius < 200)
        {
            light.pointLightOuterRadius += 1;
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
