﻿using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// Generic model 3d models have have only decorative uses.
/// </summary>
public class GenericModel3D : Model3D {


		public override int[] ModelTriangles (int meshNo)
		{
			switch (objInt().item_id)
			{
			case 336://Bench
				{
					return BenchTriangles().Reverse().ToArray();
				}
			case 345:
				{
					return BeamTriangles().Reverse().ToArray();	
				}
			case 361:
				{
					return ShelfTriangles().Reverse().ToArray();
				}
			}
			return base.ModelTriangles(meshNo);
		}

		public override Vector3[] ModelVertices ()
		{
				//		return new int[]{4,7,6,4,6,5,6,7,2,7,3,2,5,2,1,5,6,2,7,0,3,7,4,0,4,1,0,4,5,1,0,1,2,0,2,3,8,18,21,8,19,18,8,21,19,19,21,18,15,25,24,15,24,27,15,24,25,15,27,24,26,15,25,15,26,14,14,13,15,13,12,15,13,26,25,13,25,12,9,8,10,10,8,11,9,20,19,9,19,8,11,8,15,12,11,15,22,23,24,22,24,27,31,22,27,31,27,30,31,30,23,23,30,24,17,21,18,17,16,21,16,28,29,16,29,21,30,27,21,30,21,29,28,17,29,17,18,29,20,10,19,10,11,19}.Reverse().ToArray();
				switch (objInt().item_id)
				{
				case 336://Bench
						{
								return BenchVertices();
						}
				case 345://Beam
						{
								return BeamVertices();
						}
				case 361:
						{
								return ShelfVertices();
						}
				}
				return base.ModelVertices();
		}

		public override Color ModelColour (int meshNo)
		{
				return Color.grey;
		}



		Vector3[] BenchVertices()
		{
				Vector3[] ModelVerts = new Vector3[32];
				ModelVerts[0] = new Vector3(0.015625f,0.1875f,0.2890625f);
				ModelVerts[1] = new Vector3(0.015625f,0.1875f,-0.2890625f);
				ModelVerts[2] = new Vector3(-0.1757813f,0.1875f,-0.2890625f);
				ModelVerts[3] = new Vector3(-0.1757813f,0.1875f,0.2890625f);
				ModelVerts[4] = new Vector3(0.015625f,0.203125f,0.2890625f);
				ModelVerts[5] = new Vector3(0.015625f,0.203125f,-0.2890625f);
				ModelVerts[6] = new Vector3(-0.1757813f,0.203125f,-0.2890625f);
				ModelVerts[7] = new Vector3(-0.1757813f,0.203125f,0.2890625f);
				ModelVerts[8] = new Vector3(0f,0.1875f,0.2421875f);
				ModelVerts[9] = new Vector3(0.0078125f,0f,0.2421875f);
				ModelVerts[10] = new Vector3(0.0078125f,0f,0.2109375f);
				ModelVerts[11] = new Vector3(0f,0.1523438f,0.1757813f);
				ModelVerts[12] = new Vector3(0f,0.1523438f,-0.1757813f);
				ModelVerts[13] = new Vector3(0.0078125f,0f,-0.2109375f);
				ModelVerts[14] = new Vector3(0.0078125f,0f,-0.2421875f);
				ModelVerts[15] = new Vector3(0f,0.1875f,-0.2421875f);
				ModelVerts[16] = new Vector3(-0.1679688f,0f,0.2421875f);
				ModelVerts[17] = new Vector3(-0.1445313f,0f,0.2421875f);
				ModelVerts[18] = new Vector3(-0.125f,0.1523438f,0.2421875f);
				ModelVerts[19] = new Vector3(-0.03515625f,0.1523438f,0.2421875f);
				ModelVerts[20] = new Vector3(-0.015625f,0f,0.2421875f);
				ModelVerts[21] = new Vector3(-0.1601563f,0.1875f,0.2421875f);
				ModelVerts[22] = new Vector3(-0.1679688f,0f,-0.2421875f);
				ModelVerts[23] = new Vector3(-0.1445313f,0f,-0.2421875f);
				ModelVerts[24] = new Vector3(-0.125f,0.1523438f,-0.2421875f);
				ModelVerts[25] = new Vector3(-0.03515625f,0.1523438f,-0.2421875f);
				ModelVerts[26] = new Vector3(-0.015625f,0f,-0.2421875f);
				ModelVerts[27] = new Vector3(-0.1601563f,0.1875f,-0.2421875f);
				ModelVerts[28] = new Vector3(-0.1679688f,0f,0.2109375f);
				ModelVerts[29] = new Vector3(-0.1601563f,0.1523438f,0.1757813f);
				ModelVerts[30] = new Vector3(-0.1601563f,0.1523438f,-0.1757813f);
				ModelVerts[31] = new Vector3(-0.1679688f,0f,-0.2109375f);

				return ModelVerts;	
		}

		int[] BenchTriangles()
		{
				return new int[]{4,7,6,4,6,5,6,7,2,7,3,2,5,2,1,5,6,2,7,0,3,7,4,0,4,1,0,4,5,1,0,1,2,0,2,3,8,18,21,8,19,18,8,21,19,19,21,18,15,25,24,15,24,27,15,24,25,15,27,24,26,15,25,15,26,14,14,13,15,13,12,15,13,26,25,13,25,12,9,8,10,10,8,11,9,20,19,9,19,8,11,8,15,12,11,15,22,23,24,22,24,27,31,22,27,31,27,30,31,30,23,23,30,24,17,21,18,17,16,21,16,28,29,16,29,21,30,27,21,30,21,29,28,17,29,17,18,29,20,10,19,10,11,19};
		}

		Vector3[] BeamVertices()
		{
				Vector3[] ModelVerts = new Vector3[32];
				ModelVerts[0] = new Vector3(0f,0f,0f);
				ModelVerts[1] = new Vector3(0f,0.0625f,0f);
				ModelVerts[2] = new Vector3(0.0625f,0.0625f,0f);
				ModelVerts[3] = new Vector3(0.0625f,0f,0f);
				ModelVerts[4] = new Vector3(0f,0f,0.5f);
				ModelVerts[5] = new Vector3(0f,0.0625f,0.5f);
				ModelVerts[6] = new Vector3(0.0625f,0.0625f,0.5f);
				ModelVerts[7] = new Vector3(0.0625f,0f,0.5f);
				return ModelVerts;
		}

		int[] BeamTriangles()
		{
				return new int[]{2,1,0,3,2,0,5,6,7,4,5,7,1,5,4,0,1,4,2,6,5,1,2,5,6,2,3,7,6,3,7,3,0,4,7,0};
		}


		Vector3[] ShelfVertices()
		{
			Vector3[] ModelVerts = new Vector3[14];	
			ModelVerts[0] = new Vector3(-0.25f,0.1289063f,-0.125f);
			ModelVerts[1] = new Vector3(-0.25f,0.1601563f,-0.125f);
			ModelVerts[2] = new Vector3(0.25f,0.1601563f,-0.125f);
			ModelVerts[3] = new Vector3(0.25f,0.1289063f,-0.125f);
			ModelVerts[4] = new Vector3(-0.25f,0.1289063f,0.125f);
			ModelVerts[5] = new Vector3(-0.25f,0.1601563f,0.125f);
			ModelVerts[6] = new Vector3(0.25f,0.1601563f,0.125f);
			ModelVerts[7] = new Vector3(0.25f,0.1289063f,0.125f);
			ModelVerts[8] = new Vector3(0.1640625f,0.1289063f,0.125f);
			ModelVerts[9] = new Vector3(0.1640625f,0f,0.125f);
			ModelVerts[10] = new Vector3(0.1640625f,0.1289063f,-0.00390625f);
			ModelVerts[11] = new Vector3(-0.1640625f,0.1289063f,0.125f);
			ModelVerts[12] = new Vector3(-0.1640625f,0.1289063f,-0.00390625f);
			ModelVerts[13] = new Vector3(-0.1640625f,0f,0.125f);
			return ModelVerts;
		}

		int [] ShelfTriangles()
		{
			return new int[]{4,7,3,0,4,3,10,9,8,9,10,8,13,12,11,12,13,11,2,1,0,3,2,0,5,6,7,4,5,7,5,4,0,1,5,0,6,5,1,2,6,1,7,6,2,3,7,2};	
		}

}
