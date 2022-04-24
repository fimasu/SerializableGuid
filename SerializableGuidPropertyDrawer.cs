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
        // �v���p�e�B�̕`����J�n����
        EditorGUI.BeginProperty(position, label, property);
        // �܂肽���߂�O���[�v�̎n�_
        isShown = EditorGUI.BeginFoldoutHeaderGroup(position, isShown, label);
        // �ҏW�s�\�ȃO���[�v�̎n�_
        EditorGUI.BeginDisabledGroup(true);
        // SerializableGuid�̍\���v�f��byte[16]�Ɉړ�����
        property.Next(true);
        // �t�B�[���h���C���f���g���Ȃ��悤�ɂ���
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        // �܂肽���܂�Ă��Ȃ����byte[16]��4x4�ŕ`�悷��
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
        // �C���f���g�����ʂ�ɂ���
        EditorGUI.indentLevel = indent;
        // �ҏW�s�\�ȃO���[�v�̏I�_
        EditorGUI.EndDisabledGroup();
        // �܂肽���߂�O���[�v�̏I�_
        EditorGUI.EndFoldoutHeaderGroup();
        // �v���p�e�B�̕`����I������
        EditorGUI.EndProperty();
    }
}
#endif