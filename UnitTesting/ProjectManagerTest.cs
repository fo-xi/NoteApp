using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteApp;
using NUnit.Framework;
using System.IO;
using System.Reflection;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace UnitTesting
{
	class ProjectManagerTest
	{
        public static readonly string path =
           Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Data.txt";

        public static readonly string referencePath =
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            + "\\ReferencePath\\Data.txt";

        public static readonly string incorrectData =
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            + "\\ReferencePath\\IncorrectData.txt";

        public static readonly string nonЕxistentPath = "..\\nkbrnb\\fbk.txt";


        [Test(Description = "A test writing to a file")]
        public void TestWriteToFile_WithCreatedFile()
        {
            ProjectManager.Path = path;
            if (File.Exists(ProjectManager.Path))
            {
                File.Delete(ProjectManager.Path);
            }
            File.Create(ProjectManager.Path).Close();

            var newProject = new Project
            {
                Notes = new ObservableCollection<Note>()
                {
                    new Note ("Home", Category.Home, "HomeHomeHomeHome",
                    new DateTime(2013, 6, 7)),
                    new Note ("Work", Category.Work, "WorkWorkWorkWork",
                    new DateTime(2011, 3, 8)),
                }
            };
            var expectedString = JsonConvert.SerializeObject(newProject);
            ProjectManager.WriteToFile(newProject);
            var actualString = File.ReadAllText(ProjectManager.Path);
            Assert.AreEqual(expectedString, actualString,
                "An exception may occur if the file cannot be saved");
        }

        [Test(Description = "A positive test reading from a file")]
        public void TestReadFromFile_CorrectData()
        {
	        ProjectManager.Path = path;
	        if (File.Exists(ProjectManager.Path))
	        {
		        File.Delete(ProjectManager.Path);
	        }

	        File.Create(ProjectManager.Path).Close();
	        var expectedString = File.ReadAllText(referencePath);
	        var expectedProject = JsonConvert.DeserializeObject<Project>(expectedString);
	        File.WriteAllText(ProjectManager.Path, expectedString);
	        var actualProject = ProjectManager.ReadFromFile();
	        Assert.AreEqual(expectedProject.Notes[0].Title,
		        actualProject.Notes[0].Title, "Different file contents");
        }

        [Test(Description = "A negative test reading from a file")]
        public void TestReadFromFile_IncorrectData()
        {
            var expected = new Project();
            ProjectManager.Path = incorrectData;

            var actual = ProjectManager.ReadFromFile();
            Assert.AreEqual(expected.Notes, actual.Notes, "Different file contents");
        }

        [Test(Description = "A test reading to a nonexistent file path")]
        public void TestReadFromFile_NonexistentFilePath()
        {
            var expected = new Project();
            ProjectManager.Path = nonЕxistentPath;
            var actual = ProjectManager.ReadFromFile();
            Assert.AreEqual(expected.Notes, actual.Notes,
                "An exception may occur if the path does not exist");
        }
    }
}
