using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Interfaces
{
    public interface ICalculations
    {
        List<string> Register { get; set; }

        double Add(double x, double y);
    }
}
