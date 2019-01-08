using IJERTS.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.BLL
{
    public interface ILogin
    {
        Users ValidateLogin(Users users);

        Users ValidateEditorLogin(Users users);

        Users ValidateUserActivation(string sActivateLink);

        string InsertLoginHistory(UserLoginHistory userLoginHistory);

        string UpdateUserSession(UserLoginHistory userLoginHistory);

        string ChangePassword(Users users);

    }
}
