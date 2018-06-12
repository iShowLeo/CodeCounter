/***
*     Title: 
*     Description:计数器模板,由子类扩展实现计数细节。
*     Date: 2018/06/10
*     Version:1.0
*     Author:Cwy
*     Modify Recoder:
*/

using System; 

namespace CodeCounter
{
     
    abstract class CounterTemplate
    { 
        protected string[] _content;  
        protected bool isEffective;
        protected bool isInComment;
        protected bool isEmpty;
         
        int _commentCount=0;
        int _emptyCount=0;
        int _totalCount=0;
        int _effectiveCount=0; 


        protected CounterTemplate(string[] content)
        {
            _content = content;
        }

        /// <summary>
        /// 计数功能
        /// </summary>
        public void Count( )
        {
            try
            {
                _totalCount = _content.Length;
                for (int i = 0; i < _content.Length; i++)
                {
                    string line = _content[i];
                    _Pipe(line);

                    if (_IsEmpty()) _emptyCount++;
                    if (_IsComment()) _commentCount++;
                    if (_IsEffective()) _effectiveCount++;
                    //Console.WriteLine("第{0}行 Empty:{1} Effective:{2} Comment:{3}", i + 1, _emptyCount, _effectiveCount, _commentCount);
                    _TailSet(i);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
           
        }

        /// <summary>
        /// 打印计数功能
        /// </summary>
        /// <param name="file"></param>
        public void  Print(string file) {
            Console.WriteLine("file:{0} total:{1} empty:{2} effective:{3} comment:{4}",
                file, _totalCount, _emptyCount, _effectiveCount, _commentCount);
        }



        #region childMethod

        protected virtual void _Pipe(string str)
        {
        }

        protected virtual void _TailSet(int lineindex)
        {
            isEmpty = false;
            isEffective = false;
            isInComment = false;
        }

        protected virtual bool _IsEmpty( )
        {
            return isEmpty;
        }  

        protected virtual bool _IsComment( )
        {
            return isInComment && !isEmpty;
        }

        protected virtual bool _IsEffective( )
        {
            return isEffective;
        }
        #endregion
    }
}
