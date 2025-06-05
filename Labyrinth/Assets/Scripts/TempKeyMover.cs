using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempKeyMover : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    void Update()
    {
        float movementAmount = moveSpeed * Time.deltaTime;
        transform.Translate(0, 0, movementAmount);
    }
}
