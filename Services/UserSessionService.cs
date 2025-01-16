using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FineTrack.Database;
using Microsoft.EntityFrameworkCore;

namespace FineTrack.Services
{
    public class UserSessionService
    {
        public User? CurrentUser { get;  private set; }

        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
        }

        public bool IsLoggedIn()
        {
            return CurrentUser != null;
            
        }

        public void Logout()
        {
            CurrentUser = null;
        }


        //this function should update the CurrentUser
       
    }
}
