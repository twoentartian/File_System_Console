using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Command Set:
1) create file [name,content]
2) create folder [name]
3) open file [name]
4) open folder [name]/[..]
5) del file [name] 
6) del folder [name]
7) open memo [name]
8) create memo [name]
9) list folder self
	*/

namespace OPClassProgramDesignFinalExam
{
	class Program
	{
		static void Main(string[] args)
		{
			FileSystem BASEFileSystem = FileSystem.Instance();
			User aUser = User.Instance();
			aUser.NowLocation = BASEFileSystem.BASE;
			BASEFileSystem.BASE.Open();
			while (true)
			{
				aUser.UserInput();
			}
		}
	}

	#region Config
	interface IConsoleInterfacePrint
	{
		void PrintLine(string arg);
		void Print(string arg);
	}
	interface IConsoleInterfaceScan
	{
		void ScanLine(ref string arg);
	}
	class RunTimeParameter
	{
		public static string ExtensionName = ".vDOS";
	}
	#endregion 
}
