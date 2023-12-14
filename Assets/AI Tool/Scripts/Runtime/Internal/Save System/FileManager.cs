using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace AINodeToolInternal {
    public static class FileManager {

        public static string GetPath() {
            if (!Directory.Exists(m_InternalPath)) {
                CreateDirectory();
                return m_InternalPath;
            } else {
                return m_InternalPath;
            }
        }

        private static string m_InternalPath = Application.persistentDataPath + "/AI Node Tool/";

        public static void CreateDirectory() {
            if (!Directory.Exists(m_InternalPath)) {
                DirectoryInfo directoryInfo = Directory.CreateDirectory(m_InternalPath);
                directoryInfo.Attributes = FileAttributes.Hidden;
            }
        }
    }
}


