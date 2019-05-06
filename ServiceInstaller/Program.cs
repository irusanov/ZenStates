/*
 * Created by SharpDevelop.
 * User: Jon
 * Date: 2016-05-12
 * Time: 10:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration.Install;

namespace ServiceInstaller
{
	class Program
	{
		public static void Main(string[] args)
		{
			// Check if install or uninstall
			string[] install_args;
			if(args.Length > 0 && args[0] == "/uninstall") {
				install_args = new string[]{ "/U", "AsusTCsrv.exe" };
				Console.WriteLine("Uninstalling AsusTCsrv ...");
			}
			else {
				install_args = new string[]{ "AsusTCsrv.exe" };
				Console.WriteLine("Installing AsusTCsrv ...");
			}
			
			ManagedInstallerClass.InstallHelper(install_args);
		}
	}
}