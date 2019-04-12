using UnityEngine;
public class RunLayerHanlder
{
    private static RunLayerHanlder instance;
    public static RunLayerHanlder GetInstance()
    {
        if (instance == null)
        {
            instance = new RunLayerHanlder();
        }
        return instance;
    }
    private RunLayerHanlder() { }
    public bool Handle(JSONObject jb)
    {
        string name = jb["name"].str;
        if(name == "linkbroadcast")
        {
            return LinkBroadcast(jb["code"].n);
        }
        else if(name == "unlinkbroadcast")
        {
            return UnLinkBroadcast(jb["code"].n);
        }
        else
        {
            return false;
        }
    }

    private bool LinkBroadcast(float code)
    {
        Debug.Log("LinkBroadcast" + code);
        return false;
    }

    private bool UnLinkBroadcast(float code)
    {
        Debug.Log("UnLinkBroadcast" + code);
        return false;
    }
}
