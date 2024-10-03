using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    public class EditorUtil
    {
        public static void AutoAssignForView(GameObject gameObject)
        {
            var autoAssignMap = new Dictionary<string, FieldInfo>();

            var view = gameObject.GetComponent<FigmaView>();
            if (view == null) return;

            foreach (var field in view.GetType()
                         .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var autoAssignByName = field.GetCustomAttribute<AutoAssignByNameAttribute>();
                if (autoAssignByName == null) continue;
                autoAssignMap[autoAssignByName.Name] = field;
            }

            var childrenTransform = gameObject.GetComponentsInChildren<Transform>();
            foreach (var (autoAssignName, field) in autoAssignMap)
            {
                var autoAssignNames = autoAssignName.Split("/");
                foreach (var childTransform in childrenTransform)
                {
                    if (autoAssignNames[0] != childTransform.gameObject.name) continue;

                    var foundTransform = childTransform.parent.Find(autoAssignName);
                    field.SetValue(view, foundTransform.GetComponent(field.FieldType));
                    break;
                }
            }
        }
    }
}