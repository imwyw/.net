using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Observer
{
    public interface IBuyObserver
    {
        void Update(Tickets ticket, int count);
    }
}
