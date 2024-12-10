using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
	[SerializeField] GameObject ParticlesRef;
	ParticleSystem particle;
	private void Start()
	{
		PlayerMovement pm = GetComponent<PlayerMovement>();
		particle = ParticlesRef.GetComponent<ParticleSystem>();

		pm.groundedCheck.GroundCheckChanged += (s, e) =>
		{
			var em = particle.emission;
			em.rateOverDistance = e.CustomVariable ? 1 : 0;
		};
	}
}
