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
    //Table named TransactionType for category/transaction type management
    public class TransactionType
    {
        [PrimaryKey, AutoIncrement]
        public int TransactionTypeId { get; set; }

        
        public string TypeOfTransaction { get; set; }

        public string TransactionCategory { get; set; }
        public int UserId { get; set; }


    }
}
