/***
*     Title:  
*     Description:文件辅助接口
*     Date: 2018/06/10
*     Version:1.0
*     Author:Cwy
*     Modify Recoder:
*/

using System;
using System.IO;
using System.Text;
using Newtonsoft.Json; 
using System.Collections.Generic;



namespace CodeCounter
{
    class FilesHelper
    {

        /// <summary>
        /// 映射当前路径下包含子目录中文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        public static void AllFilesMap(string path ,Action<string> callback) {
            try
            {
                List<string> filesList = new List<string>();
                _ForeachAllFile(path, filesList);
                for (int i = 0; i < filesList.Count; i++)
                {
                    callback(filesList[i]);
                }  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); 
            } 
        }

        /// <summary>
        /// 映射当前路径下文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        public static void CurrentFilesMap(string path,Action<string> callback) {
            try
            {
                foreach (string item in Directory.EnumerateFiles(path))
                {
                    callback(item);
                }  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }


        /// <summary>
        /// 加载JSON配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T LoadJsonConfig<T>(string path) 
        {
            try
            {
                string jsonstr = File.ReadAllText(path, Encoding.UTF8);
                T config = JsonConvert.DeserializeObject<T>(jsonstr); 
                return config;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default(T); 
            } 
        }

        /// <summary>
        /// 根据路径加载源码列表
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] LoadCodeSource(string path)
        {
            try
            {
                string[] filelines = File.ReadAllLines(path, Encoding.UTF8); 
                return filelines;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            } 
        }

        /// <summary>
        /// 递归查找路径下所有文件相对路径，并加入列表
        /// </summary>
        /// <param name="path"></param>
        /// <param name="files"></param>
        static void _ForeachAllFile(string path, List<string> files) {
            if (!Directory.Exists(path)||files==null) return; 
            foreach (string item in Directory.EnumerateFileSystemEntries(path))
            {
                if (Directory.Exists(item))
                {
                    _ForeachAllFile(item, files);
                }
                if (File.Exists(item))
                {
                    files.Add(item);
                } 
            }
            return;
        }
    }
}
