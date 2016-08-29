using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OPClassProgramDesignFinalExam
{
	class Memo
	{
		#region Construction
		private static Memo _instance;
		protected Memo()
		{

		}
		public static Memo Instance()
		{
			return _instance ?? (_instance = new Memo());
		}
		#endregion

		#region WriteDataPart
		private void AddNextFileAndFolder(ref FileAndFolder argFileAndFolder)
		{
			if (argFileAndFolder == null)
			{

			}
			else
			{
				if (argFileAndFolder.ClassType() == "File")
				{
					File tempFile = (File)argFileAndFolder;
					Data = Data + "#FiNa#" + tempFile.Name;
					Data = Data + "#FiCo#" + tempFile.Content;

				}
				else if (argFileAndFolder.ClassType() == "Folder")
				{
					Folder tempFolder = (Folder)argFileAndFolder;
					Data = Data + "#FoNa#" + tempFolder.Name;
					AddFilesInFolder(ref tempFolder);
				}
				AddNextFileAndFolder(ref argFileAndFolder.NextFileAndFolder);
			}
		}

		private void AddFilesInFolder(ref Folder argFolder)
		{
			if (argFolder.StartFileAndFolder != null)
			{
				AddNextFileAndFolder(ref argFolder.StartFileAndFolder);
			}
			Data = Data + "#FoEn#";
		}

		private string Data;

		public int WriteToMemo(string argString)
		{
			string nowPath = System.Environment.CurrentDirectory;
			if (System.IO.File.Exists(nowPath + "\\" + argString + RunTimeParameter.ExtensionName))
			{
				return 1;
			}
			Data = "";
			FileSystem BASEFileSystem = FileSystem.Instance();
			FileAndFolder tempFileAndFolder = BASEFileSystem.BASE.StartFileAndFolder;
			AddNextFileAndFolder(ref tempFileAndFolder);
			FileStream mainFileStream = new FileStream(nowPath + "\\" + argString + RunTimeParameter.ExtensionName, FileMode.Create);
			StreamWriter mainStreamWriter = new StreamWriter(mainFileStream);
			Data = Data + "#";
			mainStreamWriter.Write(Data);
			mainStreamWriter.Flush();
			mainFileStream.Flush();
			mainStreamWriter.Close();
			mainFileStream.Close();
			return 0;
		}
		#endregion

		#region ReadDataPart
		//Return code 0: no error. 1:Data file is invaild
		private int ReadFromMemoAllFile(ref string argString, ref Folder argFolder)
		{
			string fileName = "";
			string fileContent = "";
			string folderName = "";

			while (argString != "")
			{
				if (argString == "#")
				{
					return 0;
				}
				if (argString.Substring(0, 6) == "#FiNa#")
				{
					int locationOfNextCommand;
					bool fileState = false;
					for (locationOfNextCommand = 6; locationOfNextCommand < argString.Length; locationOfNextCommand++)
					{
						if (argString.Substring(locationOfNextCommand, 1) == "#")
						{
							fileName = argString.Substring(6, locationOfNextCommand - 6);
							fileState = true;
							break;
						}
					}
					if (!fileState)
					{
						return 1;
					}
					fileState = false;
					for (int locationEndOfFile = locationOfNextCommand + 6; locationEndOfFile < argString.Length; locationEndOfFile++)
					{
						if (argString.Substring(locationEndOfFile, 1) == "#")
						{
							fileContent = argString.Substring(locationOfNextCommand + 6, locationEndOfFile - locationOfNextCommand - 6);
							fileState = true;
							break;
						}
					}
					if (!fileState)
					{
						return 1;
					}
					argFolder.AddFileAndFolder(new File(fileName, fileContent));
					argString = argString.Substring(12 + fileName.Length + fileContent.Length);
					if (ReadFromMemoAllFile(ref argString, ref argFolder) == 1)
					{
						return 1;
					}
				}
				else if (argString.Substring(0, 6) == "#FoNa#")
				{
					bool fileState = false;
					for (int locationOfNextCommand = 6; locationOfNextCommand < argString.Length; locationOfNextCommand++)
					{
						if (argString.Substring(locationOfNextCommand, 1) == "#")
						{
							folderName = argString.Substring(6, locationOfNextCommand - 6);
							fileState = true;
							break;
						}
					}
					if (!fileState)
					{
						return 1;
					}
					Folder tempFolder = new Folder(folderName);
					tempFolder.ParentFolder = argFolder;
					argFolder.AddFileAndFolder(tempFolder);
					argString = argString.Substring(folderName.Length + 6);
					if (ReadFromMemoAllFile(ref argString, ref tempFolder) == 1)
					{
						return 1;
					}
				}
				else if (argString.Substring(0, 6) == "#FoEn#")
				{
					argString = argString.Substring(6);
				}
				else
				{
					return 1;
				}
			}
			return 0;
		}

		private readonly FileSystem BASEFileSystem = FileSystem.Instance();

		private User aUser = User.Instance();

		//Return code 0: no error. 1:No file. 2:Data file is invaild
		public int ReadFromMemo(string argString)
		{
			BASEFileSystem.BASE.StartFileAndFolder = null;
			StreamReader strReader;
			string nowPath = System.Environment.CurrentDirectory;
			if (System.IO.File.Exists(nowPath + "\\" + argString + RunTimeParameter.ExtensionName))
			{
				strReader = new StreamReader(nowPath + "\\" + argString + RunTimeParameter.ExtensionName, false);
				Data = "";
				string readLine = strReader.ReadLine();
				if (readLine != null)
				{
					Data = readLine.ToString();
					int temp = ReadFromMemoAllFile(ref Data, ref BASEFileSystem.BASE);
                    if (temp == 0)
                    {
	                    aUser.NowLocation = BASEFileSystem.BASE;
	                    BASEFileSystem.BASE.Open();
	                    return 0;
                    }
					else if (temp == 1)
					{
						return 2;
					}
					else
					{
						return 0; //Never reach
					}
				}
				else
				{
					return 2;
				}
			}
			else
			{
				return 1;
			}
		}
		#endregion
	}
}
