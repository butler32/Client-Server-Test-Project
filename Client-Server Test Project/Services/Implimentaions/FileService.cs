using Client_Server_Test_Project.Models;
using Client_Server_Test_Project.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Client_Server_Test_Project.Services.Implimentaions
{
    public class FileService : IFileService
    {
        public List<Card> Open(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                throw new NotImplementedException();
            }
        }
    }
}
