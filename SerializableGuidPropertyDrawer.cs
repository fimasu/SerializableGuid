# if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SerializableGuid))]
public class SerializableGuidPropertyDrawer : PropertyDrawer {
    private bool isShown = true;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) 
        => isShown ? base.GetPropertyHeight(property, label) + EditorGUIUtility.singleLineHeight * 4 
        : base.GetPropertyHeight(property, label);

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        // プロパティの描画を開始する
        EditorGUI.BeginProperty(position, label, property);
        // 折りたためるグループの始点
        isShown = EditorGUI.BeginFoldoutHeaderGroup(position, isShown, label);
        // 編集不能なグループの始点
        EditorGUI.BeginDisabledGroup(true);
        // SerializableGuidの構成要素のbyte[16]に移動する
        property.Next(true);
        // フィールドをインデントしないようにする
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        // 折りたたまれていなければbyte[16]を4x4で描画する
        if (isShown) {
            var anchorX = EditorGUIUtility.singleLineHeight;
            var anchorY = position.y + EditorGUIUtility.singleLineHeight;
            for (int offsetY = 0; offsetY < 4; offsetY++) {
                for (int offsetX = 0; offsetX < 4; offsetX++) {
                    var x = anchorX + offsetX * position.width / 4;
                    var y = anchorY + offsetY * 20;
                    var index = offsetY << 2 | offsetX;
                    EditorGUI.PropertyField(new Rect(x, y, position.width / 4, EditorGUIUtility.singleLineHeight), property.GetArrayElementAtIndex(index), GUIContent.none);
                }
            }
        }
        // インデントを元通りにする
        EditorGUI.indentLevel = indent;
        // 編集不能なグループの終点
        EditorGUI.EndDisabledGroup();
        // 折りたためるグループの終点
        EditorGUI.EndFoldoutHeaderGroup();
        // プロパティの描画を終了する
        EditorGUI.EndProperty();
    }
}
#endif