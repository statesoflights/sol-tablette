using UnityEngine;
using System.Collections;

public class particlesBlocker : MonoBehaviour {

	private ParticleSystem ps;
	private ParticleSystem.CollisionEvent[] collisionEvents;
	private ParticleSystem.Particle [] ParticleList;

	public int divisionIndex;

	void Start () 
	{
		divisionIndex = 2;
	}

	void OnParticleCollision(GameObject other) {
		if(other.tag!="Mirror")
		{
			// SETTINGS NECESSAIRES POUR EFFECTUER L'ANALYSE DES PARTICULES ET COLLISIONS
			ps = (ParticleSystem)GetComponent("ParticleSystem");
			collisionEvents = new ParticleSystem.CollisionEvent[16];
			ParticleList = new ParticleSystem.Particle[ps.particleCount];

			ps.GetParticles(ParticleList);
			
			int safeLength = particleSystem.safeCollisionEventSize;
			if (collisionEvents.Length < safeLength)
				collisionEvents = new ParticleSystem.CollisionEvent[safeLength];
			
			int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);
			int i = 0;

			while (i < numCollisionEvents) 
			{
				
				// COORDONNEES DE L'IMPACT
				Vector3 collisionHitLoc = collisionEvents[i].intersection;
				
				for(int j = 0; j < ParticleList.Length; j++)
				{
					// COORDONNEES DES PARTICULES
					Vector3 particleHitLoc =  transform.TransformPoint(ParticleList[j].position);
					
					var particleSize = ParticleList[j].size;
					
					// VERIFICATION SI UNE PARTICULE CORRESPOND AU POINT D'IMPACT (On vérifie aussi que cette coordonnée tiens compte du rayon de la particule. Si lors du traitement les particule sont trop petites on peux éventuellement affecter une autre valeur... ou supprimer la division pour s'appuyer alors sur le diamètre)
					if(Vector3.Distance(collisionHitLoc, particleHitLoc) <= particleSize/divisionIndex && Vector3.Distance(collisionHitLoc, particleHitLoc) >= -particleSize/divisionIndex)
					{	
						// ON DECLENCHE LA DESTRUCTION AUTOMATIQUE DES PARTICULES
						ParticleList[j].velocity=new Vector3(0, 0, 0);
						
						// LE TEMPS QUE LE COLLECTEUR FASSE SON OEUVRE ON REDUIT L'ECHELLE DES PARTICULES
						ParticleList[j].size=0;
					}		
				}
				ps.SetParticles(ParticleList, ps.particleCount);
				
				i++;
			}
		}
	}
}
