/*接受报文，并转化为相应对象的类*/
public class MailHanlder
{
    private static MailHanlder instance;
    public static MailHanlder GetInstance()
    {
        if(instance == null)
        {
            instance = new MailHanlder();
        }
        return instance;
    }

    private MailHanlder() {
        nodeLayerHanlder = NodeLayerHanlder.GetInstance();
    }
    private NodeLayerHanlder nodeLayerHanlder;

    public bool Handle(string str) {
        JSONObject jb = new JSONObject(str);
        if (jb["type"].str == "node") {
            return nodeLayerHanlder.Handle(jb); 
        }
        else
        {
            return false;
        }
    }
}

