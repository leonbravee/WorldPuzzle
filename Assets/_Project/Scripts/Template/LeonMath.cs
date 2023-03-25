using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace  LeonBrave
{
	public static class LeonsMath 
	{

		public static Vector3[] GetParabolaPoints(Vector3 a, Vector3 b, Vector3 c, int quality=100) {
			Vector3[] final = new Vector3[quality];

			Vector3 q;
			Vector3 r;
			Vector3 p;

			float interval = 1 / (float)quality;
			float t = 0;

			for (int i = 0; i < quality; i++) {
				t += interval;

				q = (1-t)*a + t*b;
				r = (1-t)*b + t*c;
				p = (1-t)*q + t*r;
				final [i] = p;
			}

			return final;
		}
		public static Vector3[] GetParabolaPoints(Vector3 a, Vector3 c,float height=1f ,int quality=100) {
			Vector3[] final = new Vector3[quality];

			Vector3 b = (a + c) / 2;
			b.y = height;
			Vector3 q;
			Vector3 r;
			Vector3 p;

			float interval = 1 / (float)quality;
			float t = 0;

			for (int i = 0; i < quality; i++) {
				t += interval;

				q = (1-t)*a + t*b;
				r = (1-t)*b + t*c;
				p = (1-t)*q + t*r;
				final [i] = p;
			}

			return final;
		}
		
		
		
		public static float InvoluteOfCircleX( float a, float theta )
		{
			return a * ( Mathf.Cos( theta ) + theta * Mathf.Sin( theta ) );
		}
		public static float InvoluteOfCircleY( float a, float theta )
		{
			return a * ( Mathf.Sin( theta ) - theta * Mathf.Cos( theta ) );
		}
	
		public static float InvoluteOfCircleZ( float b, float theta )
		{
			return b * theta;
		}

	
		//Spiral şekiller elde etmek için kullanılabilir
		public static Vector2 InvoluteOfCircle( float a, float theta )
		{
			return new Vector2(
				InvoluteOfCircleX( a, theta ),
				InvoluteOfCircleY( a, theta )
			);
		}
		
		//Spiral şekiller elde etmek için kullanılabilir
		public static Vector3 InvoluteOfCircle( float a, float b, float theta )
		{
			return new Vector3(
				InvoluteOfCircleX( a, theta ),
				InvoluteOfCircleY( a, theta ),
				InvoluteOfCircleZ( b, theta )
			);
		}
	}

	

}
