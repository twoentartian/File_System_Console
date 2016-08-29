using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPClassProgramDesignFinalExam
{
	class Command : IConsoleInterfacePrint
	{
		#region Construction
		public Command()
		{
			
		}
		#endregion

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
		#endregion

		public void Print(string arg)
		{
			System.Console.Write(arg);
		}
		public void PrintLine(string arg)
		{
			System.Console.Write("    ");
			System.Console.WriteLine(arg);
		}

		public virtual string GetClassName()
		{
			return "";
		}

		public virtual void Execute(string[] argStrings, ref Folder argFolder)
		{
			
		}
	}

	class CreateFileCommand : Command
	{
		public override void Execute(string[] argStrings, ref Folder argFolder)
		{
			base.Execute(argStrings, ref argFolder);
			string name = "";
			string Content = "";
			for (int temp = 0; temp < argStrings[2].Length; temp++)
			{
				if (argStrings[2].Substring(temp, 1) == " ")
				{
					name = argStrings[2].Substring(0, temp);
					Content = argStrings[2].Substring(temp + 1);
					break;
				}
			}
			if (name == "")
			{
				name = argStrings[2];
			}
			File tempFile = new File(name, Content);
			argFolder.AddFileAndFolder(tempFile);
		}

		public override string GetClassName()
		{
			return "CreateFileCommand";
		}
	}

	class CreateFolderCommand : Command
	{
		public override void Execute(string[] argStrings, ref Folder argFolder)
		{
			base.Execute(argStrings, ref argFolder);
			Folder tempFolder = new Folder(argStrings[2]);
			tempFolder.ParentFolder = argFolder;
			argFolder.AddFileAndFolder(tempFolder);
		}

		public override string GetClassName()
		{
			return "CreateFolderCommand";
		}
	}

	class OpenFileCommand : Command
	{
		public override void Execute(string[] argStrings, ref Folder argFolder)
		{
			base.Execute(argStrings, ref argFolder);
			FileAndFolder tempFileAndFolder = new FileAndFolder();
			if (argFolder.FindFileAndFolder(ref tempFileAndFolder, argStrings[2], new File()) != 0)
			{
				PrintLine("Does not find name");
			}
			else
			{
				if (tempFileAndFolder.GetType() == (new File()).GetType())
				{
					tempFileAndFolder.Open();
				}
				else
				{
					PrintLine("Does not find name");
				}
			}
		}

		public override string GetClassName()
		{
			return "OpenFileCommand";
		}
	}
	
	class OpenFolderCommand : Command
	{
		public override void Execute(string[] argStrings, ref Folder argFolder)
		{
			base.Execute(argStrings, ref argFolder);
			if (argStrings[2] == "..")
			{
				if (argFolder.ParentFolder == null)
				{
					PrintLine("This is the BASE directory");
				}
				else
				{
					argFolder = argFolder.ParentFolder;
					argFolder.Open();
				}
			}
			else
			{
				FileAndFolder tempFileAndFolder = new FileAndFolder();
				if (argFolder.FindFileAndFolder(ref tempFileAndFolder, argStrings[2], new Folder()) != 0)
				{
					PrintLine("Does not find name");
				}
				else
				{
					if (tempFileAndFolder.GetType() == (new Folder()).GetType())
					{
						tempFileAndFolder.Open();
						argFolder = (Folder)tempFileAndFolder;
					}
					else
					{
						PrintLine("Does not find name");
					}
				}
			}
		}

		public override string GetClassName()
		{
			return "OpenFolderCommand";
		}
	}

	class DelFileCommand : Command
	{
		public override void Execute(string[] argStrings, ref Folder argFolder)
		{
			base.Execute(argStrings, ref argFolder);
			FileAndFolder tempFileAndFolder = new FileAndFolder();
			if (argFolder.FindFileAndFolder(ref tempFileAndFolder, argStrings[2], new File()) != 0)
			{
				PrintLine("Does not find name");
			}
			else
			{
				if (tempFileAndFolder.LastFileAndFolder == null && tempFileAndFolder.NextFileAndFolder == null)
				{
					argFolder.StartFileAndFolder = null;
				}
				else if (tempFileAndFolder.LastFileAndFolder != null && tempFileAndFolder.NextFileAndFolder == null)
				{
					tempFileAndFolder.LastFileAndFolder.NextFileAndFolder = null;
				}
				else if (tempFileAndFolder.LastFileAndFolder == null && tempFileAndFolder.NextFileAndFolder != null)
				{
					argFolder.StartFileAndFolder = tempFileAndFolder.NextFileAndFolder;
					tempFileAndFolder.NextFileAndFolder.LastFileAndFolder = null;
				}
				else if (tempFileAndFolder.LastFileAndFolder != null && tempFileAndFolder.NextFileAndFolder != null)
				{
					tempFileAndFolder.NextFileAndFolder.LastFileAndFolder = tempFileAndFolder.LastFileAndFolder;
					tempFileAndFolder.LastFileAndFolder.NextFileAndFolder = tempFileAndFolder.NextFileAndFolder;
				}
			}
		}

		public override string GetClassName()
		{
			return "DelFileCommand";
		}
	}

	class DelFolderCommand : Command
	{
		public override void Execute(string[] argStrings, ref Folder argFolder)
		{
			base.Execute(argStrings, ref argFolder);
			FileAndFolder tempFileAndFolder = new FileAndFolder();
			if (argFolder.FindFileAndFolder(ref tempFileAndFolder, argStrings[2], new Folder()) != 0)
			{
				PrintLine("Does not find name");
			}
			else
			{
				if (tempFileAndFolder.LastFileAndFolder == null && tempFileAndFolder.NextFileAndFolder == null)
				{
					argFolder.StartFileAndFolder = null;
				}
				else if (tempFileAndFolder.LastFileAndFolder != null && tempFileAndFolder.NextFileAndFolder == null)
				{
					tempFileAndFolder.LastFileAndFolder.NextFileAndFolder = null;
				}
				else if (tempFileAndFolder.LastFileAndFolder == null && tempFileAndFolder.NextFileAndFolder != null)
				{
					argFolder.StartFileAndFolder = tempFileAndFolder.NextFileAndFolder;
					tempFileAndFolder.NextFileAndFolder.LastFileAndFolder = null;
				}
				else if (tempFileAndFolder.LastFileAndFolder != null && tempFileAndFolder.NextFileAndFolder != null)
				{
					tempFileAndFolder.NextFileAndFolder.LastFileAndFolder = tempFileAndFolder.LastFileAndFolder;
					tempFileAndFolder.LastFileAndFolder.NextFileAndFolder = tempFileAndFolder.NextFileAndFolder;
				}
			}
		}

		public override string GetClassName()
		{
			return "DelFolderCommand";
		}
	}

	class CreateMemoCommand : Command
	{
		public override void Execute(string[] argStrings, ref Folder argFolder)
		{
			base.Execute(argStrings, ref argFolder);
			Memo tempMemo = Memo.Instance();
			if (tempMemo.WriteToMemo(argStrings[2]) == 1)
			{
				PrintLine("Name already exists, operation failed");
			}
			else
			{
				PrintLine("Write to hard disk finished");
			}
		}

		public override string GetClassName()
		{
			return "CreateMemoCommand";
		}
	}

	class OpenMemoCommand : Command
	{
		public override void Execute(string[] argStrings, ref Folder argFolder)
		{
			base.Execute(argStrings, ref argFolder);
			Memo tempMemo = Memo.Instance();
			int temp = tempMemo.ReadFromMemo(argStrings[2]);
            if (temp == 0)
			{
				PrintLine("Read from hard disk finished");
			}
			else if (temp == 1)
			{
				PrintLine("Does not find name");
			}
			else if (temp == 2)
			{
				PrintLine("Invaild Data");
			}
			else
			{
				//Never Reach
			}
		}

		public override string GetClassName()
		{
			return "OpenMemoCommand";
		}
	}

	class ListFolderCommand : Command
	{
		public override void Execute(string[] argStrings, ref Folder argFolder)
		{
			argFolder.SelfPrint();
		}

		public override string GetClassName()
		{
			return "ListFolderCommand";
		}
	}

	class NoneCommand : Command
	{
		public override string GetClassName()
		{
			return "NoneCommand";
		}
	}

	class CommandExecuter
	{
		//Notice this method when command set is changed
		#region Construction
		public CommandExecuter()
		{
			#region Build CommandChain Template
			_templateCommandChain.Now = new CreateFileCommand();
			_templateCommandChain.Next = new CommandChain();

			_templateCommandChain.Next.Now = new CreateFolderCommand();
			_templateCommandChain.Next.Next = new CommandChain();

			_templateCommandChain.Next.Next.Now = new OpenFileCommand();
			_templateCommandChain.Next.Next.Next = new CommandChain();

			_templateCommandChain.Next.Next.Next.Now = new OpenFolderCommand();
			_templateCommandChain.Next.Next.Next.Next = new CommandChain();

			_templateCommandChain.Next.Next.Next.Next.Now = new DelFileCommand();
			_templateCommandChain.Next.Next.Next.Next.Next = new CommandChain();

			_templateCommandChain.Next.Next.Next.Next.Next.Now = new DelFolderCommand();
			_templateCommandChain.Next.Next.Next.Next.Next.Next = new CommandChain();

			_templateCommandChain.Next.Next.Next.Next.Next.Next.Now = new CreateMemoCommand();
			_templateCommandChain.Next.Next.Next.Next.Next.Next.Next = new CommandChain();

			_templateCommandChain.Next.Next.Next.Next.Next.Next.Next.Now = new OpenMemoCommand();
			_templateCommandChain.Next.Next.Next.Next.Next.Next.Next.Next = new CommandChain();

			_templateCommandChain.Next.Next.Next.Next.Next.Next.Next.Next.Now = new ListFolderCommand();

			#endregion
		}
		#endregion

		class CommandChain
		{
			#region Constraction
			public CommandChain()
			{
				
			}
			#endregion

			public Command Now;
			public CommandChain Next;
		}

		private CommandChain _templateCommandChain = new CommandChain();

		public void FindCommand(string[] args, ref Command argCommand)
		{
			string strComparLeft = (args[0] + args[1] + "Command").ToUpper();
			string strComparRight = _templateCommandChain.Now.GetClassName().ToUpper();
			if (strComparLeft == strComparRight)
			{
				argCommand = _templateCommandChain.Now;
				return;
			}
			else
			{
				if (_templateCommandChain.Next == null)
				{
					argCommand = new NoneCommand();
					return;
				}
				else
				{
					_templateCommandChain = _templateCommandChain.Next;
					FindCommand(args, ref argCommand);
				}
			}
			return;
		}
	}

	class CommandExplainer
	{
		#region Construction
		private static CommandExplainer _instance;
		protected CommandExplainer()
		{

		}
		public static CommandExplainer Instance()
		{
			return _instance ?? (_instance = new CommandExplainer());
		}
		#endregion

		//Notice this method when command set is changed
		//return zero means no error, none zero means at least one error.
		//Command format xxx_xxx_(args)
		//Code 1: Wrong Command
		public int Explain(string argCommand,ref string[] explainResult)
		{
			int[] div = new int[2];
			int divNumber = 0;
			for (int temp = 0; temp < argCommand.Length; temp++)
			{
				if (argCommand.Substring(temp,1) == " ")
				{
					div[divNumber] = temp;
					divNumber++;
					if (divNumber == 2)
					{
						break;
					}
				}
			}
			if (divNumber !=2)
			{
				return 1;
			}
			else
			{
				explainResult[0] = argCommand.Substring(0, div[0]);
				explainResult[1] = argCommand.Substring(div[0] + 1, div[1] - div[0] - 1);
				explainResult[2] = argCommand.Substring(div[1] + 1);
				return 0;
			}
		}
	}

	class User : IConsoleInterfacePrint, IConsoleInterfaceScan
	{
		#region Construction
		private static User _instance;
		protected User()
		{
			PrintLine("-----------------------");
			PrintLine("|| Virtual DOS. V1.0 ||");
			PrintLine("-----------------------");
		}
		public static User Instance()
		{
			return _instance ?? (_instance = new User());
		}
		#endregion

		public void Print(string arg)
		{
			System.Console.Write(arg);
		}
		public void PrintLine(string arg)
		{
			System.Console.Write("    ");
			System.Console.WriteLine(arg);
		}
		public void ScanLine(ref string arg)
		{
			arg = Console.ReadLine();
		}

		public Folder NowLocation;

		public void UserInput()
		{
			Print(">>> ");
			string UserCommand = "";
			ScanLine(ref UserCommand);
			CommandExplainer aCommandExplainer = CommandExplainer.Instance();
			string[] ExplainResult=new string[3];
			if (aCommandExplainer.Explain(UserCommand, ref ExplainResult) == 0)
			{
				CommandExecuter aCommandExecuter = new CommandExecuter();
				Command tempCommand = new Command();
				aCommandExecuter.FindCommand(ExplainResult, ref tempCommand);
				if (tempCommand.GetClassName() == "NoneCommand")
				{
					PrintLine("Wrong Command");
				}
				else
				{
					tempCommand.Execute(ExplainResult, ref NowLocation);
				}
			}
			else
			{
				PrintLine("Wrong Command");
			}
			GC.Collect();
		}

		public void ComputerInput(string argCommand)
		{
			CommandExplainer aCommandExplainer = CommandExplainer.Instance();
			string[] ExplainResult = new string[3];
			if (aCommandExplainer.Explain(argCommand, ref ExplainResult) == 0)
			{
				CommandExecuter aCommandExecuter = new CommandExecuter();
				Command tempCommand = new Command();
				aCommandExecuter.FindCommand(ExplainResult, ref tempCommand);
				tempCommand.Execute(ExplainResult, ref NowLocation);
			}
			GC.Collect();
		}
	}
}
