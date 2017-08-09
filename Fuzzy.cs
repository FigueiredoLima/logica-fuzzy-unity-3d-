using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Fuzzy : MonoBehaviour {

	float fome;
	float trabalho;
	float sono;

	float intervaloTempo;

	public AnimationCurve faminto;
	public AnimationCurve normal;
	public AnimationCurve bemAlimentado;

	public AnimationCurve trabalhar;
	public AnimationCurve normalt;
	public AnimationCurve semtrabalhar;

	public AnimationCurve dormir;
	public AnimationCurve normald;
	public AnimationCurve semsono;



	float valorFaminto;
	float valorNormal;
	float valorBemAlim;

	float valortrabalhar;
	float valorNormart;
	float valorsemtrabalhar;

	float valordormir;
	float valornormald;
	float valorsemsono;




	//public Text txtFome;
	//public Text txttrabalho;
	//public Text txtdormir;

	public Scrollbar scfome;
	public Scrollbar sctrabalho;
	public Scrollbar scsono;

	//public Text txtFam;
	//public Text txtNorm;
	//public Text txtBAlim;

	public TextMesh txtStatus; 

	NavMeshAgent nav;

	// Use this for initialization
	void Start () {
		fome = 0.0f;
		trabalho = 0.0f;
		sono = 0.0f;
		intervaloTempo = 0.0f;
		nav = gameObject.GetComponent<NavMeshAgent> ();
		//Vá para a comida!!!

	}
	
	// Update is called once per frame
	void Update () {
		intervaloTempo += Time.deltaTime;
		if (intervaloTempo >= 1.0f) {
			fome += 5.0f;
			scfome.size -= 0.06f;

			trabalho += 3.0f;
			sctrabalho.size -= 0.037f;

			sono += 2.5f;
			scsono.size -= 0.03f;

			intervaloTempo = 0.0f;
		}


		//txtFome.text = "Fome: " + fome;
		//txttrabalho.text = "Trabalho: " + trabalho;
		//txtdormir.text = "Dormir: " + sono;

		AvaliarCurvas ();
		//ExibirValores ();
		Status ();

	}

	void AvaliarCurvas() {
		valorFaminto = faminto.Evaluate (fome);
		valorNormal = normal.Evaluate (fome);
		valorBemAlim = bemAlimentado.Evaluate (fome);

		valortrabalhar = trabalhar.Evaluate (trabalho);
		valorNormart = normalt.Evaluate (trabalho);
		valorsemtrabalhar = semtrabalhar.Evaluate (trabalho);

		valordormir = dormir.Evaluate (sono);
		valornormald = normald.Evaluate (sono);
		valorsemsono = semsono.Evaluate (sono);
	}

	//void ExibirValores()  {
	//	txtFam.text = "Faminto: " + valorFaminto;
	//	txtNorm.text = "Normal: " + valorNormal;
		//.text = "Bem Alimentado: " + valorBemAlim;
	//}
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.name == "Comida") {
			fome = 0;
			scfome.size += 1.0f;
		}
		if (col.gameObject.name == "cama") {
			sono = 0;
			scsono.size += 1.0f;
		}
		if (col.gameObject.name == "trabalho") {
			trabalho = 0;
			sctrabalho.size += 1.0f;
		}
	}
	void Status() {
		string status;

		if (valorFaminto > valortrabalhar && valordormir < valorFaminto) {
			status = "Estou FAMINTO!";
			nav.SetDestination (GameObject.Find ("Comida").transform.position);
			nav.speed = 5.0f;

		}else if (valortrabalhar > valordormir && valorFaminto < valortrabalhar){
			status = "Tenho que TRABALHAR...";
			nav.SetDestination (GameObject.Find ("trabalho").transform.position);
			nav.speed = 5.0f;
		}
		else if (valordormir > valorFaminto && valortrabalhar < valordormir) {
		   status = "Tenho que DORMIR...";
			nav.SetDestination (GameObject.Find ("cama").transform.position);
		    nav.speed = 5.0f;
		} else  {
			status = "Estou BEM!";
			nav.speed = 0.0f;
		}

		txtStatus.text = status;
	}

}
