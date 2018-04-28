using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace InvAddIn.Drawings
{
    class Sheet
    {
        public List<DrawingView> Views { get; private set; }
        public Document Model { get; set; }
    }
}
