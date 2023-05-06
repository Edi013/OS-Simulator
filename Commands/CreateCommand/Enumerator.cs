using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateOS.Business
{
    //iterator / cursor / generator / IEnumerable
    internal class Iterator 
    {
        private List<char> values;
        private int position;
   
        public Iterator(string contentType)
        {
            position = 0;
            switch (contentType.ToLower())
            {
                case "-alfa":
                    {
                        values = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
                        break;
                    }
                case "-num":
                    {
                        values = "0123456789".ToList();
                        break;
                    }
                case "-hex":
                    {
                        values = "0123456789ABCDEF".ToList();
                        break;
                    }

            }
        }
        public char Next()
        {
            if (position == values.Count)
                Reset();
            return values[position++];
        }
        private void Reset()
        {
            position = 0;
        }
    }
}
