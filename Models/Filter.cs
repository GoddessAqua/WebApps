using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskForIronWaterStudio.Models
{
    public class Filter
    {
        public string Name { get; set; }
        public int LowerBound { get; set; }
        public int UpperBound { get; set; }
    }
}
