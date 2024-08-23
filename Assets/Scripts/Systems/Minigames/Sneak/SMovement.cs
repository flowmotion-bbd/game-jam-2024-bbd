using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMovement : MonoBehaviour
{
    public Rigidbody2D myRigidbody { get; private set; }

    public float acccelaratorFactor = 0.5f;
    public float turnFactor = 0.02f;

    float acccelarationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;

    Vector2 speed = Vector2.zero;

    private void Awake()
    {
        this.myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplySpeedBall();
        ApplyDirectionBall();
    }

    private void ApplySpeedBall()
    {
        Vector2 speedForceVector = transform.up * acccelarationInput * acccelaratorFactor;

        myRigidbody.AddForce(speedForceVector, ForceMode2D.Force);
    }

    private void ApplyDirectionBall()
    {
        Vector2 speedForceVector = transform.right * steeringInput * acccelaratorFactor;
        myRigidbody.AddForce(speedForceVector, ForceMode2D.Force);
    }

    public void SetInputVector(Vector2 inputVector) 
    {
        steeringInput = inputVector.x;
        acccelarationInput = inputVector.y;
        speed = inputVector;
    }
}
