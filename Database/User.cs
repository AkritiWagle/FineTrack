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
    //Table named User for entries of the transactions
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string PreferredCurrency { get; set; }

    }
}
