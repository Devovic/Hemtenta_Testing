using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemtenta_Antonio_Mirkovic.blog
{
    public class Blog : IBlog
    {
        User _user;
        IAuthenticator _auth;

        public Blog(IAuthenticator auth)
        {
            _auth = auth;
        }

        public bool UserIsLoggedIn
        {
            get
            {
                if (_user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void LoginUser(User u)
        {
            if (u == null)
            {
                throw new Exception();
            }

            var dbUser = _auth.GetUserFromDatabase(u.Name);
            _user = dbUser;
        }

        public void LogoutUser(User u)
        {
            if (u == null)
            {
                throw new Exception();
            }
            _user = null;
        }

        public bool PublishPage(Page p)
        {
            if (p == null
                || string.IsNullOrEmpty(p.Title)
                || string.IsNullOrEmpty(p.Content))
            {
                throw new Exception();
                
            }

            if (!UserIsLoggedIn)
            {
                return false;
            }
            return true;
        }

        public int SendEmail(string address, string caption, string body)
        {
            if (string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(caption) || string.IsNullOrWhiteSpace(body) || !UserIsLoggedIn == true)
            {
                return 0;
            }
            else
            {
                return 1;
            }
            
        }
    }
}
