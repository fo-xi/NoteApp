using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NoteApp;
using NUnit.Framework;

namespace UnitTesting
{
	class ProjectTest
	{
		[Test(Description = "Positive test of the getter Notes")]
		public void TestNotesGet_CorrectValue()
		{
			var project = new Project();
			var expected = new ObservableCollection<Note>()
			{
				new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", DateTime.Now),
				new Note ("Work", Category.Work, "WorkWorkWorkWorkWork", DateTime.Now),
			};
			project.Notes = expected;
			var actual = project.Notes;
			Assert.AreEqual(expected, actual, "The Notes getter " +
											  "returns an incorrect list");
		}

		[Test(Description = "Positive test of the setter Notes")]
		public void TestNotesSet_CorrectValue()
		{
			var project = new Project();
			var expected = new ObservableCollection<Note>()
			{
				new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", DateTime.Now),
				new Note ("Work", Category.Work, "WorkWorkWorkWorkWork", DateTime.Now),
			};
			Assert.DoesNotThrow(() =>
			{
				project.Notes = expected;
			}, "The Notes setter accepts the correct notes");
		}

		[Test(Description = "Test of the SortingNotes " +
			"when the input list is not empty")]
		public void TestSortingNotes_ListNotEmpty()
		{
			var project = new Project
			{
				Notes = new ObservableCollection<Note>()
				{
					new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", new DateTime(2013, 6, 7)),
					new Note ("Work", Category.Work, "WorkWorkWorkWorkWork", new DateTime(2011, 6, 7)),
					new Note ("Finance", Category.Finance, "FinanceFinanceFinanceFinance", new DateTime(2015, 6, 7)),
					new Note ("Documentation", Category.Documentation, "DocumentationDocumentation", new DateTime(2016, 6, 7)),
				}
			};

			var expected = new Project
			{
				Notes = new ObservableCollection<Note>()
				{
					new Note ("Work", Category.Work, "WorkWorkWorkWorkWork", new DateTime(2011, 6, 7)),
					new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", new DateTime(2013, 6, 7)),
					new Note ("Finance", Category.Finance, "FinanceFinanceFinanceFinance", new DateTime(2015, 6, 7)),
					new Note ("Documentation", Category.Documentation, "DocumentationDocumentation", new DateTime(2016, 6, 7))
				}
			};

			var actual = new Project
			{
				Notes = Project.SortingNotes(project.Notes)
			};

			Assert.AreEqual(expected.Notes[1].Title,
				actual.Notes[1].Title, "Returns an unordered notes");
		}

		[Test(Description = "Test of the SortingNotes " +
							"when the input list is empty")]
		public void TestSortingNotes_ListEmpty()
		{
			var project = new Project
			{
				Notes = new ObservableCollection<Note>()
			};
			var expected = new ObservableCollection<Note>();
			var actual = Project.SortingNotes(project.Notes);

			Assert.AreEqual(expected, actual, "The list is empty");
		}

		[Test(Description = "Test of the SortingNotesCategory " +
							"when the input list is not empty")]
		public void TestSortingNotesCategory_ListNotEmpty()
		{ 
			var project = new Project
			{
				Notes = new ObservableCollection<Note>()
				{
					new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", new DateTime(2013, 6, 7)),
					new Note ("Work", Category.Work, "WorkWorkWorkWorkWork", new DateTime(2011, 6, 7)),
					new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", new DateTime(2015, 6, 7)),
					new Note ("Documentation", Category.Documentation, "DocumentationDocumentation", new DateTime(2016, 6, 7)),
				}
			};
			var expected = new Project
			{
				Notes = new ObservableCollection<Note>()
				{
					new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", new DateTime(2013, 6, 7)),
					new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", new DateTime(2015, 6, 7)),
				}
			};

			var actual = new Project
			{
				Notes = Project.SortingNotes(Category.Home, project.Notes)
			};

			Assert.AreEqual(expected.Notes[1].Title,
				actual.Notes[1].Title, "Returns an unordered notes");
		}

		[Test(Description = "Test of the SortingNotesCategory " +
							"when the input list is empty")]
		public void TestSortingNotesCategory_ListEmpty()
		{
			var project = new Project
			{
				Notes = new ObservableCollection<Note>()
			};
			var expected = new List<Note>();
			var actual = Project.SortingNotes(Category.Home, project.Notes);

			Assert.AreEqual(expected, actual, "The list is empty");
		}

		[Test(Description = "Test of the SortingNotes with " +
							"the found note category")]
		public void TestSortingNotesCategory_Category()
		{
			var project = new Project
			{
				Notes = new ObservableCollection<Note>()
				{
					new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", new DateTime(2013, 6, 7)),
					new Note ("Work", Category.Work, "WorkWorkWorkWorkWork", new DateTime(2011, 6, 7)),
					new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", new DateTime(2015, 6, 7)),
					new Note ("Documentation", Category.Documentation, "DocumentationDocumentation", new DateTime(2016, 6, 7)),
				}
			};
			var expected = new Project
			{
				Notes = new ObservableCollection<Note>()
				{
					new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", new DateTime(2013, 6, 7)),
					new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", new DateTime(2015, 6, 7)),
				}
			};

			var actual = new Project
			{
				Notes = Project.SortingNotes(Category.Home, project.Notes)
			};

			Assert.AreEqual(expected.Notes[1].Title,
				actual.Notes[1].Title, "Returns an unordered notes");
		}

		[Test(Description = "Test of the SortingNotesCategory" +
							"when there is no suitable note in the list")]
		public void TestSortingNotesCategory_Noategory()
		{
			var project = new Project
			{
				Notes = new ObservableCollection<Note>()
				{
					new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", new DateTime(2013, 6, 7)),
					new Note ("Work", Category.Work, "WorkWorkWorkWorkWork", new DateTime(2011, 6, 7)),
					new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", new DateTime(2015, 6, 7)),
					new Note ("Documentation", Category.Documentation, "DocumentationDocumentation", new DateTime(2016, 6, 7)),
				}
			};
			var expected = new Project
			{
				Notes = new ObservableCollection<Note>()
			};
			var expectedString = JsonConvert.SerializeObject(expected.Notes);

			var actual = new Project
			{
				Notes = Project.SortingNotes(Category.People, project.Notes)
			};
			var actualString = JsonConvert.SerializeObject(actual.Notes);

			Assert.AreEqual(expectedString, actualString, "Returns an invalid value");
		}

		[Test(Description = "Test of the SortingNotesCategory " +
							"when all notes are written to the list with the selected category")]
		public void TestSortingNotesCategory_AllCategory()
		{
			var project = new Project
			{
				Notes = new ObservableCollection<Note>()
				{
					new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", new DateTime(2013, 6, 7)),
					new Note ("Work", Category.Work, "WorkWorkWorkWorkWork", new DateTime(2011, 6, 7)),
					new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", new DateTime(2015, 6, 7)),
					new Note ("Documentation", Category.Documentation, "DocumentationDocumentation", new DateTime(2016, 6, 7)),
				}
			};
			var expected = new Project
			{
				Notes = new ObservableCollection<Note>()
				{
					new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", new DateTime(2013, 6, 7)),
					new Note ("Work", Category.Work, "WorkWorkWorkWorkWork", new DateTime(2011, 6, 7)),
					new Note ("Home", Category.Home, "HomeHomeHomeHomeHome", new DateTime(2015, 6, 7)),
					new Note ("Documentation", Category.Documentation, "DocumentationDocumentation", new DateTime(2016, 6, 7)),
				}
			};

			var actual = new Project
			{
				Notes = Project.SortingNotes(Category.All, project.Notes)
			};

			Assert.AreEqual(expected.Notes[2].Title,
				actual.Notes[2].Title, "Returns an unordered notes");
		}
	}
}
