using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoker : DefaultTrackableEventHandler {

	protected override void OnTrackingFound() {
		base.OnTrackingFound();

		GlobalVariables.smoker = true;
	}

	protected override void OnTrackingLost() {
		base.OnTrackingLost();

		GlobalVariables.smoker = false;
	}

}
