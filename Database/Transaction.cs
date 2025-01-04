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
    public class Transaction
    {
        [PrimaryKey,AutoIncrement]
        public int TransactionId { get; set; }


        public string TransactionType { get; set; }

        public decimal TransactionAmount { get; set; }

        public string TransactionSource { get; set; }

        public DateTime TransactionDate { get; set; }

        public string Remarks { get; set; }

    }
}
