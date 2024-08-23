using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SInputHandler : MonoBehaviour
{
    SMovement sMovement;

    private void Awake()
    {
        sMovement = GetComponent<SMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        sMovement.SetInputVector(inputVector);
    }
}
