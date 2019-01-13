using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetClient : NetworkDiscovery
{
    public List<string> gamesFound;
    public GameObject gamesFoundListUI;
    public GameObject foundGameButtonPrefab;

    public void StartClient()
    {
        Initialize();
        StartAsClient();
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        base.OnReceivedBroadcast(fromAddress, data);

        var items = data.Split(':');
        if (items.Length == 3 && items[0] == "NetworkManager")
        {
            if (NetworkManager.singleton != null && NetworkManager.singleton.client == null)
            {
                if (!gamesFound.Exists(str => str == data))
                {
                    gamesFound.Add(data);
                    GameObject game = Instantiate(foundGameButtonPrefab, gamesFoundListUI.transform);
                    game.GetComponentInChildren<Text>().text = fromAddress;
                    game.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        Debug.Log("KLIKNIĘTE AAAAAAAAAAAAAAAAA");
                        NetworkManager.singleton.networkAddress = fromAddress; //items[1];
                        NetworkManager.singleton.networkPort = Convert.ToInt32(items[2]);
                        NetworkManager.singleton.StartClient();
                    });
                }
            }
        }

        Debug.Log("Client discovery received broadcast " + data + " from " + fromAddress);
        Debug.Log(data);
        Debug.Log(fromAddress);
    }
}
