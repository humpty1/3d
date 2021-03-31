#pragma warning disable 642

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;
using Args;
using Logger;

namespace test
{
    class set{
        static public ArgFlg  hlpF ;
        static public ArgFlg  dbgF ;
        static public ArgFlg  vF ;
        static public ArgIntMM    logLvl ;
        static public ArgFloatMM  step ;
        static public ArgFloatMM  max ;

        static  set (){
           hlpF   =  new ArgFlg(false, "?","help",    "to see this help");
           vF     =  new ArgFlg(false, "v",  "verbose", "additional info");
           dbgF   =  new ArgFlg(false, "d",  "debug",   " step * pi /180 - to make radians");
           logLvl =  new ArgIntMM(1,      "l",  "log",   "log level", "LLL");
           logLvl.setMin(1);
           logLvl.setMax(8);


           step   =  new ArgFloatMM(2.0, "s", "step", "step degree. for calculation sin && cos",  "PPP");
           step.setMax(0.0001);
           step.setMax(10.0);
           max    =  new ArgFloatMM(90.0, "m", "max", "max degree ",  "mmm");
           max.setMax(0.0);
           max.setMax(720.0);

        }
        static public  void usage(){
           Args.Arg.mkVHelp("to make  uments", "...", vF

                ,hlpF
                ,dbgF
                ,vF
                ,logLvl
                ,step
                ,max
                );
        }
    }




    class Program
    {

/*        static  Program(){
          var format = new System.Globalization.NumberFormatInfo();
          format.NumberDecimalSeparator = ".";
        }  */

        [STAThread]
        static void Main(string[] args)
        {



           for (int i = 0; i<args.Length; i++){
             if (set.hlpF.check(ref i, args))
               set.usage();
             else if (set.dbgF.check(ref i, args))
               ;
             else if (set.vF.check(ref i, args))
               ;
             else if (set.logLvl.check(ref i, args))
               ;
             else if (set.step.check(ref i, args))
               ;
             else if (set.max.check(ref i, args))
               ;
           }

 /*
           double  c  =   double.Parse("0.5", 
           
                        (NumberStyles.AllowCurrencySymbol |
              NumberStyles.AllowExponent  |
              NumberStyles.AllowThousands |
              NumberStyles.AllowDecimalPoint |
              NumberStyles.AllowLeadingSign  |
              NumberStyles.AllowLeadingWhite |
              NumberStyles.AllowTrailingWhite
             ));
*/           
          // System.Globalization.NumberStyles.AllowDecimalPoint);


           DateTime st = DateTime.Now;
           using (LOGGER Logger = new LOGGER(LOGGER.uitoLvl(set.logLvl))){
  						if (set.vF)
  						   Logger.cnslLvl = IMPORTANCELEVEL.Stats;
  						 Logger.WriteLine(IMPORTANCELEVEL.Stats, "   x град/радиан:   sin(x)| cos(x) ");
  						for (double x = 0.0; x <= set.max; x+=set.step){
  						  if (set.dbgF)
  						   Logger.WriteLine(IMPORTANCELEVEL.Stats, "'{0}'/{1} :   {2}|{3}"
                   , x, x* Math.PI / 180, Math.Sin(x* Math.PI / 180), Math.Cos(x*  Math.PI / 180));
  						  else 
  						   Logger.WriteLine(IMPORTANCELEVEL.Stats, "'{0}':   {1}|{2}"
                   , x, Math.Sin(x), Math.Cos(x));

  						
  						
  						}
              DateTime fn = DateTime.Now;
              Logger.WriteLine(IMPORTANCELEVEL.Stats, "time of work with table is {0} secs"
                   , (fn - st).TotalSeconds);

           }
        }
    }                
}
