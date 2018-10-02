using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gender : DefaultTrackableEventHandler {

	protected override void OnTrackingFound() {
		base.OnTrackingFound();

		GlobalVariables.gender = this.tag;
	}


}
