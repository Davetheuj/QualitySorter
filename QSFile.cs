using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualitySorter
{
    class QSFile
    {public
           string name;
        public int sizeDurationRatio;

        public QSFile(string name, double duration, long size)
        {
            this.name = name;
            this.sizeDurationRatio = (int)((size / duration)/100000);
        }

    }
}
