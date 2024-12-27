//using Microsoft.Extensions.Logging;
using Serilog;

namespace LogPOC
{ 

    class Program
    {
        //static ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Trace));
        //static ILogger logger = factory.CreateLogger("Program");
        static ILogger loggerSerilog = new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.Console().WriteTo.File("C:\\Users\\Work\\Desktop\\Internship\\Interns-BE\\Harsha\\Logging\\log.txt").CreateLogger();

        static void Main(string[] args)
        {

            //logger.LogDebug("In main method");
            loggerSerilog.Debug("In Main method");

            var list = new List<string>();
            //logger.LogTrace("list is created");
            loggerSerilog.Verbose("list is created");
            //logger.LogWarning("list is empty");
            loggerSerilog.Warning("List is empty");
            try
            {
                //logger.LogTrace("trying to print to first element of list");
                loggerSerilog.Verbose("trying to print to first element of list");
                Console.WriteLine(list[0]);
                
            }
            catch(Exception ex)
            {
                if (list.Count == 0)
                {
                    //logger.LogError("tried to access element without checking does it contain element");
                    loggerSerilog.Error("tried to access element without checking does it contain element");
                }
                //logger.LogCritical(ex.ToString());
                loggerSerilog.Fatal(ex.ToString()); 
            }
            //logger.LogInformation("Hello world! Logging is {Description}", "fun");
            loggerSerilog.Information("Hello world! Logging is {Description}", "fun");
        }
    }
}