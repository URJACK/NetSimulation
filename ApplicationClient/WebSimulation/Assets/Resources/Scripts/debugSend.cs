using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugSend : MonoBehaviour
{
    public void send(string message)
    {
        SocketClient sc = new SocketClient();
        Debug.Log(message);
        
        sc.Send(message);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
}
