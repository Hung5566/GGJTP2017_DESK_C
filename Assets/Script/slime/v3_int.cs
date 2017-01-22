using UnityEngine;
using System.Collections;

public class Vector3_Int
{
	private const int gravity = 10;

	public bool is_enable = false; // is in process.
	public bool is_active = false; // is not delay.
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
		is_enable = true;
		is_active = false;
	}

	public void AddPower(int in_power, int in_delay)
	{
		if (delay <= in_delay) {
			// delay is smaller than next - do nothing.
		} else {
			// delay is greater than next - process.
			int delay_delta = in_delay - delay;
			for (int i = 0; i < delay_delta; ++i) {
				power -= gravity;
				y += power;
			}
		}

		is_enable = true;
		is_active = false;
	}

	public void Tick()
	{
		if (!is_enable) {
			return;
		}

		if (delay > 0) {
			--delay;
			return;
		} else {
			is_active = true; // no more delay.
		}

		power -= gravity;
		y += power;
		if (y <= 0) {
			y = 0;
			is_enable = false; // all stopped.
		}
	}

	public bool IsEnable()
	{
		return is_enable;
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
