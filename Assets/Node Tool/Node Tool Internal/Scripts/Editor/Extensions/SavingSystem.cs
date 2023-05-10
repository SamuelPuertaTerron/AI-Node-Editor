using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace NodeTool
{
    public static class SavingSystem
    {
        private static string m_FolderLocation = Application.persistentDataPath + @"\AI Node Tool\";

        public static void Save(string fileName, string content)
        {
            if(DoesDirectoryExist())
            {
                if(!File.Exists(m_FolderLocation + fileName))
                {
                    File.Create(m_FolderLocation + fileName);      
                    WriteToFile(m_FolderLocation + fileName, content); 
                }
                else
                {
                    WriteToFile(m_FolderLocation + fileName, content); 
                }
            }
        }

        public static void Load(string fileName)
        {
            
        }

        private static void WriteToFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        private static bool DoesDirectoryExist()
        {
            if(Directory.Exists(m_FolderLocation))
            {
                return true;
            }
            else 
            {
                DirectoryInfo dirInfo = Directory.CreateDirectory(m_FolderLocation);
                dirInfo.Attributes = FileAttributes.Hidden;
                return true;
            }
        }
    }
}

