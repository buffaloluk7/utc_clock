using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTCClock.Business.Interfaces
{
    public interface IObserver
    {
        void Update(IObservable subject);
    }
}
