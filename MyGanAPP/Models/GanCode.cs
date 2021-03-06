using System;
using System.Collections.Generic;
using System.Text;

namespace MyGanAPP.Models
{
    public class GanCode
    {
        private const int HASHCODE = 9973;

        public static string CreateGroupCode(int ID)
        {
           return "G" + $"{HASHCODE / ID}" + "N" + $"{HASHCODE % ID}";
            
        }

        public static int CodeToGroupID(string code)
        {
            int gPos = code.IndexOf('G');
            int nPos = code.IndexOf('N');

            if (gPos != 0 || nPos <= 2 )
                return 0;

            string param1 = code.Substring(1, nPos - 1);
            string param2 = code.Substring(nPos + 1);

            int p1 = 0, p2 = 0;
            if (!int.TryParse(param1, out p1) ||
                !int.TryParse(param2, out p2))
                return 0;

            return (HASHCODE - p2 ) / p1;

        }
    }
}
