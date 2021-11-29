using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Domain.Aggregates.User
{
    /// <summary>
    /// A user of the application
    /// </summary>
    public class User
    {
        private string _userName;
        public string UserName
        {
            get
            {
                return new string(_userName);
            }
            init
            {
                if (value.IsNullOrEmpty())
                {
                    throw new ArgumentException(nameof(UserName));
                }
                _userName = value.Trim();
            }
        }

        private string _firstName;
        public string FirstName
        {
            get
            {
                return new string(_firstName);
            }
            set
            {
                if (value.IsNullOrEmpty())
                {
                    throw new ArgumentException(nameof(FirstName));
                }
                if (value.ContainUnAuhtorizedChar())
                {
                    throw new ArgumentException(nameof(FirstName));
                }

                _firstName = value.Trim();
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return new string(_lastName);
            }
            set
            {
                if (value.IsNullOrEmpty())
                {
                    throw new ArgumentException(nameof(LastName));
                }
                if (value.ContainUnAuhtorizedChar())
                {
                    throw new ArgumentException(nameof(LastName));
                }

                _lastName = value.Trim();
            }
        }

        private string _email;
        public string Email
        {
            get
            {
                return new string(_email);
            }
            set
            {
                if (value.IsNullOrEmpty())
                {
                    throw new ArgumentException(nameof(Email));
                }
                if (!value.Contains("@") || !value.Contains("."))
                {
                    throw new ArgumentException(nameof(LastName));
                }

                _email = value.Trim();
            }
        }

        public string Password { get; set; }

        public UserRole Role { get; set; }

    }
}
