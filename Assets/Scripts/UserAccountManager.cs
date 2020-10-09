using UnityEngine;
using System.Collections;
using DatabaseControl;
using UnityEngine.SceneManagement;

public class UserAccountManager : MonoBehaviour {

	public static UserAccountManager instance;

	void Awake ()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(this);
	}

	public static string LoggedIn_Username { get; protected set; } //stores username once logged in
	public static string LoggedIn_Password { get; protected set; } //stores password once logged in

	public static bool IsLoggedIn { get; protected set; }

	public string loggedOutSceneName = "Login";

	public delegate void OnDataReceivedCallback(string data);

	public void LogOut ()
	{
		LoggedIn_Username = "";
		LoggedIn_Password = "";

		IsLoggedIn = false;

		Debug.Log("User logged out!");

		SceneManager.LoadScene(loggedOutSceneName);
	}

	public void LogIn(string username, string password)
	{
		LoggedIn_Username = username;
        LoggedIn_Password = password;

		IsLoggedIn = true;
	}


	public void GetData(OnDataReceivedCallback onDataReceived)
	{ //called when the 'Get Data' button on the data part is pressed

		if (IsLoggedIn)
		{
			//ready to send request
			sendGetDataRequest(onDataReceived); //calls function to send get data request
		}
	}

	private void sendGetDataRequest(OnDataReceivedCallback onDataReceived)
	{
		string data = "ERROR";

		if (onDataReceived != null)
			onDataReceived.Invoke(data);
	}

}
