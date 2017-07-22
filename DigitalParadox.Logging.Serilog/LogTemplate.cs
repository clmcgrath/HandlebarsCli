using System.Text;

namespace DigitalParadox.Logging.Serilogger
{

        public class LogTemplate : ILogTemplate
        {
            public bool EnableTimeStamp { get; set; } = true;

            public string TimeStampTemplate { get; set; } = "{Timestamp:yyyy-MM-dd HH:mm:ss}";
            public string LevelTemplate { get; set; } = " { Level: u} ";
            public string MessageTemplate { get; set; } = " { Message } ";

            public string ExceptionTemplate { get; set; } = " { NewLine } { Exception } ";


            public string Template  => RenderTemplate();


            protected virtual string  RenderTemplate()
            {
                var str = new StringBuilder();
                if (EnableTimeStamp)
                {
                    str.Append(TimeStampTemplate);
                }

                str.Append(LevelTemplate);
                str.Append(MessageTemplate);
                str.Append(ExceptionTemplate);
                return str.ToString();
            }
        
    }
}
