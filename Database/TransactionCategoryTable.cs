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
    //Table named TransactionCategoryTable for category/transaction type management
    public class TransactionCategoryTable
    {
        [PrimaryKey, AutoIncrement]
        public int TransactionTypeId { get; set; }

        public string TypeOfTransaction { get; set; }

        public string TransactionCategory { get; set; }

    }
}
