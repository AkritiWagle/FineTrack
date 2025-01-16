using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FineTrack.Database
{
    //Table named Transaction for entries of the transactions
    public class Debt
    {
        [PrimaryKey, AutoIncrement]
        public int DebtId { get; set; }
        public string DebtTitle { get; set; }

        public string DebtCategory { get; set; }

        public string DebtStatus { get; set; }

        public decimal DebtAmount { get; set; }

        public string DebtSource { get; set; }

        public DateTime DebtDate { get; set; }

        public DateTime DebtDueDate { get; set; }

        public string DebtRemarks { get; set; }
        public int UserId { get; set; }


    }
}
