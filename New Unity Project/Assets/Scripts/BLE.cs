using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BLE : MonoBehaviour {

	public Button myselfButton;
	public string DeviceName = "Daydream controller";  // changed from "RFDuino"
	public string ServiceUUID = "FE55";
	public string SubscribeUUID = "0001";

	enum States {
		None,
		Scan,
		Connect,
		Subscribe,
		Subscribed,
		Disconnect,
	}

	private bool connected = false;
	private States state = States.None;
	private float timeout = 0f;
	private string deviceAddress;
	private bool foundSubscribeID = false;
	private byte[] dataBytes = null;
	private DataScript d;

	void Reset(){
		connected = false;
		timeout = 0f;
		state = States.None;
		deviceAddress = null;
		foundSubscribeID = false;
		dataBytes = null;
	}
		
	// start state machine 
	void StartBluetooth(){
		Reset (); //resets all instance variables
		Debug.Log("hello world");
		// set up interface as central 
		BluetoothLEHardwareInterface.Initialize (true, false, () => { 
			//begin scanning for peripherals
			Debug.Log("Initialize");
			setState(States.Scan, 0.1f);
		}, (error) => {
			BluetoothLEHardwareInterface.Log ("Error during initialize: " + error);
		});
	}

	// update state of state machine
	void setState(States newState, float newTimeout){
		state = newState;
		timeout = newTimeout;
	}

	// Use this for initialization
	void Start () {
		d = DataScript.getInstance();
		//myselfButton = GetComponent<Button>();
		myselfButton.onClick.AddListener(() => StartBluetooth());
	}


	//workflow: 
	//(1) scan for devices
	//(2) if device found, connect to device
	//(3) connect successful, scan for services
	//(4) service found, scan for characteristics
	//(5) got characteristics, get value for characteristic


	// Update is called once per frame
	void Update () {

		// do some stuff with the timeout so as to 
		// not do two things at once
		if (timeout > 0f) {
			timeout -= Time.deltaTime;
			if (timeout <= 0f) {
				timeout = 0f;
				Debug.Log("Timeout " + timeout);
				switch (state) {

				case States.None:
					Debug.Log("none");
					break;

				case States.Scan: //(1) scan for devices
					Debug.Log("scanning " + ServiceUUID);
				  // scan peripherals for peripherals with serviceUUID of serviceUUID = "2220"
					BluetoothLEHardwareInterface.ScanForPeripheralsWithServices (new []{ ServiceUUID }, (address, name) => {
						// callback with adress and name 
						Debug.Log ("addr" + address + "name" + name);
						// GET NAME OF RFDUINO 
						if (name.Contains (DeviceName)) {
							deviceAddress = address;
							// stop scan if we find the arduino
							BluetoothLEHardwareInterface.StopScan ();
							setState (States.Connect, 0.5f);
						}
					}, (address, name, rssi, bytes) => {
						//callback with address, name and advertising info 
						Debug.Log ("advertising info addr" + address + " name: " + name + " bytes " + bytes);
					});
					break;

				case States.Connect: // (2) if device found, connect to device
					foundSubscribeID = false;
					Debug.Log("connecting");
			// no callback for connect or service action, because each are enumerated and would have to carefully
			// set timeouts for all of them - ie: connect would call service actin for every service found,
			// every service found would call charactersitic action for every characteristic it has
			// ultimately, we only care about read characteristic 
					BluetoothLEHardwareInterface.ConnectToPeripheral (deviceAddress, null, null, (address, serviceUUID, characteristicUUID) => {
						// characterstic action
						Debug.Log("serviceUUID " + serviceUUID);
						Debug.Log("characteristicUUID " + characteristicUUID);
						foundSubscribeID = foundSubscribeID || IsEqual (characteristicUUID, SubscribeUUID);
						if (foundSubscribeID) {
							Debug.Log("connected");
							connected = true;
							setState (States.Subscribe, 2f);
						}
					});
					break;

				case States.Subscribe: //service found, subscribe to characteristics
				// which one ?! use subscribecharacteristic NOT readcharacteristic for if characteristic value will change 
				// which eventually it will because sending different packets of data

					//DataScript d = gameObject.GetComponent<DataScript> ();

					Debug.Log("subscribing");
					BluetoothLEHardwareInterface.SubscribeCharacteristic (deviceAddress, ServiceUUID, SubscribeUUID, (notification) => {
						//notificationAction
						Debug.Log ("notification action is " + notification);
					}, (characteristicUUID, bytes) => {
						//action
						Debug.Log("subscribed");
						string s = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
						Debug.Log ("value changed, data: " + s );
						bool dataNonZero = d.receiveData(s);
						Debug.Log(dataNonZero.ToString());
						dataBytes = bytes;
						setState(States.Subscribed, 10f);
					});
					break;

				case States.Subscribed:
					Debug.Log("subscribed");
					break;

				case States.Disconnect:
					BluetoothLEHardwareInterface.DisconnectPeripheral (deviceAddress, (address) => {
						BluetoothLEHardwareInterface.Log ("1");
						BluetoothLEHardwareInterface.DeInitialize (() => {

							connected = false;
							setState(States.None, 10f);
						});
					});
					break;
				}
			}
		}

	}
		


	private bool IsEqual(string uuid1, string uuid2)
	{
		if (uuid1.Length == 4)
			uuid1 = FullUUID (uuid1);
		if (uuid2.Length == 4)
			uuid2 = FullUUID (uuid2);

		return (uuid1.ToUpper().CompareTo(uuid2.ToUpper()) == 0);
	}

	private string FullUUID (string uuid)
	{
		return "0000" + uuid + "-0000-1000-8000-00805f9b34fb";
	}

	void Destroy(){
		myselfButton.onClick.RemoveListener (() => StartBluetooth ());
	}

	public void StopBluetooth() {
		setState(States.Disconnect, 1f);
	}

}