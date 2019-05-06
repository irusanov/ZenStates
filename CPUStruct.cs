/*
 * Created by SharpDevelop.
 * User: Jon
 * Date: 2016-04-27
 * Time: 20:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AsusTC
{
	/// <summary>
	/// Description of CPUStruct.
	/// </summary>
	public struct CPUStruct : IEquatable<CPUStruct>
	{
		int member; // this is just an example member, replace it with your own struct members!
		
		#region Equals and GetHashCode implementation
		// The code in this region is useful if you want to use this structure in collections.
		// If you don't need it, you can just remove the region and the ": IEquatable<CPUStruct>" declaration.
		
		public override bool Equals(object obj)
		{
			if (obj is CPUStruct)
				return Equals((CPUStruct)obj); // use Equals method below
			else
				return false;
		}
		
		public bool Equals(CPUStruct other)
		{
			// add comparisions for all members here
			return this.member == other.member;
		}
		
		public override int GetHashCode()
		{
			// combine the hash codes of all members here (e.g. with XOR operator ^)
			return member.GetHashCode();
		}
		
		public static bool operator ==(CPUStruct left, CPUStruct right)
		{
			return left.Equals(right);
		}
		
		public static bool operator !=(CPUStruct left, CPUStruct right)
		{
			return !left.Equals(right);
		}
		#endregion
	}
}
