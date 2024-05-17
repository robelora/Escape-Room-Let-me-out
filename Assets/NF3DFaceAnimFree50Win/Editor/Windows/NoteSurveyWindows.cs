

using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.NFEditor
{
    class NoteSurveyWindows : EditorWindow
    {

        private const string MESSAGE_A =
   "\n\n   Help us make NaturalFront 3D animation better for you! \n\n   ";

        private const string Survey_Link = "https://forms.gle/tKxbEZhrRA5bLP4B7";

        public void OnGUI()
        {
            Color defaultTextColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;

            GUI.skin.button.normal.textColor = defaultTextColor;
            GUI.skin.button.onHover.textColor = defaultTextColor;
            GUI.skin.label.normal.textColor = defaultTextColor;
            GUI.skin.label.onNormal.textColor = defaultTextColor;
            GUI.skin.label.onHover.textColor = defaultTextColor;
            EditorStyles.radioButton.onFocused.textColor = defaultTextColor;
            EditorStyles.radioButton.onHover.textColor = defaultTextColor;
            EditorStyles.radioButton.onActive.textColor = defaultTextColor;
            EditorStyles.radioButton.onNormal.textColor = defaultTextColor;
            EditorStyles.radioButton.normal.textColor = defaultTextColor;

            DrawInstructions();

        }

        private void DrawInstructions()
        {
            try
            {
                GUI.TextArea(new Rect(5, 30, 380, 65), MESSAGE_A);

                GUIStyle txtStyle = new GUIStyle(GUI.skin.button);
                Color txtColor = Color.red;
                txtStyle.normal.textColor = txtColor;
                txtStyle.hover.textColor = txtColor;
                txtStyle.fontSize = 20;

                if (GUI.Button(new Rect(45, 170, 300, 60), "Complete a short survey", txtStyle))
                {
                    Application.OpenURL(Survey_Link);
                }

            }
            catch (UnityException exp)
            {
                Debug.LogError(exp.Message);
            }

        }

    }
}
