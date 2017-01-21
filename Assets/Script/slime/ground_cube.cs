using UnityEngine;
using System.Collections;

public class ground_cube : MonoBehaviour
{
	public Vector3_Int cube_process_data;

	public bool act;
	public int delay;
	public int power;

	void Update ()
	{
		cube_process_data.Tick();

		act = cube_process_data.is_active;
		delay = cube_process_data.delay;
		power = cube_process_data.power;

		transform.position = cube_process_data.GetNewPoint();

		if (cube_process_data.IsActive() == false) {
			GetComponent<Renderer>().material.color = Color.white;
			Destroy(this);
		}
	}
}
