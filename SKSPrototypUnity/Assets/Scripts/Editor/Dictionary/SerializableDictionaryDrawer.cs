#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SerializableDictionary<,>))]
public class SerializableDictionaryDrawer : PropertyDrawer
{
	public float padding = 3f; // Padding zwischen den Elementen
	public float valueWidthPercentage = 0.7f; // Prozentsatz der Breite f�r den Wert

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);

		// Zeichne den Label des Feldes
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		// Erhalte die Listenelemente f�r Schl�ssel und Werte
		var keysProperty = property.FindPropertyRelative("keys");
		var valuesProperty = property.FindPropertyRelative("values");
		var count = keysProperty.arraySize;

		// Berechne die H�he f�r ein einzelnes Zeilenfeld
		var lineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

		// Breite f�r den "Remove Element" Button
		var buttonWidth = lineHeight;

		// Breite f�r den Wert
		var valueWidth = (position.width - buttonWidth - padding * 2f) * valueWidthPercentage;

		// Breite f�r den Schl�ssel
		var keyWidth = position.width - valueWidth - buttonWidth - padding * 3f;

		// Position f�r den "Add Element" Button
		var addButtonRect = new Rect(position.x, position.y, position.width, lineHeight);

		// Zeichne den "Add Element" Button
		if (GUI.Button(addButtonRect, "Add Element"))
		{
			OnAddButtonPressed(keysProperty, valuesProperty);
		}

		// Berechne die H�he des gesamten Dictionary-Bereichs
		var dictionaryHeight = count * lineHeight;

		// Position f�r das erste Dictionary-Element
		var elementPosition = new Rect(position.x, position.y + lineHeight, position.width, lineHeight);

		// Zeichne jedes Schl�ssel-Wert-Paar
		for (int i = 0; i < count; i++)
		{
			// Erhalte die SerializedProperties f�r den aktuellen Schl�ssel und Wert
			var keyProperty = keysProperty.GetArrayElementAtIndex(i);
			var valueProperty = valuesProperty.GetArrayElementAtIndex(i);

			// Berechne die Rechtecke f�r den Schl�ssel und den "Remove Element" Button
			var keyRect = new Rect(elementPosition.x, elementPosition.y, keyWidth, EditorGUI.GetPropertyHeight(keyProperty));
			var buttonRect = new Rect(elementPosition.xMax - buttonWidth, elementPosition.y, buttonWidth, EditorGUI.GetPropertyHeight(keyProperty));

			// Zeichne die PropertyFields f�r den Schl�ssel
			CreateKey(keyRect, keyProperty, GUIContent.none, i);

			// Zeichne den "Remove Element" Button
			if (GUI.Button(buttonRect, "-"))
			{
				keysProperty.DeleteArrayElementAtIndex(i);
				valuesProperty.DeleteArrayElementAtIndex(i);
				break;
			}

			var valueHeight = EditorGUI.GetPropertyHeight(valueProperty, GUIContent.none, true);
			bool simple = true;

			// Maybe needs more cases
			switch (valueProperty.propertyType)
			{
				case SerializedPropertyType.Generic:
					simple = false;
					break;
				default:
					break;
			}


			if (valueProperty.isExpanded)
			{
				// Berechne das Rechteck f�r den Wert
				var valueRect = new Rect(elementPosition.x + padding, elementPosition.y + lineHeight, position.width, EditorGUI.GetPropertyHeight(valueProperty));
				// Zeichne die PropertyFields f�r den Wert
				EditorGUI.PropertyField(valueRect, valueProperty, new GUIContent(valueProperty.type.ToString()), true);
			}
			else if (!valueProperty.isExpanded && !simple)
			{
				// Berechne das Rechteck f�r den Wert
				var valueRect = new Rect(keyRect.xMax + padding, elementPosition.y, valueWidth, EditorGUI.GetPropertyHeight(valueProperty));
				// Zeichne die PropertyFields f�r den Wert
				EditorGUI.PropertyField(valueRect, valueProperty, new GUIContent(valueProperty.type.ToString()), true);
			}
			else
			{
				// Berechne das Rechteck f�r den Wert
				var valueRect = new Rect(keyRect.xMax + padding, elementPosition.y, valueWidth, EditorGUI.GetPropertyHeight(valueProperty));
				// Zeichne die PropertyFields f�r den Wert
				EditorGUI.PropertyField(valueRect, valueProperty, GUIContent.none, true);
			}



			// Aktualisiere die Position f�r das n�chste Dictionary-Element
			elementPosition.y += Mathf.Max(EditorGUI.GetPropertyHeight(keyProperty), EditorGUI.GetPropertyHeight(valueProperty)) + EditorGUIUtility.standardVerticalSpacing;
		}

		EditorGUI.EndProperty();
	}

	protected virtual void CreateKey(Rect position, SerializedProperty property, GUIContent label, int index)
	{
		EditorGUI.PropertyField(position, property, label);
	}

	protected virtual void OnAddButtonPressed(SerializedProperty keysProperty, SerializedProperty valuesProperty)
	{
		keysProperty.arraySize++;
		valuesProperty.arraySize++;

		// Fix for enums cant beleave this worked lol (Enums NEED a Unknown state on 0 so this workes)
		keysProperty.GetArrayElementAtIndex(keysProperty.arraySize - 1).enumValueFlag = 0;
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		var keysProperty = property.FindPropertyRelative("keys");
		int count = keysProperty.arraySize;

		// Berechne die H�he f�r ein einzelnes Zeilenfeld
		float lineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

		// H�he des "Add Element" Buttons und der Schl�ssel-Wert-Paare
		float totalHeight = (count + 1) * EditorGUIUtility.standardVerticalSpacing;
		totalHeight += EditorGUIUtility.singleLineHeight;

		// Ber�cksichtige die H�he der Werte, wenn sie mehrzeilig sind
		for (int i = 0; i < count; i++)
		{
			var valueProperty = property.FindPropertyRelative("values").GetArrayElementAtIndex(i);
			totalHeight += EditorGUI.GetPropertyHeight(valueProperty);
			totalHeight += EditorGUIUtility.standardVerticalSpacing / 2;
		}

		//totalHeight += EditorGUIUtility.standardVerticalSpacing / 2;

		return totalHeight;
	}
}
#endif