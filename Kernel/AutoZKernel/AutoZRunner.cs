//////////////////
///Barton Joe
///V1012 Modify RAR command
///      Add "-s"
///For solid compress
///        Modify compress
///        Add "*" check
///Fix sender V1001 merge compress function
///        You can use this function if only need to compress all files under the strDrvRoot
///        to add "*" mark after the path(Windows Command's type)
//////////////////
using System;
using System.IO;
using System.Diagnostics;
using System.Collections;

namespace AutoZKernel
{
    public class AutoZRunner
    {
        public static void runBat(string strDir, string strFileName, string strArg)
        {
            if (!Directory.Exists(strDir) || !File.Exists(strDir + "\\" + strFileName)) return;
            Process proc = new Process();
            try
            {
                proc.StartInfo.WorkingDirectory = strDir;
                proc.StartInfo.FileName = strFileName;
                proc.StartInfo.Arguments = strArg;
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
        }
        public static void runBatWriteLog(string strDir, string strFileName, string strArg, string strLogPath, string strProgramName)
        {
            if (!Directory.Exists(strDir) || !File.Exists(strDir + "\\" + strFileName))
            {
                AutoZData.writeLog("Can't get directory or file [" + strDir + "] or [" + strDir + "\\" + strFileName + "]!", strLogPath, strProgramName);
                return;
            }
            Process proc = new Process();
            try
            {
                AutoZData.writeLog("Start batch process!", strLogPath, strProgramName);
                proc.StartInfo.WorkingDirectory = strDir;
                proc.StartInfo.FileName = strFileName;
                proc.StartInfo.Arguments = strArg;
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                AutoZData.writeLog(string.Format("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString()), strLogPath, strProgramName);
            }
            finally
            {
                proc.Close();
                AutoZData.writeLog("Batch process finished!", strLogPath, strProgramName);
            }
        }
        public static string runCMD(string cmd, bool bNeedWait)
        {
            Process p = new Process();
            string strRst = string.Empty;
            try
            {
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = false;
                p.Start();
                p.StandardInput.AutoFlush = true;
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.WriteLine("exit");
                strRst = p.StandardOutput.ReadToEnd();
                if (bNeedWait)
                {
                    p.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
            finally
            {
                p.Close();
            }
            return strRst;
        }
        public static bool closeProcess(string ProcName)
        {
            bool result = false;
            ArrayList procList = new ArrayList();
            string tempName = string.Empty;
            int begpos;
            int endpos;
            foreach (Process thisProc in Process.GetProcesses())
            {
                tempName = thisProc.ToString();
                begpos = tempName.IndexOf("(") + 1;
                endpos = tempName.IndexOf(")");
                tempName = tempName.Substring(begpos, endpos - begpos);
                procList.Add(tempName);
                if (tempName == ProcName)
                {
                    if (!thisProc.CloseMainWindow())
                        thisProc.Kill();
                    result = true;
                }
            }
            return result;
        }
        public static void unCompress(string strAddOnsRoot, string strFullName, string strDrvRoot)
        {
            string strCmd = strAddOnsRoot + "WinRAR\\WinRAR.exe x -y \"{0}\" \"{1}\"";
            strCmd = string.Format(strCmd, strFullName, strDrvRoot);
            runCMD(strCmd, true);
            closeProcess("cmd.exe");
        }
        public static void compress(string strPkgType, string strAddOnsRoot, string strDrvRoot, string strPkgName)
        {
            string strCmd = string.Empty;
            switch (strPkgType.ToUpper())
            {
                case "EXE":
//V1012
                    strCmd = strAddOnsRoot + "WinRAR\\WinRAR.exe a -ep1 -r -sfx -s \"{0}\" \"{1}\"";
//V1012
                    break;
                case "ZIP":
                    strCmd = strAddOnsRoot + "WinRAR\\WinRAR.exe a -ep1 -r \"{0}\" \"{1}\"";
                    break;
            }
            string strReleaseRoot = string.Empty;
//V1012
            if (strDrvRoot.Contains("*"))
            {
                strReleaseRoot = strDrvRoot;
            }
//V1012
            else
            {
                if (Directory.Exists(strDrvRoot))
                {
                    DirectoryInfo diRoot = new DirectoryInfo(strDrvRoot);
                    if (diRoot.GetDirectories().Length > 0)
                    {
                        strReleaseRoot = diRoot.GetDirectories()[0].FullName;
                    }
                }
            }
            strCmd = string.Format(strCmd, strPkgName, strReleaseRoot);
            runCMD(strCmd, true);
            closeProcess("cmd.exe");
        }
        public static void runScriptAu3(string strCmdHead, string[] strsArgs)
        {
            strCmdHead += " \"{0}\" \"{1}\" \"{2}\"";
            strCmdHead = string.Format(strCmdHead, strsArgs);
            runCMD(strCmdHead, false);
            closeProcess("cmd.exe");
        }
        public static void runScriptAu3withLog(string strCmdHead, string[] strsArgs, string strLogPath)
        {
            strCmdHead += " \"{0}\" \"{1}\" \"{2}\"";
            strCmdHead = string.Format(strCmdHead, strsArgs);
            AutoZData.writeLog("Command [" + strCmdHead + "]!!", strLogPath, "Kernel-runScriptAu3withLog");
            runCMD(strCmdHead, false);
            closeProcess("cmd.exe");
        }

        //UnUse V1012
        public static void runCommandsWithDDK(string strArg, string[] strsCommands)
        {
            Process process = new Process();
            try
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = strArg;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = false;
                process.Start();
                for (int i = 0; i < strsCommands.Length; i++)
                {
                    process.StandardInput.WriteLine(strsCommands[i]);
                }
                process.StandardInput.WriteLine("&&exit");
                process.StandardInput.AutoFlush = true;
                string strRst = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
            finally
            {
                process.Close();
            }
            //return strRst;
        }
    }
}
