using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ControlCoverAmount : MonoBehaviour {

	public int coverAmount;
	private UnityEngine.UI.Text textObject;
	private Rigidbody rigidbody;

	private float lastRotationY;
	private float lastVelocityY;

	private int MinCoverAmount = 10000000;

	public int increaseAmount = 1000000; 

	void Start() {
		
	}

	// Use this for initialization
	void Awake () {
		 textObject = GetComponentInChildren<UnityEngine.UI.Text>();	
		rigidbody = GetComponentInChildren<Rigidbody>();

		 textObject.text = "R " + coverAmount + "";

		 lastRotationY = transform.rotation.y;
		 lastVelocityY = rigidbody.velocity.y;

		 Debug.Log("Awake: tag ");
	}

	void Update() {
		
		updateOnRotate();
		updateOnYPosition();

		textObject.text = "R " + coverAmount + "";
	}
	
	public void GetQuote() {
		CreateQuote(coverAmount);
	}

	public void updateOnRotate() {
		float currentRotationY = transform.rotation.y;

		float deltaRotationY = lastRotationY - currentRotationY;

		// Debug.Log("deltaY:" + deltaRotationY);

		if (deltaRotationY > 0.005) {
			decrease();
			lastRotationY = currentRotationY;
		}

		if (deltaRotationY < -0.005) {
			increase();
			lastRotationY = currentRotationY;
		}
	}

	public void updateOnYPosition() {
		float currentVelocityY = rigidbody.velocity.y;

		// float deltaY = lastY - currentY;

		// Debug.Log("Y:" + deltaY);
		// Debug.Log("currentVelocityY:" + currentVelocityY);

		// if (deltaY < -0.05) {
		// 	lastY = currentY;
		// }
	}
	
	public void increase() {
		coverAmount += increaseAmount;
	}

	public void decrease() {
		coverAmount -= increaseAmount;

		if (coverAmount < MinCoverAmount) {
			coverAmount = MinCoverAmount;
		}
	}





	// root service

	/* Create a Root Object to store the returned json data in */
  [System.Serializable]
  public class Quotes
  {
    public Quote[] values;
  }

  public class LifeInsurance {
	public string type {get; set;}
	public int cover_amount {get; set;}
	public string cover_period {get; set;}
	public int basic_income_per_month {get; set;}
	public string education_status {get; set;}
	public bool smoker {get; set;}
	public string gender {get; set;}
	public int age {get; set;}

	public LifeInsurance(int cover_amount) {
		this.type = "root_term";
		this.cover_amount = cover_amount;
		this.cover_period = "1_year";
		basic_income_per_month = 3000000;
		education_status = "undergraduate_degree";
    	smoker = false;
    	gender = "male";
    	age = 30;
	}

	public List<Param> getParams() {
		return null;
		// List<Param> parameters = new List<Param>();
		// parameters.Add(new Param("type", type));
		// parameters.Add(new Param("cover_amount", cover_amount.ToString()));
		// parameters.Add(new Param("cover_period", cover_period));
		// parameters.Add(new Param("basic_income_per_month", basic_income_per_month.ToString()));
		// parameters.Add(new Param("education_status", education_status));
		// parameters.Add(new Param("smoker", smoker.ToString()));
		// parameters.Add(new Param("gender", gender));
		// parameters.Add(new Param("age", age.ToString()));
		
		// return parameters;
	}
  }

  [System.Serializable]
  public class Quote
  {
    public string package_name;
    public string sum_assured;
    public int base_premium;
    public string suggested_premium;
    public string created_at;
    public string quote_package_id;
    public QuoteModule module;
  }

  [System.Serializable]
  public class QuoteModule
  {
    public string type;
    public string make;
    public string model;
  }

  [Serializable]
  public class Param
  {
    public Param (string _key, string _value) {
      key = _key;
      value = _value;
    }
    public string key;
    public string value;
  }

  public string api_key = "sandbox_Y2FlNzMzZTYtZmM0Zi00NGY0LWIxZjctZjdmY2Q3YjE3N2VlLnV3NkQ1dHF4Q054ZGhhdEY3MnFkZ1MzM3NBYVJ4QzhU";
  public TextMesh textComponent;


  public void CreateQuote(int coverAmount) {
	List<Param> parameters = new List<Param>();
	parameters.Add(new Param("type", "root_term"));
	parameters.Add(new Param("cover_amount", coverAmount.ToString()));
	parameters.Add(new Param("cover_period", "1_year"));
	parameters.Add(new Param("basic_income_per_month", "3000000"));
	parameters.Add(new Param("education_status", "undergraduate_degree"));
	parameters.Add(new Param("smoker", GlobalVariables.smoker ? "true" : "false"));
	parameters.Add(new Param("gender", GlobalVariables.gender != null ? GlobalVariables.gender : "male"));
	parameters.Add(new Param("age", "30"));

    StartCoroutine(CallAPICoroutine("https://sandbox.root.co.za/v1/insurance/quotes", parameters));
  }

  IEnumerator CallAPICoroutine(String url, List<Param> parameters)
  {

    string auth = api_key + ":";
    auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
    auth = "Basic " + auth;

    WWWForm form = new WWWForm();

    foreach (var param in parameters) {
      form.AddField(param.key, param.value);
    }

    UnityWebRequest www = UnityWebRequest.Post(url, form);
    www.SetRequestHeader("AUTHORIZATION", auth);
    yield return www.Send();

    if (www.isNetworkError || www.isHttpError)
    {
      Debug.Log(www.downloadHandler.text);
    }
    else
    {
      Quotes json = JsonUtility.FromJson<Quotes>("{\"values\":" + www.downloadHandler.text + "}");

      String text = "R" + (json.values[0].base_premium / 100);
      Debug.Log("Form upload complete!");
      Debug.Log(text);
      textComponent.text = text;
    }
    yield return true;
  }
}
