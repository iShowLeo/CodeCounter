/***
*     Title: 
*     Description:程序入口
*     Date: 2018/06/10
*     Version:1.0
*     Author:Cwy
*     Modify Recoder:
*/

using System; 
using System.Threading;


namespace CodeCounter
{
    class Entrace
    {
        static void Main(string[] args)
        { 
            CommandLine cmd = new CommandLine(args);
            cmd.Run();  
            Thread.Sleep(3000); 
        } 
    }
}
