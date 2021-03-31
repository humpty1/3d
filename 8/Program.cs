using System.Threading;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using ut;
using Args;
using Logger;

#pragma warning disable 642 



namespace tst3d
{                         
    static class gbl
    {
        static public ArgFlg  hlpF ;
        static public ArgFlg  dbgF ;
//        static public ArgFlg  expF ;
        static public ArgFlg  vF ;    //  подписывать номера точек
        static public ArgFlg  lnF ;  // рисовать линии или точки
        static public ArgIntMM    logLvl ;
      //  static public ArgFloatMM  perCent ;
        static public ArgStr  flNm ;  // имя файла с точками
        static public LOGGER log;

        static  gbl (){
           hlpF   =  new ArgFlg(false, "?","help",    "to see this help");
           vF     =  new ArgFlg(false, "v",  "verbose", "additional info");
           lnF    =  new ArgFlg(false, "ln",  "line",    "line flag");
           dbgF   =  new ArgFlg(false, "d",  "debug",   "debug mode");
//           expF   =  new ArgFlg(false, "e",  "expFunc",   "new func");
           logLvl =  new ArgIntMM(1,      "l",  "log",   "log level", "LLL");
           logLvl.setMin(1);
           logLvl.setMax(8);

           //logLvl.show = false;
           flNm   =  new ArgStr("data3d.min1.txt", "f", "file", "data file", "FLNM");
         //  flNm.required = true;
/*
           perCent   =  new ArgFloatMM(0.05, "p", "percent", "percent for something",  "PPP");
           perCent.setMax(100.0);
*/      }
        static public  void usage(){
           Args.Arg.mkVHelp("to show functions of two arguments", "", vF
                ,hlpF
                ,dbgF
//                ,expF
                ,vF
                ,logLvl
                ,flNm
                );
                Environment.Exit(1);
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(	string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU", false);
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);

           for (int i = 0; i<args.Length; i++){
             if (hlpF.check(ref i, args))
               usage();
             else if (dbgF.check(ref i, args))
               ;
             else if (vF.check(ref i, args))
               ;
             else if (lnF.check(ref i, args))
               ;
             else if (logLvl.check(ref i, args))
               ;
             else if (flNm.check(ref i, args))
               ;
           }

           DateTime st = 	DateTime.Now;
           using (LOGGER Logger = new LOGGER(LOGGER.uitoLvl(logLvl))){
              log = Logger;
  						if (vF)
  						   Logger.cnslLvl = IMPORTANCELEVEL.Spam;

              Logger.WriteLine(IMPORTANCELEVEL.Stats
                  ,"I'l started  with {0} ",  (string)flNm
              );
              Logger.WriteLine("I'l started  "
              );
              Process proc = Process.GetCurrentProcess();

              long gTMst, pWSst, pVMst; 
              Graph3D x = new Graph3D("3d example", 10);

              string info = String.Format(
                "Total Mem/virMem/GC mem: {0}/{1}/{2} kBytes"
                    , (pWSst = proc.PeakWorkingSet64/1024)
                       , (pVMst = proc.PeakVirtualMemorySize64/1024)
                          , (gTMst = GC.GetTotalMemory(false)/1024));   //Retrieves the number of bytes currently thought to be allocated. 


              Logger.WriteLine(IMPORTANCELEVEL.Stats, info);

              Application.Run(x);
              
              proc = Process.GetCurrentProcess();
              info = String.Format(
                "Diff Mem/virMem/GC mem: {0}/{1}/{2} kBytes"
                    , (proc.PeakWorkingSet64/1024 - pWSst )
                       , (proc.PeakVirtualMemorySize64/1024 - pVMst)
                          , (GC.GetTotalMemory(false)/1024 - gTMst));   //Retrieves the number of bytes currently thought to be allocated. 


              Logger.WriteLine(IMPORTANCELEVEL.Stats, info);
              info = String.Format(
                "Total Mem/virMem/GC mem: {0}/{1}/{2} kBytes"
                    , (proc.PeakWorkingSet64/1024 )
                       , (proc.PeakVirtualMemorySize64/1024 )
                          , (GC.GetTotalMemory(false)/1024));   //Retrieves the number of bytes currently thought to be allocated. 


              Logger.WriteLine(IMPORTANCELEVEL.Spam, info);

              DateTime fn = DateTime.Now;
              Logger.WriteLine(IMPORTANCELEVEL.Stats, "time of work with file '{1}' is {0} secs"
                   , (fn - st).TotalSeconds, (string)flNm);

//              Thread.Sleep(1000);
           }
        }
}}                	