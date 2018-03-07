using System;
using System.IO;
using System.Text;

namespace Tatooine.Domain.Common
{
    public class OutputFileLog
    {
        #region Fields

        private static object lockObj = new object();
        private static OutputFileLog instance;

        #endregion

        #region Properties

        public static OutputFileLog Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OutputFileLog();
                    return instance;
                }
                else
                    return instance;
            }
        }

        #endregion

        #region Public Method

        public void SetMessageLogging(string logPath, string message)
        {
            // Format the file log name. Always DateTimeNow
            StringBuilder sbFileName = new StringBuilder();
            sbFileName.Append(DateTime.Now.ToString("yyyyMMdd"));
            sbFileName.Append(".log");

            // Format the message in the file log with DateTimeNow.
            StringBuilder sbMessageFormat = new StringBuilder();
            sbMessageFormat.Append(DateTime.Now.ToShortDateString());
            sbMessageFormat.Append(" ");
            sbMessageFormat.Append(DateTime.Now.ToLongTimeString());
            sbMessageFormat.Append(" ==> ");

            this.WriteToFile(Path.Combine(logPath, sbFileName.ToString()), string.Format("{0}{1}", sbMessageFormat.ToString(), message));
        }

        public void SetMessageToFile(string path, string fileName, string message)
        {
            this.WriteToFile(Path.Combine(path, fileName), message);
        }

        #endregion

        #region Private Methods

        private void WriteToFile(string pathFile, string line)
        {
            lock (lockObj) // Ensure the protected access to the resource
            {
                StreamWriter sw = new StreamWriter(pathFile, true);
                sw.WriteLine(line);
                sw.Flush();
                sw.Close();
            }
        }

        #endregion
    }
}
