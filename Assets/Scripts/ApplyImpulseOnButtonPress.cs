using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ApplyImpulseOnButtonPress : MonoBehaviour
{
    public float strength = 500;
    public float torque = 100;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            Debug.Log("null keyboard");
            return;
        }
            

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Space!");

            rb.AddForce(Vector3.up * strength);
            rb.AddRelativeTorque(Random.onUnitSphere * torque);
        }
    }
}
