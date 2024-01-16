using System;
using System.IO;
using UnityEngine;

namespace NodeToolEditor.Utils
{
    public class NodeToolUtilities
    {
        /// <summary>
        /// Return the path for the node tool settings.
        /// </summary>
        public static string NodeToolSettingsPath
        {
            get
            {
                return GetNodeToolPathInternal();
            }
        }

        /// <summary>
        /// Open a directory at the specified path
        /// </summary>
        /// <param name="directoryPath">The path of the directory</param>
        public static void OpenDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Debug.LogErrorFormat("Error: Cannot found path at {0}. Please provide the correct path or create the directory.", directoryPath);
                return;
            }

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = directoryPath,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        private static string nodeToolPathInternal = Application.persistentDataPath + @"\Node Settings";
        private static string extensionName = ".nte";

        private static string GetNodeToolPathInternal()
        {
            if (CreateDirectory())
            {
                return nodeToolPathInternal;
            }

            return NodeToolSettingsPath;
        }

        private static bool CreateDirectory()
        {
            if (!Directory.Exists(nodeToolPathInternal))
            {
                Directory.CreateDirectory(nodeToolPathInternal);

                return true;
            }

            return true;
        }

        public static void SaveFile(string saveLocation, string text)
        {
            string newSavePath = saveLocation + extensionName;

            PrintError<IOException>("Node Tool Error: Failed to save nood tool settings at path " + newSavePath + ".", () =>
            {
                FileStream fileStream = new FileStream(newSavePath,
                                FileMode.OpenOrCreate,
                                FileAccess.ReadWrite,
                                FileShare.None);

                if (File.Exists(newSavePath))
                {
                    File.Create(newSavePath);
                }

                File.WriteAllText(newSavePath, text);
            });
        }

        public static string ReadFile()
        {
            return string.Empty;
        }

        /// <summary>
        /// Runs through the code through a try-catch statement to find an exeception
        /// </summary>
        /// <typeparam name="TExecption">The Exception to check the code for</typeparam>
        /// <param name="message">The error message to display</param>
        /// <param name="action">The code to run through</param>
        public static void PrintError<TExecption>(string message, Action action) where TExecption : Exception
        {
            try
            {
                action.Invoke();
            }
            catch (TExecption ex)
            {
                Debug.LogErrorFormat(message + "\n" + " Error Message: {0}", ex.Message);
            }
        }
    }
}


