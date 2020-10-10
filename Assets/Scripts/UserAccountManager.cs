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
	public string loggedInSceneName = "Lobby";

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

		SceneManager.LoadScene (loggedInSceneName);
	}


	public void SendData(string data)
	{ //called when the 'Send Data' button on the data part is pressed
		if (IsLoggedIn)
		{
			//ready to send request
			StartCoroutine(sendSendDataRequest(LoggedIn_Username, LoggedIn_Password, data)); //calls function to send: send data request
		}
	}

	IEnumerator sendSendDataRequest(string username, string password, string data)
	{
		IEnumerator e = DCF.SetUserData(username, password, data); // << Send request to set the player's data string. Provides the username, password and new data string
		while (e.MoveNext())
		{
			yield return e.Current;
		}
	}

	public void GetData(OnDataReceivedCallback onDataReceived)
	{ //called when the 'Get Data' button on the data part is pressed

		if (IsLoggedIn)
		{
			//ready to send request
			StartCoroutine(sendGetDataRequest(LoggedIn_Username, LoggedIn_Password, onDataReceived)); //calls function to send get data request
		}
	}

	IEnumerator sendGetDataRequest(string username, string password, OnDataReceivedCallback onDataReceived)
	{
		string data = "ERROR";

		IEnumerator e = DCF.GetUserData(username, password); // << Send request to get the player's data string. Provides the username and password
		while (e.MoveNext())
		{
			yield return e.Current;
		}
		data = e.Current as string; // << The returned string from the request

		if (onDataReceived != null)
			onDataReceived.Invoke(data);
	}
}
