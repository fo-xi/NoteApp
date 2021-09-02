using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    /// <summary>
    /// Field validation class.
    /// </summary>
	public static class Validator
	{
        /// <summary>
        /// Checking the length of a string.
        /// </summary>
        /// <param name="value">String.</param>
        /// <param name="finalLength">Final length.</param>
        /// <param name="message">Error message.</param>
        /// <returns></returns>
        public static bool IsStringLength(string value, int finalLength, out string message)
        {
            message = String.Empty;

            if (value.Length > finalLength)
            {
                message = "The value must be less than 50 characters";
                return false;
            }
            return true;
        }
    }
}
