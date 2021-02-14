using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;
using WindowsInput;
using System.Diagnostics;
using System.Threading;
using System.Configuration;

namespace PGPDecryption
{
    class Program
    {
        static void Main(string[] args)
        {
            string pgp = ConfigurationManager.AppSettings["pgp"];
            string password = ConfigurationManager.AppSettings["password"];
            string processName = ConfigurationManager.AppSettings["processName"];
            string fileName = ConfigurationManager.AppSettings["fileName"];
            string sourcePath = ConfigurationManager.AppSettings["sourcePath"];
            string targetPath = ConfigurationManager.AppSettings["targetPath"];

            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);

            try
            {
                #region Deleting existing decrypted file
                try
                {
                    System.IO.File.Delete(sourceFile);
                }
                catch (Exception e)
                {
                    throw e;
                }
                #endregion

                InputSimulator sim = new InputSimulator();

                #region Opening EXE
                try
                {
                    Console.WriteLine("EXE FOUND!");
                    Process.Start(pgp);
                    Console.WriteLine("EXE OPENED!");
                }
                catch (Exception e)
                {
                    throw e;
                } 
                #endregion

                Process[] pname = Process.GetProcessesByName(processName);

                #region Entering Password
                try
                {
                    if (pname.Length > 0)
                    {
                        sim.Keyboard.Sleep(5000).TextEntry(password);
                        sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                    }
                    //Console.WriteLine("Password Entered!");
                }
                catch (Exception e)
                {
                    throw e;
                } 
                #endregion

                //Console.WriteLine("Checking for process!!!!");
                //Console.WriteLine(pname);

                #region Checking for Process end
                try
                {
                    while (pname.Length > 0)
                    {
                        //Console.WriteLine("INSIDE WHILE");
                        pname = null;
                        pname = Process.GetProcessesByName(processName);
                        if (pname.Length == 1)
                        {
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    //Console.WriteLine("Process Terminated!!!");
                }
                catch (Exception e)
                {
                    throw e;
                } 
                #endregion

                #region Specify Source and Destination
                try
                {
                    //Console.WriteLine("Copying Data!!!!");
                    System.IO.File.Copy(sourceFile, destFile, true);
                    //Test
                }
                catch (Exception e)
                {
                    throw e;
                } 
                #endregion

            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}
