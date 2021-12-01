using MyCocktail.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCocktail.Domain.Aggregates.UserAggregate
{
    /// <summary>
    /// A user of the application
    /// </summary>
    public class User
    {
        #region Id
        private Guid? _id;

        public Guid? Id
        {
            get
            {
                return _id == null ? null : new Guid(_id.ToString());
            }
            init
            {
                _id = value;
            }
        }
        #endregion

        #region UserName
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
        #endregion

        #region FirstName
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
        #endregion

        #region LastName
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
        #endregion

        #region Email
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
        #endregion

        #region CreationDate
        private DateTime _creationDate;

        public DateTime CreationDate
        {
            get
            {
                return new DateTime(_creationDate.Year, _creationDate.Month, _creationDate.Day);
            }
            init
            {
                _creationDate = value;
            }
        }
        
        #endregion

        public string Password { get; set; }

        public UserRole Role { get; set; }

    }
}
