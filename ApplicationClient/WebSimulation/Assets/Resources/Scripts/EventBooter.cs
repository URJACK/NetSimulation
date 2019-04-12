using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventBooter : MonoBehaviour
{
    private readonly MailPoster mp = MailPoster.GetInstance();
    // Start is called before the first frame update
    void Start()
    {
        SocketClient sc = new SocketClient();
        mp.SetId("127.0.0.1");
        mp.SetToken("ADFB12GA");
        JSONObject jb = mp.Regist("fufangzhou");
        jb = mp.Link("ADFA", "BDFG", 12, 20);
        sc.Send(jb.ToString());
    }
}
