using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private Vector3[] wheelSuspensionConnectionPoints;
    private BoxCollider boxCollider;
    private Rigidbody rb;

    public float suspensionMaxLength = 0.6f;
    public float suspensionMaxForce = 10f;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();

        wheelSuspensionConnectionPoints = new Vector3[4];

        
        
    }

    void FixedUpdate()
    {
        /*
        wheelSuspensionConnectionPoints[0] = transform.TransformPoint(boxCollider.center + new Vector3(-boxCollider.size.x, -boxCollider.size.y, -boxCollider.size.z) * 0.5f); // Back left
        wheelSuspensionConnectionPoints[1] = transform.TransformPoint(boxCollider.center + new Vector3(boxCollider.size.x, -boxCollider.size.y, -boxCollider.size.z) * 0.5f); // Back right
        wheelSuspensionConnectionPoints[2] = transform.TransformPoint(boxCollider.center + new Vector3(boxCollider.size.x, -boxCollider.size.y, boxCollider.size.z) * 0.5f); // Front right
        wheelSuspensionConnectionPoints[3] = transform.TransformPoint(boxCollider.center + new Vector3(-boxCollider.size.x, -boxCollider.size.y, boxCollider.size.z) * 0.5f); // Front left
        for (int i = 0; i < wheelSuspensionConnectionPoints.Length; i++)
        {
            RaycastHit hitInfo = new RaycastHit();
            Physics.Raycast(transform.position + wheelSuspensionConnectionPoints[i], -transform.up, out hitInfo, suspensionMaxLength);
            Debug.DrawLine(transform.position + wheelSuspensionConnectionPoints[i], transform.position + wheelSuspensionConnectionPoints[i] - transform.up * suspensionMaxLength, Color.red);

            float compressionRatio = 1 - (hitInfo.distance / suspensionMaxLength);
            rb.AddForceAtPosition(suspensionMaxForce * compressionRatio * transform.up, transform.position + wheelSuspensionConnectionPoints[i]);
        }*/
        
        Vector3 size = boxCollider.size;
        Vector3 center = new Vector3(boxCollider.center.x, boxCollider.center.y, boxCollider.center.z);
        Vector3[] corners = new Vector3[4];

        corners[0] = new Vector3(center.x + size.x / 2, center.y, center.z + size.z / 2);
        corners[1] = new Vector3(center.x - size.x / 2, center.y, center.z - size.z / 2);
        corners[2] = new Vector3(center.x + size.x / 2, center.y, center.z - size.z / 2);
        corners[3] = new Vector3(center.x - size.x / 2, center.y, center.z + size.z / 2);

        //float rayLength = size.y / 2 + 0.05f;
        /*
        Debug.DrawRay(transform.TransformPoint(corners[0]), -transform.up * rayLength, Color.red);
        Debug.DrawRay(transform.TransformPoint(corners[1]), -transform.up * rayLength, Color.red);
        Debug.DrawRay(transform.TransformPoint(corners[2]), -transform.up * rayLength, Color.red);
        Debug.DrawRay(transform.TransformPoint(corners[3]), -transform.up * rayLength, Color.red);
        */

        
        for (int i = 0; i < wheelSuspensionConnectionPoints.Length; i++)
        {
            RaycastHit hitInfo = new RaycastHit();
            Physics.Raycast(transform.TransformPoint(corners[i]), -transform.up, out hitInfo, suspensionMaxLength);
            //Debug.DrawRay(transform.TransformPoint(corners[i]), -transform.up, Color.green, suspensionMaxLength);
            
            //Debug.DrawLine(transform.position + wheelSuspensionConnectionPoints[i], transform.position + wheelSuspensionConnectionPoints[i] - transform.up * suspensionMaxLength, Color.red);
            float compressionRatio = 1 - (hitInfo.distance / suspensionMaxLength);
            if (hitInfo.collider == null)
            {
                compressionRatio = 0;
            }
            rb.AddForceAtPosition(suspensionMaxForce * compressionRatio * transform.up, transform.TransformPoint(corners[i]));
            Debug.Log(hitInfo.distance + " | " + suspensionMaxForce + " | " + compressionRatio + " | " + transform.up);

            float rayLength = hitInfo.distance;
            Debug.DrawRay(transform.TransformPoint(corners[i]), -transform.up * rayLength, Color.red);

            Debug.DrawLine(transform.TransformPoint(corners[i]), hitInfo.point, Color.blue);
        }
    }
}
