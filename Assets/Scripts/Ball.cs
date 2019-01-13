using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ball : NetworkBehaviour
{
    [SyncVar]
    private Vector3 angularVelocity;

    public PlayableArea playableArea;

    [SyncVar]
    public Vector3 velocity;

    [SyncVar]
    public float speed = 10.0f;

    [SyncVar]
    public float maxAngularVelocity = 250.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (!isServer)
        {
            return;
        }

        velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
        {
            return;
        }
        
        transform.position += velocity * Time.deltaTime;
        transform.Rotate(angularVelocity.normalized, angularVelocity.magnitude * maxAngularVelocity * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isServer)
        {
            return;
        }

        //=====
        // Handle regular ball collisions
        angularVelocity = new Vector3(Random.value, Random.value, Random.value);
        Vector3 impactNormal = collision.contacts[0].normal;
        velocity = Vector3.Reflect(velocity, impactNormal);

        // Remove up/down component
        Vector3 zAxis = playableArea.coordinateSystem.up;

        float length = velocity.magnitude;
        Vector3 upDown = Vector3.Dot(zAxis, velocity) * zAxis;
        velocity = Vector3.Normalize(velocity - upDown) * length;
        
        //=====
        // Reflected velocity vector should deviate towards the sides of the paddle
        Vector3 xAxis = playableArea.coordinateSystem.right;
        Vector3 yAxis = playableArea.coordinateSystem.forward;
        if (collision.gameObject.tag == "Paddle")
        {
            Vector3 direction = velocity.normalized;
            length = velocity.magnitude;

            float amount = Vector3.Dot(transform.position - collision.gameObject.transform.position, xAxis);
            direction = Vector3.Normalize(direction + xAxis * amount);

            velocity = direction * length;
        }

        //=====
        // Prevent perfectly horizontal or vertical bouncing
        float dot = Vector3.Dot(velocity.normalized, xAxis);
        if (Mathf.Abs(dot) > 0.9)
        {
            Vector3 direction = velocity.normalized;
            length = velocity.magnitude;

            Vector3 deviationVector = yAxis;
            if (Random.Range(-1.0f, 1.0f) > 0)
            {
                deviationVector *= -1.0f;
            }
            velocity = (direction + deviationVector).normalized * length;
        }

        dot = Vector3.Dot(velocity.normalized, yAxis);
        if (Mathf.Abs(dot) > 0.9)
        {
            Vector3 direction = velocity.normalized;
            length = velocity.magnitude;

            Vector3 deviationVector = xAxis;
            if (Random.Range(-1.0f, 1.0f) > 0)
            {
                deviationVector *= -1.0f;
            }
            velocity = (direction + deviationVector).normalized * length;
        }

        //=====
        // Ensure that velocity magnitude stays constant
        velocity = velocity.normalized * speed;
    }
}
