using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkScript : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("AAAA");
        if (!isServer)
            return;

        Debug.Log("Hahah");
        transform.position += Vector3.forward * Time.deltaTime * 10.0f;
    }
}
