using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
	public struct RangeInVertical
	{
		public float bottom;
		public float top;
		
		public RangeInVertical(float bottom, float top)
		{
			this.bottom = bottom;
			this.top = top;
		}
	}

	public struct RangeInHorizontal
	{
		public float left;
		public float right;
		
		public RangeInHorizontal(float left, float right)
		{
			this.left = left;
			this.right = right;
		}
	}

	public static class Range
	{
		static public bool OutOfRange(float value, RangeInVertical range)
		{
			return value < range.bottom || range.top < value;
		}
		
		static public bool InRange(float value, RangeInVertical range)
		{
			return range.bottom > value && value < range.top;
		}
		
		static public bool OutOfRange(float value, RangeInHorizontal range)
		{
			return value < range.left || range.right < value;
		}
		
		static public bool InRange(float value, RangeInHorizontal range)
		{
			return range.left > value && value < range.right;
		}
		
	}
}
