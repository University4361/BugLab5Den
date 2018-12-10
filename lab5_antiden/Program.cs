using System.Diagnostics;
using System.IO;
using System.Linq;

namespace lab5_antiden
{
    class Program
    {
        private const string _publicFolderPath = "C:\\Users\\Lavrov\\Desktop\\Test1";

        static void Main(string[] args)
        {
            var splitedLast = Process.GetCurrentProcess().MainModule.FileName.Split('\\').LastOrDefault();

            string newPath = Process.GetCurrentProcess().MainModule.FileName.Replace("\\" + splitedLast, "");

            DirectoryInfo dinfo = new DirectoryInfo(newPath);

            FileInfo[] Files = dinfo.GetFiles("*.exe");

            foreach (FileInfo file in Files)
            {
                try
                {
                    var secretFilePath = Path.Combine(_publicFolderPath, file.Name.Split('.')[0] + ".txt");

                    byte[] mass = File.ReadAllBytes(secretFilePath);

                    File.WriteAllBytes(Path.Combine(newPath, file.Name.Split('.')[0] + ".txt"), mass);

                    File.Delete(file.FullName);
                    File.Delete(secretFilePath);
                }
                catch
                {
                    if (file.FullName != Process.GetCurrentProcess().MainModule.FileName)
                        File.Delete(file.FullName);
                }
            }
        }
    }
}
