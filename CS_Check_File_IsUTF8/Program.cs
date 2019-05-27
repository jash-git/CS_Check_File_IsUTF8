using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Check_File_IsUTF8
{
    //https://blog.xuite.net/jaofeng.chen/DesignShow/4702957-%E7%94%A8%E7%A8%8B%E5%BC%8F%E5%88%A4%E6%96%B7%E6%96%87%E5%AD%97%E6%AA%94%E7%9A%84%E6%AA%94%E6%A1%88%E7%B7%A8%E7%A2%BCEncoding
    //https://docs.microsoft.com/zh-tw/dotnet/api/system.text.encoding.getencoding?view=netframework-4.8
    class Program
    {
        static bool IsUTF8File (String StrName)
        {
            bool blnAns = false;

            Stream reader = File.Open(StrName, FileMode.Open, FileAccess.Read);
            Encoding encoder = null;
            byte[] header = new byte[4];
            // 讀取前四個Byte
            reader.Read(header, 0, 4);
            if (header[0] == 0xFF && header[1] == 0xFE)
            {
                // UniCode File
                reader.Position = 2;
                encoder = Encoding.Unicode;
            }
            else if (header[0] == 0xEF && header[1] == 0xBB && header[2] == 0xBF)
            {
                // UTF-8 File
                reader.Position = 3;
                encoder = Encoding.UTF8;
                blnAns = true;
            }
            else
            {
                // Default Encoding File
                reader.Position = 0;
                encoder = Encoding.Default;
            }

            reader.Close();
            // .......... 接下來的程式

            return blnAns;
        }

        static void pause()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }

        static void Main(string[] args)
        {
            bool bln01 = IsUTF8File ("test01.sycsv");
            bool bln02 = IsUTF8File ("test02.sycsv");
            Console.WriteLine("test01.sycsv IS UTF8 : {0}", bln01);
            Console.WriteLine("test02.sycsv IS UTF8 : {0}", bln02);
            pause();
        }
    }
}
