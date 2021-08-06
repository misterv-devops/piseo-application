using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLSpectrometer
{
    public enum Interface
    {
        InterfaceISA = 0,
        InterfacePCI = 1,
        InterfaceTest = 3,
        InterfaceUSB = 5,
        InterfacePCIe = 10,
        InterfaceEthernet = 11
    }
}
