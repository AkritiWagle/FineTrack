using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FineTrack.Database;

namespace FineTrack.Services
{
    public class UserSessionService
    {
        public User CurrentUser { get; set; }

        public bool IsLoggedIn => CurrentUser != null;

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
