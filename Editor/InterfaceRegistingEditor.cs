using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(InterfaceRegister<>))]
public class InterfaceRegistingEditor : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{

		//���̃X�N���v�g���Ď�����ϐ�
		var targetProp = property.FindPropertyRelative("go");

		string targetTypeName = property.FindPropertyRelative("typeName").stringValue;




		//�ȑO�o�^����Ă����Q�[���I�u�W�F�N�g & ���ݓo�^����Ă���Q�[���I�u�W�F�N�g
		var oldObject = (GameObject)targetProp.objectReferenceValue;
		var newObject = (GameObject)EditorGUI.ObjectField(position, label, oldObject, typeof(GameObject),true);




		//���ݓo�^����Ă���V�[����null�ł���ꍇ�A�V�[�����Ƃ��� "" ��o�^
		if (newObject == null)
		{
			targetProp.objectReferenceValue = null;
		}
		else if (newObject != oldObject)
		{
			//newScene��null�ł͂Ȃ��A����
			//�ȑO�̂��̂ƌ��݂̂��̂��قȂ�ꍇ
			//�܂�A�ʂ�SceneAsset�ɒu��������ꂽ�ꍇ

			//�h���b�v���ꂽGameObject���C���^�[�t�F�[�X�������Ă��邩�m�F
			bool existsInBuildSettings = newObject.GetComponents<MonoBehaviour>().Any(mono => mono.GetType().GetInterface(targetTypeName) != null);

			//�����Ă���ꍇ�X�V
			if (existsInBuildSettings) targetProp.objectReferenceValue = newObject;
			else
			{
				//�܂܂�Ă��Ȃ��ꍇ�A���̎|�����O�ɏo��
				Debug.LogError($"<b>�K�v�ȃC���^�[�t�F�[�X����������Ă��܂���</b>\n {targetTypeName} �����������R���|�[�l���g�����Q�[���I�u�W�F�N�g��o�^���Ă�������");
			}
		}
	}

}
