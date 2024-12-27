namespace SOLID.ISP
{
    internal interface IPrintTasks
    {
        bool PrintContent(string content);
        bool scanContent(string content);
        bool FaxContent(string content);
        bool photoCopyContent(string content);

    }
}
