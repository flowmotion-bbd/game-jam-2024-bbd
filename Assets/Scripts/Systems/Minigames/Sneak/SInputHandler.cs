using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SInputHandler : MonoBehaviour
{
    SMovement sMovement;
    SneakMinigameManager sneakMinigameManager;

    private void Awake()
    {
        sMovement = GetComponent<SMovement>();
        sneakMinigameManager = FindAnyObjectByType<SneakMinigameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sneakMinigameManager.MinigameInProgess)
        {
            Vector2 inputVector = Vector2.zero;

            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");

            sMovement.SetInputVector(inputVector);
        }
    }
}
