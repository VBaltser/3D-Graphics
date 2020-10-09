using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_Graph
{
    public class LabBuilder
    {
        Lab lab = new Lab();

        public Lab Build()
        {
            return lab;
        }

        public LabBuilder SetWindowSize(int w, int h)
        {
            lab.Width = w;
            lab.Height = h;

            return this;
        }
    }
}
