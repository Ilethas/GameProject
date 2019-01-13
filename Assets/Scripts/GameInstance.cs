using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameInstance : NetworkBehaviour
{
    public static GameInstance singleton;

    public PlayableArea playableArea;
    public Transform paddleSpawn;
    public float paddleOffset = 2.0f;

    [SyncVar]
    public float totalPlayerPaddleOffset = 0.0f;

    GameInstance()
    {
        singleton = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
