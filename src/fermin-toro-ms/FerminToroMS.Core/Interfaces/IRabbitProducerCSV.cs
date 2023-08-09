using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Core.Interfaces
{
    public interface IRabbitProducerCSV
    {
        public void SendProductMessage<T>(T message);
    }
}
