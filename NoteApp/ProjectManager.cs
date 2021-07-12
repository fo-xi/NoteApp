using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
	public static class ProjectManager
	{
        /// <summary>
        /// File save folder.
        /// </summary>
        private static readonly string _folder = Environment.GetFolderPath
            (Environment.SpecialFolder.ApplicationData) + "\\NoteApp\\";

        /// <summary>
        /// Save file.
        /// </summary>
        private static readonly string _file = "Note.txt";

        /// <summary>
        /// File save path.
        /// </summary>
        public static string Path { get; set; } = _folder + _file;

        /// <summary>
        /// Saves the object <see cref="Project"/> to a file.
        /// </summary>
        /// <param name="project">All contact information.</param>
        public static void WriteToFile(Project project)
        {
            if (!Directory.Exists(_folder))
            {
                Directory.CreateDirectory(_folder);
            }

            if (!File.Exists(Path))
            {
                File.Create(Path).Close();
            }

            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(Path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, project);
            }
        }

        /// <summary>
        /// Reads information about the object <see cref="Project"/>
        /// from a file.
        /// </summary>
        public static Project ReadFromFile()
        {
            Project project = new Project();

            if (File.Exists(Path))
            {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamReader sr = new StreamReader(Path))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    try
                    {
                        project = ((Project)serializer.Deserialize<Project>(reader));
                    }
                    catch
                    {
                        project = new Project();
                    }
                }
            }

            return project;
        }
    }
}
