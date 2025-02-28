using UnityEngine;
using System.Collections;

public class FluidEmission : MonoBehaviour {
	public GameObject SuperiorVC;
	public GameObject InferiorVC;

	public GameObject Mitral;

	public GameObject RV1;
	public GameObject RV2;
	public GameObject RV3;
	public GameObject RV4;
	public GameObject RV5;
	public GameObject RV6;

	public GameObject Pulmonary;

	public GameObject Tricuspid;

	public GameObject LV1;
	public GameObject LV2;
	public GameObject LV3;

	public GameObject Aortic;

	public void OpenVC(){
		SuperiorVC.GetComponent<ParticleSystem> ().Play ();
		InferiorVC.GetComponent<ParticleSystem> ().Play ();
	}
	public void CloseVC(){
		SuperiorVC.GetComponent<ParticleSystem> ().Stop ();
		InferiorVC.GetComponent<ParticleSystem> ().Stop ();
	}



	public void MitralEmit(){
		Mitral.GetComponent<ParticleSystem> ().Play ();
	}
	public void MitralStop(){
		Mitral.GetComponent<ParticleSystem> ().Stop ();
	}

	public void StartRV1(){
		RV1.GetComponent<ParticleSystem> ().Play ();

	}
	public void StopRV1(){
		RV1.GetComponent<ParticleSystem> ().Stop();
	}
	public void StartRV2(){
		RV2.GetComponent<ParticleSystem> ().Play ();
	}
	public void StopRV2(){
		RV2.GetComponent<ParticleSystem> ().Stop();
	}
	public void StartRV3(){
		RV3.GetComponent<ParticleSystem> ().Play ();
	}
	public void StopRV3(){
		RV3.GetComponent<ParticleSystem> ().Stop();
	}
	public void StartRV4(){
		RV4.GetComponent<ParticleSystem> ().Play ();
	}
	public void StopRV4(){
		RV4.GetComponent<ParticleSystem> ().Stop();
	}
	public void StartRV5(){
		RV5.GetComponent<ParticleSystem> ().Play ();
	}
	public void StopRV5(){
		RV5.GetComponent<ParticleSystem> ().Stop();
	}
	public void StartRV6(){
		RV6.GetComponent<ParticleSystem> ().Play ();
	}
	public void StopRV6(){
		RV6.GetComponent<ParticleSystem> ().Stop();
	}
	public void StartPulmonary(){
		Pulmonary.GetComponent<ParticleSystem> ().Play ();
	}
	public void StopPulmonary(){
		Pulmonary.GetComponent<ParticleSystem> ().Stop();
	}

	public void TricuspidEmit(){
		Tricuspid.GetComponent<ParticleSystem> ().Play ();
	}
	public void TricuspidStop(){
		Tricuspid.GetComponent<ParticleSystem> ().Stop ();
	}


	public void StartLV1(){
		LV1.GetComponent<ParticleSystem> ().Play ();

	}
	public void StopLV1(){
		LV1.GetComponent<ParticleSystem> ().Stop();
	}

	public void StartLV2(){
		LV2.GetComponent<ParticleSystem> ().Play ();

	}
	public void StopLV2(){
		LV2.GetComponent<ParticleSystem> ().Stop();
	}

	public void StartLV3(){
		LV3.GetComponent<ParticleSystem> ().Play ();

	}
	public void StopLV3(){
		LV3.GetComponent<ParticleSystem> ().Stop();
	}

	public void StartAortic(){
		Aortic.GetComponent<ParticleSystem> ().Play ();
	}
	public void StopAortic(){
		Aortic.GetComponent<ParticleSystem> ().Stop();
	}
}
