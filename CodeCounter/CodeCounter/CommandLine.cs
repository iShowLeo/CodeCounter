/***
*     Title: 
*     Description:命令行解析器，根据命令参数执行相关任务
*     Date: 2018/06/10
*     Version:1.0
*     Author:Cwy
*     Modify Recoder:
*/

using System;
using System.IO;


namespace CodeCounter
{



    class CommandLine
    {
        string[] _args;

        FileType _fileType = FileType.none;
        SearchType _searchType = SearchType.none;
        string _path = "";   //输入路径 
        int _threadCount = 0;

        #region publicMethod
        public CommandLine(string[] args)
        {
            _args = args;
            _CommandFilter();
            _SetSearchType();
            //_SetFileType();
        }

        public void Run()
        {
            try
            {
                if (_searchType == SearchType.dir)
                {
                    FilesHelper.AllFilesMap(_path, _OnStart);
                }
                else if (_searchType == SearchType.file)
                {
                    _OnStart(_path);
                }
                else
                {
                    Console.WriteLine("参数路径不存在");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

       

        public FileType GetFileType()
        {
            return _fileType;
        }

        #endregion

        #region privateMethod
        /// <summary>
        /// 命令信息传入处理器并开始执行
        /// </summary>
        /// <param name="file"></param>
        void _OnStart(string file)
        {
            
            if (_JudgeFileType(file))
            { 
                CountHandler counter = new CountHandler(file);
                counter.CountInThread(_threadCount);
            }
        }

        /// <summary>
        /// 若未用命令参数指定类型，则根据后缀判定
        /// </summary>
        /// <param name="file"></param>
        bool _JudgeFileType(string file)
        { 
            if (file.EndsWith(".cpp") || file.EndsWith(".c"))
            {
                _fileType = FileType.cpp;
                _threadCount++;
                return true;
            }
            else
            {
                return false;
            }

        }

        void _CommandFilter()
        {
            if (_args.Length != 0)
            {
                _path = _args[0];
            }
            else
            {
                _path = "./";
            }
        }



        void _SetSearchType()
        {
            if (Directory.Exists(_path))
            {
                _searchType = SearchType.dir;
            }
            else if (File.Exists(_path))
            {
                _searchType = SearchType.file; 
            }

        }

        //void _SetFileType()
        //{
        //    if (_args.Length < 2) return;
        //    string typecmd = _args[1];
        //    if (typecmd == "cpp" || typecmd == "c")
        //    {
        //        _fileType = FileType.cpp;
        //    }
        //}


    }
    #endregion

    #region enum
    enum FileType
    {
        none,
        cpp
    }

    enum SearchType
    {
        none,
        dir,
        file
    }
    #endregion
}
