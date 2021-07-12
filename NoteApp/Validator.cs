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
        /// <param name="initialLength">Initial length.</param>
        /// <param name="finalLength">Final length.</param>
        /// <param name="message">Error message.</param>
        /// <returns></returns>
        public static bool IsStringLength(string value,
            int initialLength, int finalLength, out string message)
        {
            message = String.Empty;

            if ((value.Length < initialLength) || (value.Length > finalLength))
            {
                message = "Value must be in the range from " +
                          initialLength + " to " + finalLength;
                return false;
            }
            return true;
        }
    }
}
