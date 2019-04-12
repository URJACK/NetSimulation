using UnityEngine;

public class NodeLayerHanlder
{
    
    private static NodeLayerHanlder instance;
    public static NodeLayerHanlder GetInstance()
    {
        if (instance == null)
        {
            instance = new NodeLayerHanlder();
        }
        return instance;
    }
    private NodeLayerHanlder() {
        deviceLayerHanlder = DeviceLayerHanlder.GetInstance();
        runLayerHanlder = RunLayerHanlder.GetInstance();
    }
    private DeviceLayerHanlder deviceLayerHanlder;
    private RunLayerHanlder runLayerHanlder;

    public bool Handle(JSONObject jb)
    {
        string name = jb["name"].str;
        if (name == "ipbroadcast")
        {
            return IpBroadcast(jb["id"].str, jb["ip"].str);
        }
        else if (name == "nodebroadcast")
        {
            return NodeBroadcast(jb["id"].str, jb["ip"].str);
        }
        else if (name == "registresp")
        {
            return RegistResp(jb["code"].n, jb["token"].str);
        }
        else if (name == "inforesp")
        {
            return InfoResp(jb["code"].n, jb["info"].str);
        }
        else if (name == "nreadybroadcast")
        {
            return NReadyBroadcast(jb["code"].n, jb["id"].str);
        }
        else if (name == "sdata")
        {
            return SData(jb["updata"]);
        }
        else
        {
            return false;
        }
    }
    private bool IpBroadcast(string id, string ip)
    {
        Debug.Log("ipBroadcast " + id);
        return true;
    }
    private bool NodeBroadcast(string id, string ip)
    {
        Debug.Log("NodeBroadcast " + id);
        return true;
    }
    private bool RegistResp(float code, string token)
    {
        Debug.Log("RegistResp " + code);
        return true;
    }
    private bool InfoResp(float code, string info)
    {
        Debug.Log("InfoResp " + code);
        return true;
    }
    private bool NReadyBroadcast(float code, string id)
    {
        Debug.Log("NReadyBroadcast " + code);
        return true;
    }
    private bool SData(JSONObject deviceData)
    {
        if (deviceData["type"].str == "device")
        {
            return deviceLayerHanlder.Handle(deviceData);
        }
        else if (deviceData["type"].str == "run")
        {
            return runLayerHanlder.Handle(deviceData);
        }
        else
        {
            return false;
        }
    }
}
