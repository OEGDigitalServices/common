using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Profile
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    public class UserTicketData
    {
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        /// <value>The user ID.</value>
        /// <remarks></remarks>
        public Guid UserID { get; set; }

        /// <summary>
        /// Gets or sets the dial number.
        /// </summary>
        /// <value>The dial number.</value>
        /// <remarks></remarks>
        public string DialNumber { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        /// <remarks></remarks>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        /// <remarks></remarks>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        /// <remarks></remarks>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is mobinil.
        /// </summary>
        /// <value><c>true</c> if this instance is mobinil; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public bool IsMobinil { get; set; }

        /// <summary>
        /// Gets or sets Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the name of the nick.
        /// </summary>
        /// <value>
        /// The name of the nick.
        /// </value>
        public string NickName { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is CP user.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is CP user; otherwise, <c>false</c>.
        /// </value>
        public bool? IsCPUser { get; set; }

        public string CookieCreatedDate { get; set; }

        public override string ToString()
        {
            return String.Format("UserID:{0}\t\tDialNumber:{1}\t\tEmail:{2}\t\tFirstName:{3}\t\tLastName:{4}\t\tIsMobinil:{5}\t\tAddress:{6}\t\tNickName:{7}\t\tIsCPUser:{8}", UserID.ToString(), DialNumber, Email, FirstName, LastName, IsMobinil, Address, NickName, IsCPUser);
        }
        /// <summary>
        /// Gets or sets the cp account number.
        /// </summary>
        /// <value>
        /// The cp account number.
        /// </value>
        public string CPAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is guest.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is guest; otherwise, <c>false</c>.
        /// </value>
        public bool IsGuest { get; set; }
    }
}
