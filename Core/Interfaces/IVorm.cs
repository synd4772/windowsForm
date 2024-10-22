using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KolmRakendust.Core.Enums;

namespace KolmRakendust.Core.Interfaces
{
    public interface IVorm
    {
        public string VormName { get; set; }
        public FormType FormType { get; set; }
    }
}
