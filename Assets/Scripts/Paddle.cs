using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Paddle : NetworkBehaviour
{
    public PlayableArea playableArea;
    public float speed = 8.0f;
    public Material localPlayerMaterial;
    public Material otherPlayerMaterial;

    public bool movingRight = false;
    public bool movingLeft = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void OnStartAuthority()
    {
        MeshRenderer leftRenderer = transform.Find("Model").Find("LeftBox").GetComponent<MeshRenderer>();
        MeshRenderer rightRenderer = transform.Find("Model").Find("RightBox").GetComponent<MeshRenderer>();

        if (hasAuthority)
        {
            leftRenderer.material = localPlayerMaterial;
            rightRenderer.material = localPlayerMaterial;
        }
        else
        {
            leftRenderer.material = otherPlayerMaterial;
            rightRenderer.material = otherPlayerMaterial;
        }

        playableArea = GameObject.Find("PlayableArea").GetComponent<PlayableArea>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority)
        {
            return;
        }

        Vector3 xVector = playableArea.coordinateSystem.right;
        Vector3 position = playableArea.coordinateSystem.InverseTransformPoint(transform.position);
        
        if (Input.GetKey(KeyCode.A) && position.x > playableArea.leftPaddleBoundary)
        {
            transform.position -= xVector * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) && position.x < playableArea.rightPaddleBoundary)
        {
            transform.position += xVector * speed * Time.deltaTime;
        }
    }

    public void Move(float amount)
    {
        if (!hasAuthority)
        {
            return;
        }

        Vector3 xVector = playableArea.coordinateSystem.right;
        Vector3 position = playableArea.coordinateSystem.InverseTransformPoint(transform.position);
        if (amount < 0.0f && position.x > playableArea.leftPaddleBoundary ||
            amount > 0.0f && position.x < playableArea.rightPaddleBoundary)
        {
            transform.position += amount * xVector * speed * Time.deltaTime;
        }
    }
}
