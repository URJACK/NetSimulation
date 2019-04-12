public class CodeRecorder
{
    private static CodeRecorder instance;
    public static CodeRecorder GetInstance()
    {
        if (instance == null)
        {
            instance = new CodeRecorder();
        }
        return instance;
    }
    private CodeRecorder()
    {

    }
}