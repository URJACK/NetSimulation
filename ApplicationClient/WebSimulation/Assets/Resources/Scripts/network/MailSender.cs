public class MailSender
{
    private string id;
    private string token;
    private static MailSender instance;
    private MailSender(){}

    /* Private Package */
    private void NodeLayer(string name, out JSONObject jb)
    {
        jb = new JSONObject();
        jb.AddField("type", "node");
        jb.AddField("name", name);
    }
    private void DeviceLayer(JSONObject njb, string name,out JSONObject djb)
    {
        djb = new JSONObject();
        djb.AddField("type", "device");
        djb.AddField("name", name);
        njb.AddField("id", id);
        njb.AddField("token", token);
        njb.AddField("updata", djb);
    }
    private void RunLayer(JSONObject njb, string name, out JSONObject rjb)
    {
        rjb = new JSONObject();
        rjb.AddField("type", "run");
        rjb.AddField("name", name);
        njb.AddField("id", id);
        njb.AddField("token", token);
        njb.AddField("updata", rjb);
    }
    
    /* Layer:node */
    public JSONObject Regist(string id)
    {
        JSONObject jb;
        NodeLayer("regist", out jb);
        jb.AddField("id", id);
        return jb;
    }
    public JSONObject Info()
    {
        JSONObject jb;
        NodeLayer("info", out jb);
        jb.AddField("token", token);
        jb.AddField("id", id);
        return jb;
    }
    public JSONObject Quit()
    {
        JSONObject jb;
        NodeLayer("quit", out jb);
        jb.AddField("token", token);
        jb.AddField("id", id);
        return jb;
    }
    public JSONObject Modify()
    {
        JSONObject jb;
        NodeLayer("modify", out jb);
        jb.AddField("token", token);
        jb.AddField("id", id);
        return jb;
    }
    public JSONObject NReady()
    {
        JSONObject jb;
        NodeLayer("nready", out jb);
        jb.AddField("token", token);
        jb.AddField("id", id);
        return jb;
    }
    public JSONObject IpBroadcastResp(int code)
    {
        JSONObject jb;
        NodeLayer("ipbroadcastresp", out jb);
        jb.AddField("code",code);
        jb.AddField("id", id);
        return jb;
    }
    public JSONObject NodeBroadcastResp(int code)
    {
        JSONObject jb;
        NodeLayer("nodebroadcastresp", out jb);
        jb.AddField("code", code);
        jb.AddField("id", id);
        return jb;
    }
    public JSONObject NReadyBroadcastResp(int code)
    {
        JSONObject jb;
        NodeLayer("nreadybroadcastresp", out jb);
        jb.AddField("code", code);
        jb.AddField("id", id);
        return jb;
    }

    /* Layer:device */
    public JSONObject SetImuPhyPorts(float portsize) {
        JSONObject njb;
        NodeLayer("cdata",out njb);
        DeviceLayer(njb, "setimuphyports", out JSONObject djb);
        djb.AddField("portsize", portsize);
        return njb;
    }
    public JSONObject ImuPhyPortBroadcastResp(int code)
    {
        JSONObject njb;
        NodeLayer("cdata", out njb);
        DeviceLayer(njb, "imuphyportbroadcastresp", out JSONObject djb);
        djb.AddField("code", code);
        return njb;
    }
    public JSONObject DReady()
    {
        JSONObject njb;
        NodeLayer("cdata", out njb);
        DeviceLayer(njb, "dready", out JSONObject djb);
        return njb;
    }
    public JSONObject DReadyBroadcastResp(int code)
    {
        JSONObject njb;
        NodeLayer("cdata", out njb);
        DeviceLayer(njb, "dreadybroadcastresp", out JSONObject djb);
        djb.AddField("code", code);
        return njb;
    }

    /* Layer:run */
    public JSONObject Link(string idA,string idB,float imuphyportA,float imuphyportB)
    {
        JSONObject njb;
        NodeLayer("cdata", out njb);
        RunLayer(njb, "link", out JSONObject rjb);
        rjb.AddField("idA", idA);
        rjb.AddField("idB", idB);
        rjb.AddField("imuphyportA", imuphyportA);
        rjb.AddField("imuphyportB", imuphyportB);
        return njb;
    }
    public JSONObject LinkBroadCastResp(int code)
    {
        JSONObject njb;
        NodeLayer("cdata", out njb);
        RunLayer(njb, "linkbroadcastresp", out JSONObject rjb);
        rjb.AddField("code", code);
        return njb;
    }
    public JSONObject UnLink(string idA, string idB, float imuphyportA, float imuphyportB)
    {
        JSONObject njb;
        NodeLayer("cdata", out njb);
        RunLayer(njb, "unlink", out JSONObject rjb);
        rjb.AddField("idA", idA);
        rjb.AddField("idB", idB);
        rjb.AddField("imuphyportA", imuphyportA);
        rjb.AddField("imuphyportB", imuphyportB);
        return njb;
    }
    public JSONObject UnLinkBroadCastResp(int code)
    {
        JSONObject njb;
        NodeLayer("cdata", out njb);
        RunLayer(njb, "unlinkbroadcastresp", out JSONObject rjb);
        rjb.AddField("code", code);
        return njb;
    }

    /* Setting Interface */
    public static MailSender GetInstance()
    {
        if(instance == null)
        {
            instance = new MailSender();
        }
        return instance;
    }
    public void SetId(string id)
    {
        this.id = id;
    }
    public void SetToken(string token)
    {
        this.token = token;
    }
}