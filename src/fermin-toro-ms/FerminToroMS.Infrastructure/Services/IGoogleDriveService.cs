using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Infrastructure.Services
{
    public interface IGoogleDriveService
    {
        byte[] DownloadFile(string fileId);
    }
}
