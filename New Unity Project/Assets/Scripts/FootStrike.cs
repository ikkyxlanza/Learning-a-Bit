using UnityEngine;
using System.Collections;

public class FootStrike {

	private ArrayList data = new ArrayList();
	private double[] mean;
	private int numD;

	public FootStrike(int[] d){
		Debug.Log ("footstrike constructor");
		data.Add (d);
		//mean = (double)d;
		numD = 1;
	}

	public void addData(int[] d){
		data.Add (d);
		Debug.Log ("data added:" + d[0] + d[1] + d[2] + d[3]);

		// calculate new mean


	}

	public ArrayList getData(){
		return data;
	}

	public double[] getMean(){
		return mean;
	}

}