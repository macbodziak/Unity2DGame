using UnityEditor;
using UnityEngine;

// not inheriting from anything
public class SampleMenuItem
{
    [MenuItem("Tools/Selection Info")]
    public static void ShowInfo()
    {
        //Selection is a unity class that represents the current selection in the editor
        Debug.Log(Selection.objects.Length);
    }

    //this must match the signature above, plus true, to indicate that this is going to be a validator for this menuItem
    //the second argument denotes whether this method is a validator or not
    [MenuItem("Tools/Selection Info", true)]
	[MenuItem("Tools/Rotate selection %r", true)]
    [MenuItem("Tools/Clear selection", true)]
    public static bool ShowInfoValidator()
    {
        return Selection.objects.Length > 0;
    }

    [MenuItem("Tools/Clear selection")]
    public static void ClearSelection()
    {
        Selection.activeObject = null;
    }

    [MenuItem("Tools/Rotate selection %#r")]
    public static void RotateSelection()
    {
        // Undo.RecordObjects(Selection.objects, "rotating objects");
        foreach (Object ob in Selection.objects)
        {
            GameObject gob = ob as GameObject;
            if (gob != null)
            {
                Undo.RecordObject(gob.transform,"rotation of object");
                gob.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 359f));
            }
        }
    }
}