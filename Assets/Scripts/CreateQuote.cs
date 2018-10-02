using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateQuote : MonoBehaviour {

	public string apiKey;
	private RootService service;
	private RootService.LifeInsurance lifeInsurance;


	// Use this for initialization
	void Awake () {
		service = new RootService();
		// lifeInsurance = new RootService.LifeInsurance(1000000);
	}
	
	// Update is called once per frame
	public void GenerateQuote () {
		// service.CreateQuote(lifeInsurance.getParams());
		// service.CreateQuote();
	}
}
