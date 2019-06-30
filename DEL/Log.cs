using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL
{
    public class Log
    {
        public void WriteLog(string path, string info)        {
            using (var stream = new StreamWriter(System.IO.Path.Combine(path, $"Data_{DateTime.Now:yyyyMMdd}.txt"), true))
            {
                stream.WriteLine("{0:yyyy-MM-dd HH:mm:ss}, __Info__{1}", DateTime.Now, info);
            }
        }
    }
}
