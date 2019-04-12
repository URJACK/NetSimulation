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
    private CodeRecorder() { }
    public readonly int SUCCESS = 200;
    public readonly int IPERR = 301;
    public readonly int TOKENERR = 302;
    public readonly int NAMEERR = 303;
    public readonly int REPEATLANDING = 304;
    public readonly int IMUPHYPORTOCCUPIED = 305;
    public readonly int DEVICENOTEXIST = 306;
    public readonly int LINEERR = 307;
    public readonly int IMUPHYPORTNUMERR = 308;
}