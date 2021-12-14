using MyCocktail.Domain.Helper;
using System;

namespace MyCocktail.Domain.Aggregates.UserAggregate
{
    /// <summary>
    /// A user of the application
    /// </summary>
    public class User
    {
        #region Id
        private Guid? _id;

        /// <summary>
        /// Unique id
        /// </summary>
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
        /// <summary>
        /// Username used in the service (pseudonym)
        /// Can not be null or empty, will be trimed
        /// </summary>
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

        /// <summary>
        /// User FirstName, can not be null, empty or containing unauthorized character
        /// </summary>
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

        /// <summary>
        /// User Lastname, can not be null, empty or containing unauthorized character
        /// </summary>
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

        /// <summary>
        /// User email, can not be null or empty, must contain "@"and "."
        /// </summary>
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

        /// <summary>
        /// User Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User authorisation / Role
        /// </summary>
        public UserRole Role { get; set; }

    }
}
