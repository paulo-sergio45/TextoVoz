using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextoVoz.Mvvm.Models
{
    class ScrollToIndexMessage
    {
        public int Index { get; }

        public ScrollToIndexMessage(int index) => Index = index;
    }
}
