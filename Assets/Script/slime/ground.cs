using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Vector3_Int
{
	private const int gravity = 10;

	public bool is_active = false;
	public int x = 0;
	public int y = 0;
	public int z = 0;
	public int power = 0;
	public int delay = 0;

	public Vector3_Int(int in_x, int in_y, int in_z)
	{
		x = in_x;
		y = in_y * 100;
		z = in_z;
	}

	public void SetPower(int in_power, int in_delay)
	{
		delay = in_delay;
		power += in_power;
		is_active = true;
	}

	public void Tick()
	{
		if (!is_active) {
			return;
		}

		if (delay > 0)
		{
			--delay;
			return;
		}

		power -= gravity;
		y += power;
		if (y < 0) {
			y = 0;
			is_active = false;
		}
	}
		
	public bool IsActive()
	{
		return is_active;
	}

	public Vector3 GetNewPoint()
	{
		return new Vector3(x, y / 100.0f, z);
	}

}

public class ground : MonoBehaviour
{
	private const int ground_max_x = 49;
	private const int ground_max_z = 49;

	private GameObject[,] ground_array = new GameObject[ground_max_x, ground_max_z];

	private List<Vector3_Int> wave_points = new List<Vector3_Int>();

	private void Start ()
	{
		for (int z = 0; z < ground_max_z; ++z)
		{
			for (int x = 0; x < ground_max_x; ++x)
			{
				ground_array[x, z] = GameObject.CreatePrimitive(PrimitiveType.Cube);
				ground_array[x, z].transform.position = new Vector3(x, 0.0F, z);
				ground_array[x, z].transform.parent = this.transform;
				ground_array[x, z].name = string.Format("Cube({0},{1})", x, z);
			}
		}
//		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube)
//		cube.transform.position = new Vector3(0, 0.5F, 0);
	}

	private void CreateWaveCenter(int wave_x, int wave_y, int wave_z, int wave_power)
	{
		// renew cube pos.
		ground_array[wave_x, wave_z].transform.position =
			new Vector3(wave_x, wave_y, wave_z);
		// change cube color.
		ground_array[wave_x, wave_z].GetComponent<Renderer>().material.color = Color.yellow;
		// create new point data.
		Vector3_Int new_point = new Vector3_Int(
			wave_x,
			(int)ground_array[wave_x, wave_z].transform.position.y,
			wave_z
		);
		// add power on point.
		new_point.SetPower(wave_power, 0);
		// push point into queue.
		wave_points.Add(new_point);

		MakeWaves(wave_x, wave_z, wave_power);
	}

	private void MakeWaves(int wave_x, int wave_z, int wave_power)
	{
		//int attenuation_rate = ground_max_x;

		for (int z = 0; z < ground_max_z; ++z)
		{
			for (int x = 0; x < ground_max_x; ++x)
			{
				float dist = Vector3.Distance(
					ground_array[wave_x, wave_z].transform.position,
					ground_array[x, z].transform.position);
				//Debug.Log(string.Format("({0}, {1}) Dist = {2}", x, z, dist));

				ground_array[x, z].GetComponent<Renderer>().material.color = Color.blue;

				int lower_power = (int)(wave_power * ((ground_max_x - dist) / ground_max_x));
				int delay_power = (int)(dist * 5);

				//Debug.Log(string.Format("lower_power = {0}, delay_power = {1}", lower_power, delay_power));

				Vector3_Int new_point = new Vector3_Int(
					x,
					(int)ground_array[x, z].transform.position.y,
					z
				);
				new_point.SetPower(lower_power, delay_power);
				wave_points.Add(new_point);
			}
		}
	}

	private void Update ()
	{

		if (Input.GetKeyUp("w"))
		{
//			Debug.Log(Input.mousePosition);

			System.Random rnd = new System.Random(System.DateTime.UtcNow.Millisecond);

			int wave_start_x = rnd.Next(ground_max_x);
			int wave_start_z = rnd.Next(ground_max_z);
			int wave_power = 100;

			float hight_now = ground_array[wave_start_x, wave_start_z].transform.position.y;
			//hight_now += wave_power;

			CreateWaveCenter(wave_start_x, (int)hight_now, wave_start_z, wave_power);
		}

		if (wave_points.Count > 0)
		{
			foreach (Vector3_Int pt in wave_points) {
				pt.Tick();

				ground_array[pt.x, pt.z].transform.position = pt.GetNewPoint();

				if (!pt.IsActive()) {
					ground_array[pt.x, pt.z].GetComponent<Renderer>().material.color = Color.green;
//					wave_points.Remove(pt);
				}
			}

			wave_points.RemoveAll(item => item.IsActive() == false);

			//Vector3_Int point = wave_points.Peek();

//			point.y -= 5;
//			if (point.y < 0) {
//				point.y = 0;
//			}

//			ground_array[point.x, point.z].transform.position = point.GetNewPoint();
//			if (point.IsActive()) {
//				wave_points.Enqueue(point);
//			} else {
//				ground_array[point.x, point.z].GetComponent<Renderer>().material.color = Color.green;
//			}
//			wave_points.Dequeue();

			// point up
			////Vector3_Int upper_point = new Vector3_Int(point.x, point.y, point.z + 1);
//			int upper_z = point.z + 1;
//			if (upper_z >= 0 && upper_z < ground_max_z) {
//				ground_array[point.x, upper_z].transform.position
//				= new Vector3(point.x, point.y / 100.0f, upper_z);
//				if (point.y > 0) {
//					wave_points.Enqueue(new Vector3_Int(
//						point.x, point.y * 100, upper_z)
//					);
//				}
//			}
			// point down

			// point left
			// point right



			//Debug.Log(string.Format("wave_points count = {0}", wave_points.Count));
		}

	
	}

}
