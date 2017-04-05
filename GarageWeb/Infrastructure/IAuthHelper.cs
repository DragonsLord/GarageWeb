using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageWeb.Infrastructure
{
    public interface IAuthHelper
    {
        bool SignIn(string login, string password);

        void LogOut();

        bool SetNewLogin(string new_login);

        bool SetNewPassword(string old_password, string new_password);
    }
}
