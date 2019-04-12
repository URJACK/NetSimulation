using UnityEngine;
public class DeviceLayerHanlder
{
    private static DeviceLayerHanlder instance;
    public static DeviceLayerHanlder GetInstance()
    {
        if (instance == null)
        {
            instance = new DeviceLayerHanlder();
        }
        return instance;
    }
    private DeviceLayerHanlder() { }

    public bool Handle(JSONObject jb)
    {
        string name = jb["name"].str;
        if (name == "imuphyportbroadcast")
        {
            return ImuPhyPortBroadCast(jb["code"].n, jb["id"].str);
        }
        else if (name == "dreadybroadcast") {
            return DReadyBroadcast(jb["code"].n, jb["id"].str);
        }
        else
        {
            return false;
        }
    }

    private bool ImuPhyPortBroadCast(float code,string id)
    {
        Debug.Log("ImuPhyPortBroadCast" + code);
        return false;
    }

    private bool DReadyBroadcast(float code, string id)
    {
        Debug.Log("DReadyBroadcast" + code);
        return false;
    }
}