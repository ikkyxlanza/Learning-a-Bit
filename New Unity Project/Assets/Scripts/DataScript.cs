using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DataScript {


	private static DataScript instance;
	bool zeros = true;
	int currStrike;
	public List<FootStrike> feetArr;


	private DataScript(){
		zeros = true;
		currStrike = 0;
		feetArr = new List<FootStrike> ();
	}

	public static DataScript getInstance(){
		if(instance == null){
			instance = new DataScript();
		}

		return instance;
	}
		
	public bool receiveData(string datastr) {
		//parse data
		char[] delim = {':'};
		string[] dataArr = datastr.Split (delim);
		int[] nums = new int[5];
		int zerocnt = 0;

		for(int i=0; i<dataArr.Length; i++) {
			int x = 0;
			if ( Int32.TryParse(dataArr[i], out x) ) {
				if (x == 0) zerocnt++;
			}
			nums[i] = x;
			
		}

		Debug.Log ("num1: " + nums[0] + " num2:" + nums[1] + " 3: " + nums[2] + " 4: " + nums[3] + " zerocnt: " + zerocnt);

		if (zerocnt == 5){
			if(!zeros){
				zeros = true;
				currStrike++;
			}
			return false;
				
		} else {
			// if last one was zeros, NEW FOOTSTRIKE
			// Debug.Log("ELSE STATEMENT");

			if(this.zeros){
				Debug.Log ("new footstrike");
				FootStrike newF = new FootStrike (nums);
				feetArr.Add(newF);
				zeros = false;
			} else {
				Debug.Log ("no new footstrike");
				feetArr[currStrike].addData(nums);
			}
			return true;
				
		}

		//decide what to do with it
		// if 5 zero's 
			// if have had zeros for a while, just scrap it
			// if first zeros, set zeros flag, increment pointer, scrap it

	}
		
}

