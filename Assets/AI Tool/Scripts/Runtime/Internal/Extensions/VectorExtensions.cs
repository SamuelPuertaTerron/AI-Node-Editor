using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class VectorExtensions {
    public static Vector2 GetCloseVectorFrom(this Vector2 vector, Vector2[] otherVectors) {
        if (otherVectors.Length == 0) {
            throw new System.Exception("The list of other vectors is Empty");
        }
        float minDistance = Vector2.Distance(vector, otherVectors[0]);
        Vector2 minVector = otherVectors[0];

        for (int i = otherVectors.Length - 1; i > 0; i++) {
            float newDistance = Vector2.Distance(vector, otherVectors[i]);
            if (newDistance < minDistance) {
                minDistance = newDistance;
                minVector = otherVectors[i];
            }
        }

        return minVector;
    }

    public static Vector3 GetCloseVectorFrom(this Vector3 vector, Vector3[] otherVectors) {
        if (otherVectors.Length == 0) {
            throw new System.Exception("The list of other vectors is Empty");
        }
        float minDistance = Vector3.Distance(vector, otherVectors[0]);
        Vector3 minVector = otherVectors[0];

        for (int i = otherVectors.Length - 1; i > 0; i++) {
            float newDistance = Vector3.Distance(vector, otherVectors[i]);
            if (newDistance < minDistance) {
                minDistance = newDistance;
                minVector = otherVectors[i];
            }
        }

        return minVector;
    }

    //----------------------
    // Random Vector Elements
    //---------------------

    public static Vector2 RandomVector2(this Vector2 vector) {
        return vector = new Vector2(Random.Range(0, 100),
                                    Random.Range(0, 100));
    }

    public static Vector2 RandomVector2(this Vector2 vector, float range) {
        return vector = new Vector2(Random.Range(-range, range),
                           Random.Range(-range, range));
    }

    public static Vector2 RandomVector2(this Vector2 vector, Vector2 min, Vector2 max) {
        return vector = new Vector2(Random.Range(min.x, max.x),
                                    Random.Range(min.y, max.y));
    }

    public static Vector3 RandomVector3(this Vector3 vector) {
        return vector = new Vector3(Random.Range(0, 100),
                                    Random.Range(0, 100),
                                    Random.Range(0, 100));
    }

    public static Vector3 RandomVector3(this Vector3 vector, float range) {
        return vector = new Vector3(Random.Range(-range, range),
                                    Random.Range(-range, range),
                                    Random.Range(-range, range));
    }

    public static Vector3 RandomVector3(this Vector3 vector, Vector3 min, Vector3 max) {
        return vector = new Vector3(Random.Range(min.x, max.x),
                                    Random.Range(min.y, max.y),
                                    Random.Range(min.z, max.z));
    }
}



