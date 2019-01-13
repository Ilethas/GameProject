using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MainMenuGameMode : MonoBehaviour
{
    public NetServer server;
    public NetClient client;

    public GameObject gamesFoundListUI;
    public GameObject foundGameButtonPrefab;
    public GameObject mainUI;
    public GameObject hostUI;
    public GameObject joinUI;

    // Start is called before the first frame update
    void Start()
    {
        server.onHostServerFailed = OnHostFailed;
        client.gamesFoundListUI = gamesFoundListUI;
        client.foundGameButtonPrefab = foundGameButtonPrefab;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HostGame()
    {
        hostUI.SetActive(true);
        mainUI.SetActive(false);
        joinUI.SetActive(false);

        server.StartServer();
    }

    public void JoinGame()
    {
        joinUI.SetActive(true);
        mainUI.SetActive(false);
        hostUI.SetActive(false);
        
        client.StartClient();
        client.gamesFound.Clear();
        for (int i = 0; i < gamesFoundListUI.transform.childCount; i++)
        {
            Destroy(gamesFoundListUI.transform.GetChild(i).gameObject);
        }
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Back()
    {
        mainUI.SetActive(true);
        joinUI.SetActive(false);
        hostUI.SetActive(false);

        if (client.isClient)
        {
            client.StopBroadcast();
        }
        if (server.isServer)
        {
            server.StopBroadcast();
        }
    }

    public void StartGame()
    {
        if (NetworkManager.singleton != null)
        {
            NetworkManager.singleton.StartHost();
        }
    }

    public void OnHostFailed()
    {
        Back();
    }
}
