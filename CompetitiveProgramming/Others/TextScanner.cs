using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveProgramming.Others
{
    public class TextScanner
    {
        private readonly TextReader _tr;

        public TextScanner(TextReader tr)
        {
            _tr = tr;
        }
        
        public string Next()
        {
            var sb = new StringBuilder();
            int i;
            do
            {
                i = _tr.Read();
                if (i == -1) throw new EndOfStreamException();
            }
            while (char.IsWhiteSpace((char)i));
            while (i != -1 && !char.IsWhiteSpace((char)i))
            {
                sb.Append((char)i);
                i = _tr.Read();
            }
            return sb.ToString();
        }
    }
}
