using UnityEngine;
using System.Collections;

public class ground_cube : MonoBehaviour
{
	public Vector3_Int cube_process_data;

	// debug info.
//	public bool ok;
//	public bool act;
//	public int delay;
//	public int power;
//	public Vector3 new_pos;

	void Update ()
	{
		cube_process_data.Tick();

		// debug info.
//		ok = cube_process_data.is_enable;
//		act = cube_process_data.is_active;
//		delay = cube_process_data.delay;
//		power = cube_process_data.power;
//		new_pos = cube_process_data.GetNewPoint();

		transform.position = cube_process_data.GetNewPoint();

		if (cube_process_data.IsEnable() == false) {
			GetComponent<Renderer>().material.color = Color.white;
			Destroy(this);
		}
	}
}
