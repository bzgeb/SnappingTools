using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

[EditorTool("Snap Objects to Ground Tool")]
public class SnapObjectsToGroundTool : EditorTool
{
    [SerializeField] Texture2D ToolIcon;

    GUIContent _iconContent;
    GameObject _prefab;

    SerializedObject _serializedObject;
    [SerializeField] LayerMask GroundLayerMask;
    SerializedProperty _groundLayerMaskSerializedProperty;

    private void OnEnable()
    {
        _iconContent = new GUIContent()
        {
            image = ToolIcon,
            text = "Snap Objects to Ground Tool",
            tooltip = "Snap Objects to Ground Tool"
        };
        _serializedObject = new SerializedObject(this);
        _groundLayerMaskSerializedProperty = _serializedObject.FindProperty("m_GroundLayerMask");
    }

    public override GUIContent toolbarIcon => _iconContent;

    public override void OnToolGUI(EditorWindow window)
    {
        if (EditorTools.IsActiveTool(this))
        {
            GUILayout.Window(0, new Rect(10, 20, 350, 100),
                (id) =>
                {
                    if (_groundLayerMaskSerializedProperty != null)
                    {
                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(_groundLayerMaskSerializedProperty);
                        if (EditorGUI.EndChangeCheck())
                        {
                            _serializedObject.ApplyModifiedProperties();
                        }
                    }

                    if (GUILayout.Button("Snap Selection"))
                    {
                        SnapSelection();
                    }
                }, "Snap Selection Options");
        }
    }

    void SnapSelection()
    {
        foreach (var transform in Selection.transforms)
        {
            if (PlaceOnGround(transform.position, out Vector3 point, out _))
            {
                Undo.RecordObject(transform, "Snap transform");
                transform.position = Handles.inverseMatrix.MultiplyPoint(point);
            }
        }
    }

    bool PlaceOnGround(Vector3 transformPosition, out Vector3 position, out Vector3 normal)
    {
        Ray ray = new Ray(transformPosition, Vector3.down);
        RaycastHit hit;
        position = Vector3.zero;
        normal = Vector3.up;
        bool hitGround = false;
        if (Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity,
            GroundLayerMask, QueryTriggerInteraction.Ignore))
        {
            position = ray.GetPoint(hit.distance);
            normal = hit.normal;
            hitGround = true;
        }

        return hitGround;
    }
}