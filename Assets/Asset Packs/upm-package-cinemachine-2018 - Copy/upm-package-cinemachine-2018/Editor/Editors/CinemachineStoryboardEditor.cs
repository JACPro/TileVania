using UnityEngine;
using UnityEditor;

namespace Cinemachine.Editor
{
    [InitializeOnLoad]
    internal class CinemachineStoryboardMute
    {
        const string StoryboardGlobalMuteMenuName = "Cinemachine/Storyboard Global Mute";
        [MenuItem(StoryboardGlobalMuteMenuName, false)]
        public static void StoryboardGlobalMute()
        {
            bool enable = !CinemachineStoryboardMute.Enabled;
            CinemachineStoryboardMute.Enabled = enable;
        }

        static CinemachineStoryboardMute()
        {
            CinemachineStoryboard.s_StoryboardGlobalMute = Enabled;

             /// Delaying until first editor tick so that the menu
             /// will be populated before setting check state, and
             /// re-apply correct action
             EditorApplication.delayCall += () => { Menu.SetChecked(StoryboardGlobalMuteMenuName, Enabled); };
        }

        public static string kEnabledKey = "StoryboardMute_Enabled";
        public static bool Enabled
        {
            get { return EditorPrefs.GetBool(kEnabledKey, false); }
            set
            {
                if (value != Enabled)
                {
                    EditorPrefs.SetBool(kEnabledKey, value);
                    CinemachineStoryboard.s_StoryboardGlobalMute = value;
                    Menu.SetChecked(StoryboardGlobalMuteMenuName, value);
                }
            }
        }
    }

    [CustomEditor(typeof(CinemachineStoryboard))]
    internal sealed class CinemachineStoryboardEditor : BaseEditor<CinemachineStoryboard>
    {
        public void OnDisable()
        {
            WaveformWindow.SetDefaultUpdateInterval();
        }

        const float FastWaveformUpdateInterval = 0.1f;
        float mLastSplitScreenEventTime = 0;

        public override void OnInspectorGUI()
        {
            float now = Time.realtimeSinceStartup;
            if (now - mLastSplitScreenEventTime > FastWaveformUpdateInterval * 5)
                WaveformWindow.SetDefaultUpdateInterval();

            BeginInspector();
            CinemachineStoryboardMute.Enabled
                = EditorGUILayout.Toggle(
                    new GUIContent(
                        "Storyboard Global Mute",
                        "If checked, all storyboards are globally muted."),
                    CinemachineStoryboardMute.Enabled);

            Rect rect = EditorGUILayout.GetControlRect(true);
            EditorGUI.BeginChangeCheck();
            {
                float width = rect.width;
                rect.width = EditorGUIUtility.labelWidth + rect.height;
                EditorGUI.PropertyField(rect, FindProperty(x => x.m_ShowImage));

                rect.x += rect.width; rect.width = width - rect.width;
                EditorGUI.PropertyField(rect, FindProperty(x => x.m_Image), GUIContent.none);

                EditorGUILayout.PropertyField(FindProperty(x => x.m_Aspect));
                EditorGUILayout.PropertyField(FindProperty(x => x.m_Alpha));
                EditorGUILayout.PropertyField(FindProperty(x => x.m_Center));
                EditorGUILayout.PropertyField(FindProperty(x => x.m_Rotation));

                rect = EditorGUILayout.GetControlRect(true);
                EditorGUI.LabelField(rect, "Scale");
                rect.x += EditorGUIUtility.labelWidth; rect.width -= EditorGUIUtility.labelWidth;
                rect.width /= 3;
                var prop = FindProperty(x => x.m_SyncScale);
                GUIContent syncLabel = new GUIContent("Sync", prop.tooltip);
                prop.boolValue = EditorGUI.ToggleLeft(rect, syncLabel, prop.boolValue);
                rect.x += rect.width;
                if (prop.boolValue)
                {
                    prop = FindProperty(x => x.m_Scale);
                    float[] values = new float[1] { prop.vector2Value.x };
                    EditorGUI.MultiFloatField(rect, new GUIContent[1] { new GUIContent("X") }, values);
                    prop.vector2Value = new Vector2(values[0], values[0]);
                }
                else
                {
                    rect.width *= 2;
                    EditorGUI.PropertyField(rect, FindProperty(x => x.m_Scale), GUIContent.none);
                }
                EditorGUILayout.PropertyField(FindProperty(x => x.m_MuteCamera));
            }
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(FindProperty(x => x.m_SplitView));
            if (EditorGUI.EndChangeCheck())
            {
                mLastSplitScreenEventTime = now;
                WaveformWindow.UpdateInterval = FastWaveformUpdateInterval;
                serializedObject.ApplyModifiedProperties();
            }
            rect = EditorGUILayout.GetControlRect(true);
            GUI.Label(new Rect(rect.x, rect.y, EditorGUIUtility.labelWidth, rect.height),
                "Waveform Monitor");
            rect.width -= EditorGUIUtility.labelWidth; rect.width /= 2;
            rect.x += EditorGUIUtility.labelWidth;
            if (GUI.Button(rect, "Open"))
                WaveformWindow.OpenWindow();
        }
    }
}
