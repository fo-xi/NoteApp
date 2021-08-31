using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteApp;
using NUnit.Framework;

namespace UnitTesting
{
	class NoteTest
	{
        public DateTime TimeCreation { get; private set; }

        [Test(Description = "Positive test of the getter Title")]
		public void TestTitleGet_CorrectValue()
		{
			var expected = "Home";
			var note = new Note(" ", null, " ", new DateTime(2000, 11, 21))
			{
				Title = expected
			};
			var actual = note.Title;
			Assert.AreEqual(expected, actual, "The Title getter " +
				"returns an incorrect title");
		}

		[Test(Description = "Positive test of the setter Title")]
		public void TestTitleSet_CorrectValue()
		{
			var expected = "Home";
			var note = new Note(" ", null, " ", new DateTime(2000, 11, 21));
			Assert.DoesNotThrow(() =>
			{
				note.Title = expected;
			}, "The Title setter accepts the correct title");
		}

		[TestCase("HomeHomeHomeHomeHomeHomeHomeHomeHomeHomeHomeHomeHomeHomeHomeHomeHomeHome", 
			"An exception may occur if the title contains more than 50 symbol",
		TestName = "Assigning an incorrect title that contains more than 50 symbol")]
		public void TestTitle_InvalidTitle(string wrongTitle, string message)
		{
			var note = new Note(" ", null, " ", new DateTime(2000, 11, 21));
			note.Title = wrongTitle;
			Assert.IsTrue(note.HasErrors);
		}

		[Test(Description = "Positive test of the getter Text")]
		public void TestTextGet_CorrectValue()
		{
			var expected = "Text";
			var note = new Note(" ", null, " ", new DateTime(2000, 11, 21))
			{
				Text = expected
			};
			var actual = note.Text;
			Assert.AreEqual(expected, actual, "The Text getter " +
				"returns an incorrect text");
		}

		[Test(Description = "Positive test of the setter Text")]
		public void TestTextSet_CorrectValue()
		{
			var expected = "Text";
			var note = new Note(" ", null, " ", new DateTime(2000, 11, 21));
			Assert.DoesNotThrow(() =>
			{
				note.Text = expected;
			}, "The Text setter accepts the correct text");
		}

		[Test(Description = "Positive test of the getter NoteCategory")]
		public void TestNoteCategoryGet_CorrectValue()
		{
			var expected = Category.Home;
			var note = new Note(" ", null, " ", new DateTime(2000, 11, 21))
			{
				NoteCategory = expected
			};
			var actual = note.NoteCategory;
			Assert.AreEqual(expected, actual, "The NoteCategory getter " +
				"returns an incorrect note category");
		}

		[Test(Description = "Positive test of the setter NoteCategory")]
		public void TestNoteCategorySet_CorrectValue()
		{
			var expected = Category.Home;
			var note = new Note(" ", null, " ", new DateTime(2000, 11, 21));
			Assert.DoesNotThrow(() =>
			{
				note.NoteCategory = expected;
			}, "The NoteCategory setter accepts the correct note category");
		}

		[Test(Description = "Positive test of the getter TimeCreation")]
		public void TestTimeCreationGet_CorrectValue()
		{
			var expected = DateTime.Now;
			var note = new Note(" ", null, " ", new DateTime(2000, 11, 21));
			{
				TimeCreation = expected;
			};
			var actual = note.TimeCreation;
			Assert.AreEqual(expected, actual, "The TimeCreation getter " +
				"returns an incorrect time creation");
		}

		[Test(Description = "Positive test of the getter LastModifiedTime")]
		public void TestLastModifiedTimehGet_CorrectValue()
		{
			var expected = new DateTime(2000, 11, 21);
			var note = new Note(" ", null, " ", new DateTime(2000, 11, 21));
			var actual = note.LastModifiedTime;
			Assert.AreEqual(expected, actual, "The LastModifiedTime getter " +
				"returns an incorrect date of last modified time");
		}

		[Test(Description = "Positive test of the setter LastModifiedTime")]
		public void TestLastModifiedTimeSet_CorrectValue()
		{
			var expected = new DateTime(2000, 6, 4);
			var note = new Note(" ", null, " ", new DateTime(2000, 11, 21));
			Assert.DoesNotThrow(() =>
			{
				note.LastModifiedTime = expected;
			}, "The LastModifiedTime setter accepts the correct date of last modified time");
		}

		[Test(Description = "Positive test of the constructor Note")]
		public void TestContactConstructor_CorrectValue()
		{
			var title = "Home";
			var noteCategory = Category.Home;
			var text = "HomeHomeHomeHomeHomeHomeHome";
			var timeCreation = DateTime.Now;
			var lastModifiedTime = new DateTime(2020, 11, 21);
			Assert.DoesNotThrow(() =>
			{
				var note = new Note(title, noteCategory, text, lastModifiedTime);
			}, "The Note constructor create a note object");
		}
	}
}
