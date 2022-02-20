using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charts.Services
{
    public interface IChartService
    {
        MemoryStream CreateWaveform(double[,] result);
    }
}
