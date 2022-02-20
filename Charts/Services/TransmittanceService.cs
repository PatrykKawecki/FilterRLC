using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Charts.Services
{
    public class TransmittanceService
    {
        public double[,] GetTransmittance(Models.FilterModel model)
        {
            double u1 = model.U1;
            double R1 = model.Resistance1;
            double R2 = model.Resistance2;
            double L1 = model.Inductance;
            double C1 = model.Capacitance;
            double fmin = model.Fmin;
            double fmax = model.Fmax;
            int size = model.NumOfRows;

            double[,] Results = new double[size, 4];
            Complex Z1;
            Complex Z2;
            Complex Z3;
            Complex I1;
            Complex T;
            double f = fmin;
            double df = (fmax - fmin) / (size - 1);
            double omega = 0;
            for (int i = 0; i < size; i++)
            {
                omega = 2 * Math.PI * f;
                Z1 = new Complex(R1 * R2, 0);
                Z2 = new Complex(R1 * R2 + L1 / C1, omega * (L1 * R2 + L1 * R1 - (R1 / (omega * omega * C1))));
                T = Z1 / Z2;
                Z3 = new Complex(u1 * (R1 + R2), (-u1) / (omega * C1));
                I1 = Z3 / Z2;
                Results[i, 0] = f;
                Results[i, 1] = T.Magnitude;
                Results[i, 2] = T.Phase;
                Results[i, 3] = Math.Sqrt(I1.Real * I1.Real + I1.Imaginary * I1.Imaginary); //I1.Real
                f += df;
            }
            return Results;
        }
    }
}
