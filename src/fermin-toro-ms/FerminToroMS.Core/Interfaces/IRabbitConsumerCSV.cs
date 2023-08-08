using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Interfaces
{
    public interface IRabbitConsumerCSV
    {
        void StartConsuming();
        void StopConsuming();
    }
}
