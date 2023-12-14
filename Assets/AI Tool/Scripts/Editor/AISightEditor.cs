using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AINodeToolEditor {
    using AINodeTool;

    [CustomEditor(typeof(Sight))]
    public class AISightEditor : Editor {
        private void OnSceneGUI() {
            //Draws a circle around using the sight raduis
            Sight sight = (Sight)target;
            Handles.color = Color.green;
            Handles.DrawWireArc(sight.transform.position, Vector3.up, Vector3.forward, 360, sight.Raduis);

            //Gets the angle where of the sight

            Vector3 viewAngleA = DirectionFromAngle(sight.transform.eulerAngles.y, -sight.Angle / 2);
            Vector3 viewAngleB = DirectionFromAngle(sight.transform.eulerAngles.y, sight.Angle / 2);

            //Draws two lines to display the angle
            Handles.DrawLine(sight.transform.position, sight.transform.position + viewAngleA * sight.Raduis);
            Handles.DrawLine(sight.transform.position, sight.transform.position + viewAngleB * sight.Raduis);
        }

        private Vector3 DirectionFromAngle(float eulerY, float angle) {
            angle += eulerY;
            return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
        }
    }
}
