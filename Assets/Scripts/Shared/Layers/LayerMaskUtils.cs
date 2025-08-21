using System;
using UnityEngine;

public static class LayerMaskUtils
{ 

	public static bool IsInLayerInLayerMask(int layer, LayerMask layerMask)
	{
		return layerMask == (layerMask | (1 << layer)); 
	}
}

