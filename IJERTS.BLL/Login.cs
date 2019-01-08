using IJERTS.DAL;
using IJERTS.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJERTS.BLL
{
    public class Login : ILogin
    {

        ILoginRepository _loginRepository = new LoginRepository();

        public Users ValidateLogin(Users users)
        {
            return _loginRepository.ValidateLogin(users);
        }

        public Users ValidateEditorLogin(Users users)
        {
            return _loginRepository.ValidateLogin(users);
        }

        

        public Users ValidateUserActivation(string sActivateLink)
        {
            return _loginRepository.ValidateUserActivation(sActivateLink);
        }

        public string InsertLoginHistory(UserLoginHistory userLoginHistory)
        {
            return _loginRepository.InsertLoginHistory(userLoginHistory);
        }

        public string UpdateUserSession(UserLoginHistory userLoginHistory)
        {
            return _loginRepository.UpdateUserSession(userLoginHistory);
        }

        public string ChangePassword(Users users)
        {
            return _loginRepository.ChangePassword(users);
        }

    }
}
