using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateFingerprint
{
    public class ListItem
    {
        public string Value { get; internal set; }
        public string Display { get; internal set; }

        public ListItem()
        { }

        public ListItem(string value, string display)
        {
            Value = value;
            Display = display;
        }
    }
}
