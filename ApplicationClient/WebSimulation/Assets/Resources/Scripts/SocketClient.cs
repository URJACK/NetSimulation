using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

class SocketClient
{
    private UdpClient udpClient;
    private const int localPort = 6602;
    
    private string remoteIP = "127.0.0.1";
    private int remotePort = 6667;

    public SocketClient()
    {
        udpClient = new UdpClient(localPort);
    }

    /*
    void OnReceive(IAsyncResult result)
    {
        try
        {
            byte[] buffer = udpClient.EndReceive(result, ref remotePoint);
            OnMessage(buffer);

            lock (udpClient)
            {
                udpClient.BeginReceive(OnReceive, null);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    void OnMessage(byte[] buffer)
    {
        string message = Encoding.Default.GetString(buffer);
        Debug.Log(message);
    }
    */

    public void Send(string message)
    {
        udpClient.Connect(IPAddress.Parse(remoteIP), remotePort);
        //udpClient.BeginReceive(OnReceive, null);

        byte[] sendData = Encoding.Default.GetBytes(message);
        udpClient.Send(sendData, sendData.Length);
        udpClient.Close();
    }
}