using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float amplitude = 2.0f;
    public float speed = 3.0f;
    private float offset;

    private float internalTime = 0.0f;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
        offset = Random.value * 2.0f * Mathf.PI;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = initialPosition + Mathf.Sin(internalTime * speed + offset) * amplitude * transform.up;

        internalTime += Time.deltaTime;
        if (internalTime * speed > 2.0f * Mathf.PI)
        {
            internalTime = 0.0f;
        }
    }
}
