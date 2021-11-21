using System;
using System.Collections.Generic;
using System.Text;

namespace MyGanAPP.Models
{
    class GanCode
    {
        public int KindergartenID { get; set; }
        public string Code;

        public string CreateGanCode()
        {
            Code = "G" + $"{ App.Code / KindergartenID}" + "N" + $" {App.Code % KindergartenID}";
            return Code;
        }

        public int CodeToGanID()
        {

            
            return 0;
        }
    }
}
