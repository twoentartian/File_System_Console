using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace OPClassProgramDesignFinalExam
{
	//This class uses singleton, only one file system
	class FileSystem
	{
		#region Construction
		private static FileSystem _instance;
		protected FileSystem()
		{

		}
		public static FileSystem Instance()
		{
			return _instance ?? (_instance = new FileSystem());
		}
		#endregion

		public Folder BASE = new Folder("BASE");
	}

	class FileAndFolder : IConsoleInterfacePrint
	{
		#region Property
		private string _Name;
		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
			}
		}
		public virtual string ClassType()
		{
			return "File And Folder";
		}
		#endregion

		public FileAndFolder NextFileAndFolder;
		public FileAndFolder LastFileAndFolder;
		
		public void PrintLine(string arg)
		{
			System.Console.Write("    ");
			System.Console.WriteLine(arg);
		}
		public void Print(string arg)
		{
			System.Console.Write(arg);
		}
		public virtual void Open()
		{
			PrintLine("Open Target:"+Name);
		}
	}

	class File : FileAndFolder
	{
		#region Construction
		public File(string argName, string argContent)
		{
			this.Name = argName;
			this.Content = argContent;
		}
		public File()
		{

		}
		#endregion

		#region Property
		private string _Content;
		public string Content
		{
			get
			{
				return _Content;
			}
			set
			{
				_Content = value;
			}
		}
		public override string ClassType()
		{
			return "File";
		}
		#endregion

		public override void Open()
		{
			base.Open();
			PrintLine("Content:"+Content);
		}
	}

	class Folder : FileAndFolder
	{
		#region Construction
		public Folder(string arg)
		{
			this.Name = arg;
		}

		public Folder()
		{

		}
		#endregion

		#region Property
		public override string ClassType()
		{
			return "Folder";
		}
		#endregion

		public FileAndFolder StartFileAndFolder;

		public Folder ParentFolder;

		//Get the location of the end FileAndFolder
		private void ToEnd(ref FileAndFolder argFileAndFolder)
		{
			if (argFileAndFolder.NextFileAndFolder == null)
			{
				return;
			}
			else
			{
				argFileAndFolder = argFileAndFolder.NextFileAndFolder;
				ToEnd(ref argFileAndFolder);
			}
		}

		//Provide the leading FileAndFolder to this method
		private void PrintAll(ref FileAndFolder argFileAndFolder, ref string argString)
		{
			argString = argString + argFileAndFolder.Name + "(" + argFileAndFolder.ClassType() + ")";
			if (argFileAndFolder.NextFileAndFolder == null)
			{
				return;
			}
			else
			{
				argString += ", ";
				argFileAndFolder = argFileAndFolder.NextFileAndFolder;
				PrintAll(ref argFileAndFolder, ref argString);
			}
		}

		public void SelfPrint()
		{
			if (StartFileAndFolder == null)
			{
				PrintLine("Empty Folder");
			}
			else
			{
				Print("    " + this.Name + " includes: ");
				FileAndFolder tempFileAndFolder = StartFileAndFolder;
				string tempOutput = "";
				PrintAll(ref tempFileAndFolder, ref tempOutput);
				PrintLine(tempOutput);
			}
		}

		public void AddFileAndFolder(FileAndFolder argFileAndFolder)
		{
			if (StartFileAndFolder == null)
			{
				StartFileAndFolder = argFileAndFolder;
			}
			else
			{
				FileAndFolder tempFileAndFolder = StartFileAndFolder;
				ToEnd(ref tempFileAndFolder);
				tempFileAndFolder.NextFileAndFolder = argFileAndFolder;
				argFileAndFolder.LastFileAndFolder = tempFileAndFolder;
			}
		}

		//Provide the leading FileAndFolder to this method
		//Return code represents the exit error number. 0: no error. 1: no name.
		private int _FindFileAndFolder(ref FileAndFolder argFileAndFolder, string argString, FileAndFolder type)
		{
			if (argFileAndFolder.Name == argString && argFileAndFolder.GetType() == type.GetType())
			{
				return 0;
			}
			else
			{
				if (argFileAndFolder.NextFileAndFolder != null)
				{
					argFileAndFolder = argFileAndFolder.NextFileAndFolder;
					_FindFileAndFolder(ref argFileAndFolder, argString, type);
				}
				else
				{
					return 1;
				}
				return 0;
			}
		}

		//Return code represents the exit error number. 0: no error. 1: no name.
		public int FindFileAndFolder(ref FileAndFolder argFileAndFolder, string argString, FileAndFolder type)
		{
			if (StartFileAndFolder == null)
			{
				argFileAndFolder = null;
				return 1;
			}
			else
			{
				FileAndFolder tempfilAndFolder = StartFileAndFolder;
				if (_FindFileAndFolder(ref tempfilAndFolder, argString, type) == 0)
				{
					argFileAndFolder = tempfilAndFolder;
					return 0;
				}
				else
				{
					return 1;
				}
			}
		}

		public override void Open()
		{
			base.Open();
			if (StartFileAndFolder == null)
			{
				PrintLine("Empty Folder");
			}
			else
			{
				Print("    " + this.Name + " includes: ");
				FileAndFolder tempFileAndFolder = StartFileAndFolder;
				string tempOutput = "";
				PrintAll(ref tempFileAndFolder, ref tempOutput);
				PrintLine(tempOutput);
			}
		}
	}
}
