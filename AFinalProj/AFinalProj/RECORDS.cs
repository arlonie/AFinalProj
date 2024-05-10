using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AFinalProj
{
    public class RECORDS
    { 
       [PrimaryKey]
        public string EMPNUM { get; set; }
        public string EMPNAME { get; set; }
        public double HOURSWORK { get; set; }
        public string EMPSTATUS { get; set; }
        public string CIVILSTAT { get; set; }
        public double RATEPERHOUR { get; set; }
        public double BASIC { get; set; }
        public double OVERTIME { get; set; }
        public double GROSS { get; set; }
        public double SSS { get; set; }
        public double WTAX { get; set; }
        public double PHILHEALTH { get; set; }
        public double PAGIBIG { get; set; }
        public double DEDUCTION { get; set; }
        public double NETINCOME { get; set; }
    }
}
