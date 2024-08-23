using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMovement : MonoBehaviour
{
    public Rigidbody2D rigidbody { get; private set; }

    public float acccelaratorFactor = 0.5f;
    public float turnFactor = 0.02f;

    float acccelarationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;

    Vector2 speed = Vector2.zero;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplySpeedBall();
        ApplyDirectionBall();
    }

    private void ApplySpeedBall()
    {
        Vector2 speedForceVector = transform.up * acccelarationInput * acccelaratorFactor;

        rigidbody.AddForce(speedForceVector, ForceMode2D.Force);
    }

    private void ApplyDirectionBall()
    {
        Vector2 speedForceVector = transform.right * steeringInput * acccelaratorFactor;
        rigidbody.AddForce(speedForceVector, ForceMode2D.Force);
    }

    private void ApplySpeedCar()
    {
        Vector2 speedForceVector = transform.up * acccelarationInput * acccelaratorFactor;

        rigidbody.AddForce(speedForceVector, ForceMode2D.Force);
    }

    private void ApplyDirectionCar()
    {
        rotationAngle -= steeringInput * turnFactor;
        rigidbody.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector) 
    {
        steeringInput = inputVector.x;
        acccelarationInput = inputVector.y;
        speed = inputVector;
    }
}
