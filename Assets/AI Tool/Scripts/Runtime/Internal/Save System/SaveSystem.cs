using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace AINodeToolInternal {
    public static class SaveSystem {

        public static void SaveData(object saveType) {
            JsonUtility.ToJson(saveType, false);
        }

        public static object LoadData<T>() {
            return JsonUtility.FromJson<T>(FileManager.GetPath() + "AI Node Tool/SaveData.nt");
        }
    }
}

