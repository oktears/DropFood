using UnityEngine;
using System.Collections;
using System;

public interface CrcInterface 
{ 
	long Value 
	{ 
		get; 
	} 
	
	void Reset(); 
	
	void Crc(int bval); 
	
	void Crc(byte[] buffer); 
	
	void Crc(byte[] buf, int off, int len);
}