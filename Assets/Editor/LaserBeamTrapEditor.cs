using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LaserBeamTrap))]
[CanEditMultipleObjects]
public class LaserBeamTrapEditor : Editor
{
    SerializedProperty TInactiveProp;
	SerializedProperty TActiveProp;
	SerializedProperty LaserParticleSystem;
	SerializedProperty LengthProp;
	LaserBeamTrap myTarget;
    private void OnEnable()
    {
        TInactiveProp = serializedObject.FindProperty("TInactive");
		TActiveProp = serializedObject.FindProperty("TActive");
		LaserParticleSystem = serializedObject.FindProperty("laserParticleSys");
		LengthProp = serializedObject.FindProperty("length");

		myTarget = (LaserBeamTrap)target;
    }
    public override void OnInspectorGUI()
    {
		EditorGUILayout.PropertyField(TInactiveProp, new GUIContent("Time Inactive"));
		EditorGUILayout.PropertyField(TActiveProp, new GUIContent("Time Active"));
		EditorGUILayout.PropertyField(LaserParticleSystem, new GUIContent("Laser Particle System"));

		EditorGUILayout.PropertyField(LengthProp, new GUIContent("Length of Laser"));

		LengthProp.floatValue = Mathf.Clamp(LengthProp.floatValue , 1f, 50f);
		TInactiveProp.floatValue = Mathf.Clamp(TInactiveProp.floatValue, 0f, 20f);
		TActiveProp.floatValue = Mathf.Clamp(TActiveProp.floatValue, 0f, 20f);

		AdjustElemets();

		serializedObject.ApplyModifiedProperties ();
    }

	void AdjustElemets() {
		//right emittor position
		Transform tempTrans = myTarget.transform.Find("laser_emitter_right");
		Vector3 vec = tempTrans.localPosition;
		vec.x = LengthProp.floatValue;
		tempTrans.localPosition = vec;

		//laser_beam_length
		tempTrans = myTarget.transform.Find("laser_beam");
		vec = tempTrans.localScale;
		vec.x = LengthProp.floatValue;
		tempTrans.localScale = vec;

		//set up particle system !!!
		ParticleSystem ps = LaserParticleSystem.objectReferenceValue as ParticleSystem;
		tempTrans = ps.transform;
		vec = tempTrans.localPosition;
		vec.x = LengthProp.floatValue / 2.0f;
		tempTrans.localPosition = vec;
		ParticleSystem.ShapeModule sm = ps.shape;
		sm.radius = LengthProp.floatValue / 2.0f;
		ParticleSystem.EmissionModule em = ps.emission;
		em.rateOverTime = 7f * LengthProp.floatValue;
	}
}
