using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL
{
    public class FileNPath
    {
        public void CreatePath(string subPath)
        {
            if (false == System.IO.Directory.Exists(subPath))
            {
                //创建pic文件夹
                System.IO.Directory.CreateDirectory(subPath);
            }
        }
        
        public bool FileExisted(string subPath)
        {
            if (false == System.IO.File.Exists(subPath))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
