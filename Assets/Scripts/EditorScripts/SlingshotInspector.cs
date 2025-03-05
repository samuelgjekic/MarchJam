using UnityEditor;

public class SlingshotInspector : Editor
{
    [CustomEditor(typeof(Slingshot))]
     public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Description:", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("This controls the slingshot properties. Remember to add an AnchorPoint to the Slingshot object, which will be used for checking where the player starts dragging.", MessageType.Info);

        DrawDefaultInspector(); // Draws the rest of the normal inspector
    }
}
