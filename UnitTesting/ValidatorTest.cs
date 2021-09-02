using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteApp;
using NUnit.Framework;

namespace UnitTesting
{
	class ValidatorTest
	{
		[Test(Description = "A positive validator test that checks the length of a string")]
		public void TestStringLength_CorrectValue()
		{
			var expected = "Home";
			Assert.IsTrue(Validator.IsStringLength(expected, 50, out string message));
		}

		[TestCase("HomeHomeHomeHomeHomeHomeHomeHomeHomeHomeHomeHomeHomeHome", 
			"An exception can occur if there is a line longer than 50 characters",
			TestName = "Assigning an incorrect string of more than 50 symbols")]
		public void TestStringLength_ManyCharacters(string wrongString, string message)
		{
			Assert.IsFalse(Validator.IsStringLength(wrongString, 50, out string value), message);
		}
	}
}
