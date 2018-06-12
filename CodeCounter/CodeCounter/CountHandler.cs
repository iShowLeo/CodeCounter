/***
*     Title: 
*     Description:计数处理器，调度协调源码计数工作流
*     Date: 2018/06/10
*     Version:1.0
*     Author:Cwy
*     Modify Recoder:
*/

using System; 
using System.Threading;


namespace CodeCounter
{
    class CountHandler
    {
        string _file;  
        public CountHandler(string flie)
        {
            _file = flie; 
        }

       

        public void CountAndPrint(object threadContext)
        {
            try
            { 
                int thIndex = (int)threadContext;
                Console.WriteLine("thread {0}: start...", thIndex);
                CounterTemplate counter = _Factory();
                if (counter == null) {
                    Console.WriteLine("Create counter failed!");
                    return;
                } 

                counter.Count();
                counter.Print(_file); 
                Console.WriteLine("thread {0}: end...", thIndex);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
           
        }

        public void  CountInThread(int index) {
            try
            {
                ThreadPool.QueueUserWorkItem(CountAndPrint, index);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            } 
        }

        /// <summary>
        /// 根据命令选择计数器类型
        /// </summary>
        /// <returns></returns>
        CounterTemplate _Factory()
        {
            
            string[] lines = FilesHelper.LoadCodeSource(_file);
            CounterTemplate counter = null;
            if (_file.EndsWith(".cpp") || _file.EndsWith(".c"))
            {
                counter = new CppCounter(lines);
            }
            return counter;
        }
    }
}
