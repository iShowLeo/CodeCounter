/***
*     Title:  
*     Description:Cpp计数器细节实现
*     Date: 2018/06/10
*     Version:1.0
*     Author:Cwy
*     Modify Recoder:
*/


namespace CodeCounter
{ 
    internal class CppCounter : CounterTemplate
    {
        LineMod linemod = new LineMod();
        public CppCounter(string[] _content) : base(_content)
        {
        }
         
        protected override void _Pipe(string str) {
            if (string.IsNullOrWhiteSpace(str))
            {
                isEmpty = true; 
            }
            else
            {
                linemod._line = str;
                linemod.Reset();
                linemod.StateStream();
                isEffective = linemod.isEffe;
                isInComment  = linemod.isComment; 
            }
        } 
         
    }
}
