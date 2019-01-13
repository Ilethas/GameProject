using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public GameObject BallPrefab;
    public GameObject PaddlePrefab;

    public PlayableArea playableArea;
    public Paddle paddle;

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            Transform spawn = GameInstance.singleton.paddleSpawn.transform;
            Vector3 position = spawn.position + GameInstance.singleton.totalPlayerPaddleOffset * spawn.forward;
            GameObject obj = (GameObject)Instantiate(PaddlePrefab, position, spawn.rotation);
            NetworkServer.SpawnWithClientAuthority(obj, gameObject);

            GameInstance.singleton.totalPlayerPaddleOffset += GameInstance.singleton.paddleOffset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
