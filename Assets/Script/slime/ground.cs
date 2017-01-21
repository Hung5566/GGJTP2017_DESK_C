using UnityEngine;
using System.Collections;

using System.Collections.Generic; // for List<T>, Dictionary<T>.

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

	public void AddPower(int in_power, int in_delay)
	{
		delay = (delay + in_delay) / 2;
		power = (power + in_power) / 2;
		is_active = true;
	}

	public void Tick()
	{
		if (!is_active) {
			return;
		}

		if (delay > 0) {
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
    public GameObject sandCube;
    buildingReaction BuildingReaction;
	private const int ground_max_x = 33;
	private const int ground_max_z = 33;

	private GameObject[,] ground_array = new GameObject[ground_max_x, ground_max_z];

	private Dictionary<int, Vector3_Int> wave_points = new Dictionary<int, Vector3_Int>();
	private List<int> wave_points_removeable = new List<int>();

	private Dictionary<int, Vector3_Int> wave_points_ex = new Dictionary<int, Vector3_Int>();
	private List<int> wave_points_removeable_ex = new List<int>();

	private void Start ()
	{
        BuildingReaction = GameObject.Find("building").GetComponent<buildingReaction>();
		for (int z = 0; z < ground_max_z; ++z) {
			for (int x = 0; x < ground_max_x; ++x) {
				ground_array[x, z] = GameObject.CreatePrimitive(PrimitiveType.Cube);
				ground_array[x, z].transform.position = new Vector3(x, 0.0F, z);
				ground_array[x, z].transform.parent = this.transform;
				ground_array[x, z].name = string.Format("Cube({0},{1})", x, z);

                ground_array[x, z].AddComponent<Rigidbody>();
                ground_array[x, z].GetComponent<Rigidbody>().useGravity = false;
                ground_array[x, z].GetComponent<Rigidbody>().isKinematic = true;

                GameObject sobj = Instantiate(sandCube, ground_array[x, z].transform.position - new Vector3(0, 0.5f, 0), sandCube.transform.rotation) as GameObject;
                sobj.transform.parent = ground_array[x, z].transform;
                Destroy(ground_array[x, z].GetComponent<MeshFilter>());
                Destroy(ground_array[x, z].GetComponent<MeshRenderer>());

                if (x >= ground_max_x - 2 && x <= ground_max_x+2 && z >= ground_max_x - 2 && z <= ground_max_x + 2)
                {
                    BuildingReaction.locationStr.Add(ground_array[x, z]);
                }
			}
		}
//		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube)
//		cube.transform.position = new Vector3(0, 0.5F, 0);
	}

	private void CreateWaveCenter(int wave_x, int wave_y, int wave_z, int wave_power)
	{
		int key_id = wave_z * 1000 + wave_x;

		// renew cube pos.
		ground_array[wave_x, wave_z].transform.position =
			new Vector3(wave_x, wave_y, wave_z);
		// change cube color.
		////ground_array[wave_x, wave_z].GetComponent<Renderer>().material.color = Color.yellow;
		// create new point data.
		Vector3_Int new_point = new Vector3_Int(
			wave_x,
			(int)ground_array[wave_x, wave_z].transform.position.y,
			wave_z
		);
		// add power on point.
		new_point.SetPower(wave_power, 0);
		// push point into queue.
		if (wave_points.ContainsKey(key_id)) {
			wave_points[key_id] = new_point;
		} else {
			wave_points.Add(key_id, new_point);
		}

		MakeWaves(wave_x, wave_z, wave_power);
	}

	private void MakeWaves(int wave_x, int wave_z, int wave_power)
	{
		//int attenuation_rate = ground_max_x;
		int diffusion_rate = 2;

		for (int z = 0; z < ground_max_z; ++z) {
			for (int x = 0; x < ground_max_x; ++x) {

				int key_id = z * 1000 + x;

				if (wave_points.ContainsKey(key_id)) {
					// change active point.
					float dist = Vector3.Distance(
						ground_array[wave_x, wave_z].transform.position,
						ground_array[x, z].transform.position);

					////ground_array[x, z].GetComponent<Renderer>().material.color = Color.magenta;

					int lower_power = (int)(wave_power * ((ground_max_x - dist) / ground_max_x)); // TODO:
					int delay_power = (int)(dist * diffusion_rate);

					wave_points[key_id].AddPower(lower_power, delay_power);

				} else {
					// add active point.
					float dist = Vector3.Distance(
						ground_array[wave_x, wave_z].transform.position,
						ground_array[x, z].transform.position);
					//Debug.Log(string.Format("({0}, {1}) Dist = {2}", x, z, dist));

					////ground_array[x, z].GetComponent<Renderer>().material.color = Color.blue;

					int lower_power = (int)(wave_power * ((ground_max_x - dist) / ground_max_x)); // TODO:
					int delay_power = (int)(dist * diffusion_rate);

					//Debug.Log(string.Format("lower_power = {0}, delay_power = {1}", lower_power, delay_power));

					Vector3_Int new_point = new Vector3_Int(
						x,
						(int)ground_array[x, z].transform.position.y,
						z
					);
					new_point.SetPower(lower_power, delay_power);
					wave_points.Add(key_id, new_point);
				}
			}
		}
	}

	private void Input_Wave()
	{
		if (Input.GetKeyUp("w")) {
			System.Random rnd = new System.Random(System.DateTime.UtcNow.Millisecond);

			int wave_start_x = rnd.Next(ground_max_x);
			int wave_start_z = rnd.Next(ground_max_z);
			int wave_power = 100;

			float hight_now = ground_array[wave_start_x, wave_start_z].transform.position.y;
			//hight_now += wave_power;

			CreateWaveCenter(wave_start_x, (int)hight_now, wave_start_z, wave_power);
		}
	}

	private void Update_WavePoints()
	{
		if (wave_points.Count > 0) {

			foreach (KeyValuePair<int, Vector3_Int> pt in wave_points) {
				pt.Value.Tick();

				ground_array[pt.Value.x, pt.Value.z].transform.position = pt.Value.GetNewPoint();

				if (!pt.Value.IsActive()) {
					////ground_array[pt.Value.x, pt.Value.z].GetComponent<Renderer>().material.color = Color.green;
					wave_points_removeable.Add(pt.Key);
				}
			}

			foreach (int item_key in wave_points_removeable) {
				wave_points.Remove(item_key);
			}
			wave_points_removeable.Clear();

			// remove not active items.
			//wave_points.RemoveAll(item => item.IsActive() == false);

			Debug.Log(string.Format("wave_points count = {0}", wave_points.Count));
		}
	}

	private void CreateWaveCenterEx(int wave_x, int wave_y, int wave_z, int wave_power)
	{
		int key_id = wave_z * 1000 + wave_x;

		// renew cube pos.
		ground_array[wave_x, wave_z].transform.position =
			new Vector3(wave_x, wave_y, wave_z);
		// change cube color.
		////ground_array[wave_x, wave_z].GetComponent<Renderer>().material.color = Color.red;
		// create new point data.
		Vector3_Int new_point = new Vector3_Int(
			wave_x,
			(int)ground_array[wave_x, wave_z].transform.position.y,
			wave_z
		);
		// add power on point.
		new_point.SetPower(wave_power, 0);
		// push point into queue.
		if (wave_points_ex.ContainsKey(key_id)) {
			wave_points_ex[key_id] = new_point;
		} else {
			wave_points_ex.Add(key_id, new_point);
		}

		MakeWavesEx(wave_x, wave_z, wave_power);
	}

	private void MakeWavesEx(int wave_x, int wave_z, int wave_power)
	{
		//int attenuation_rate = ground_max_x;
		int diffusion_rate = 2;

		for (int z = 0; z < ground_max_z; ++z) {
			for (int x = 0; x < ground_max_x; ++x) {

				int key_id = z * 1000 + x;

				if (wave_points_ex.ContainsKey(key_id)) {
					// change active point.
					float dist = Vector3.Distance(
						ground_array[wave_x, wave_z].transform.position,
						ground_array[x, z].transform.position);

					////ground_array[x, z].GetComponent<Renderer>().material.color = Color.magenta;

					int lower_power = (int)(wave_power * ((ground_max_x - dist) / ground_max_x)); // TODO:
					int delay_power = (int)(dist * diffusion_rate);

					wave_points_ex[key_id].AddPower(lower_power, delay_power);

				} else {
					// add active point.
					float dist = Vector3.Distance(
						ground_array[wave_x, wave_z].transform.position,
						ground_array[x, z].transform.position);
					//Debug.Log(string.Format("({0}, {1}) Dist = {2}", x, z, dist));

					////ground_array[x, z].GetComponent<Renderer>().material.color = Color.blue;

					int lower_power = (int)(wave_power * ((ground_max_x - dist) / ground_max_x)); // TODO:
					int delay_power = (int)(dist * diffusion_rate);

					//Debug.Log(string.Format("lower_power = {0}, delay_power = {1}", lower_power, delay_power));

					Vector3_Int new_point = new Vector3_Int(
						x,
						(int)ground_array[x, z].transform.position.y,
						z
					);
					new_point.SetPower(lower_power, delay_power);
					wave_points_ex.Add(key_id, new_point);
				}
			}
		}
	}

	private void Input_Wave_Ex()
	{
		if (Input.GetKeyUp("o")) {
			System.Random rnd = new System.Random(System.DateTime.UtcNow.Millisecond);

			int wave_start_x = rnd.Next(ground_max_x);
			int wave_start_z = rnd.Next(ground_max_z);
			int wave_power = 100;

			float hight_now = ground_array[wave_start_x, wave_start_z].transform.position.y;
			//hight_now += wave_power;

			CreateWaveCenterEx(wave_start_x, (int)hight_now, wave_start_z, wave_power);
		}
	}

	private void Update_WavePointsEx()
	{
		if (wave_points_ex.Count > 0) {

			foreach (KeyValuePair<int, Vector3_Int> pt in wave_points_ex) {
				pt.Value.Tick();

				ground_array[pt.Value.x, pt.Value.z].transform.position = pt.Value.GetNewPoint();

				if (!pt.Value.IsActive()) {
					////ground_array[pt.Value.x, pt.Value.z].GetComponent<Renderer>().material.color = Color.green;
					wave_points_removeable_ex.Add(pt.Key);
				}
			}

			foreach (int item_key in wave_points_removeable_ex) {
				wave_points_ex.Remove(item_key);
			}
			wave_points_removeable_ex.Clear();

			// remove not active items.
			//wave_points.RemoveAll(item => item.IsActive() == false);

			Debug.Log(string.Format("wave_points_ex count = {0}", wave_points_ex.Count));
		}
	}

	private void Update ()
	{
		if (Input.GetKeyUp("t")) {
			System.Random rnd = new System.Random(System.DateTime.UtcNow.Millisecond);

			int wave_x = 33;//rnd.Next(ground_max_x);
			int wave_z = 33;//rnd.Next(ground_max_z);
			int wave_power = 100;

			float wave_y = ground_array[wave_x, wave_z].transform.position.y;

			ground_cube cube = ground_array[wave_x, wave_z].AddComponent<ground_cube>();

			// renew cube pos.
			ground_array[wave_x, wave_z].transform.position =
				new Vector3(wave_x, wave_y, wave_z);
			// change cube color.
			ground_array[wave_x, wave_z].GetComponent<Renderer>().material.color = Color.red;
			// create new point data.
			Vector3_Int new_point = new Vector3_Int(
				wave_x,
				(int)wave_y,
				wave_z
			);
			// add power on point.
			new_point.SetPower(wave_power, 0);

			cube.cube_process_data = new_point;

			int diffusion_rate = 2;

			for (int z = 0; z < ground_max_z; ++z) {
				for (int x = 0; x < ground_max_x; ++x) {

					// add active point.
					float dist = Vector3.Distance(
						ground_array[wave_x, wave_z].transform.position,
						ground_array[x, z].transform.position);
					//Debug.Log(string.Format("({0}, {1}) Dist = {2}", x, z, dist));

					ground_array[x, z].GetComponent<Renderer>().material.color = Color.gray;

					int lower_power = (int)(wave_power * ((ground_max_x - dist) / ground_max_x)); // TODO:
					int delay_power = (int)(dist * diffusion_rate);

					//Debug.Log(string.Format("lower_power = {0}, delay_power = {1}", lower_power, delay_power));

					Vector3_Int new_point_ex = new Vector3_Int(
						x,
						(int)ground_array[x, z].transform.position.y,
						z
					);
					new_point_ex.SetPower(lower_power, delay_power);

					ground_cube cube_ex = ground_array[x, z].AddComponent<ground_cube>();

					cube_ex.cube_process_data = new_point_ex;
				}
			}

		}

		Input_Wave();
		Input_Wave_Ex();

		Update_WavePoints();
		Update_WavePointsEx();

	}

}
