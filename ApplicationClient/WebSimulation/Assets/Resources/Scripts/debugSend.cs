using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugSend : MonoBehaviour
{
    MailSender ms = MailSender.GetInstance();
    public void Interfacechecksend(int type)
    {
        SocketClient sc = new SocketClient();
        CodeRecorder cr = CodeRecorder.GetInstance();
        JSONObject jb;
        ms.SetId("127.0.0.1");
        ms.SetToken("ADFB12GA");

        if (type == 1)
        {
            jb = ms.Regist("fufangzhou");
            sc.Send(jb.ToString());
            jb = ms.Info();
            sc.Send(jb.ToString());
            jb = ms.Quit();
            sc.Send(jb.ToString());
            jb = ms.Modify();
            sc.Send(jb.ToString());
            jb = ms.NReady();
            sc.Send(jb.ToString());
            jb = ms.IpBroadcastResp(cr.SUCCESS);
            sc.Send(jb.ToString());
            jb = ms.NodeBroadcastResp(cr.IPERR);
            sc.Send(jb.ToString());
            jb = ms.NReadyBroadcastResp(cr.LINEERR);
            sc.Send(jb.ToString());
            sc.Close();
        }
        else if (type == 2)
        {
            jb = ms.SetImuPhyPorts(4);
            sc.Send(jb.ToString());
            jb = ms.ImuPhyPortBroadcastResp(cr.SUCCESS);
            sc.Send(jb.ToString());
            jb = ms.DReady();
            sc.Send(jb.ToString());
            jb = ms.DReadyBroadcastResp(cr.NAMEERR);
            sc.Send(jb.ToString());
            sc.Close();
        }else if(type == 3)
        {
            jb = ms.Link("12.3.25.6", "120.23.46.12", 1, 3);
            sc.Send(jb.ToString());
            jb = ms.LinkBroadCastResp(cr.SUCCESS);
            sc.Send(jb.ToString());
            jb = ms.UnLink("25.23.56.43", "235.23.123.86", 2, 4);
            sc.Send(jb.ToString());
            jb = ms.UnLinkBroadCastResp(cr.IPERR);
            sc.Send(jb.ToString());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
}
