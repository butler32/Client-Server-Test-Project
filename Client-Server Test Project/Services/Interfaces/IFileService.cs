using Client_Server_Test_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Server_Test_Project.Services.Interfaces
{
    public interface IFileService
    {
        List<Card> Open(string filename);
    }
}
