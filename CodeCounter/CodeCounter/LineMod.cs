/***
*     Title: 
*     Description:代码行处理模型，逐行对代码进行状态记录、控制与反馈
*     Date: 2018/06/10
*     Version:1.0
*     Author:Cwy
*     Modify Recoder:
*/
using System;

namespace CodeCounter
{
    enum LineState
    {
        instring,
        singlecomment,
        multicomment,
        normal,
        empty
    }

    class LineMod
    {
        
        public string _line = "";
        public LineState _state = LineState.empty;
        public bool isEffe = false;                      //包含有效代码
        public bool isComment = false;                   //包含注释
        public bool isContinue = false;                  //续行

        char _lastchar = char.MinValue;
        public LineMod() { }


        public void StateStream()
        {
            for (int i = 0; i < _line.Length; i++)
            {
                SetState(i); 
              
            } 
        }

        public void Reset()
        {
            if (_state != LineState.multicomment &&
                !(isContinue && _state == LineState.singlecomment))
            {
                isComment = false;
                _state = LineState.empty;
            } 

            isEffe = false;
            isContinue = false;
        }


        /// <summary>
        /// 简易状态机，用于获取、控制代码行状态
        /// </summary>
        /// <param name="index"></param>
        void SetState(int index)
        {
            char ch = _line[index];
            

            if (ch.Equals('"'))
            {
                if (_state == LineState.instring)
                {
                    _state = LineState.normal; 
                }
                else if (_state == LineState.normal)
                {
                    _state = LineState.instring; 
                }
            }
            else if (ch.Equals('/') && _lastchar.Equals('/'))
            {
                if (_state == LineState.normal || _state == LineState.empty)
                {
                    _state = LineState.singlecomment;
                    isComment = true;
                }
            }
            else if (ch.Equals('*') && _lastchar.Equals('/'))
            {
                if (_state == LineState.normal || _state == LineState.empty)
                {
                    _state = LineState.multicomment;
                    isComment = true;
                }
            }
            else if (ch.Equals('/') && _lastchar.Equals('*'))
            {
                if (_state == LineState.multicomment)
                {
                    _state = LineState.empty; 
                }
            }
            else if (!ch.Equals(' ')&&!ch.Equals('/') &&!ch.Equals('*') && !ch.Equals('\\'))
            {
                if (_state == LineState.empty)
                {
                    _state = LineState.normal;
                    isEffe = true;
                }
            }
            if (index == _line.Length - 1 && ch == '\\')
            {
                isContinue = true;
            }

            _lastchar = ch;
        }

       
    }
}
