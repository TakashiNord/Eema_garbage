/*
 * Created by SharpDevelop.
 * User: gal
 * Date: 07.06.2021
 * Time: 12:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;

namespace ScanIP
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class Class1
	{
		public Class1()
		{
		}
	}
	
public class IPSegment {

    private UInt32 _ip;
    private UInt32 _mask;

    public IPSegment(string ip, string mask) {
        _ip = ip.ParseIp();
        _mask = mask.ParseIp();
    }

    public UInt32 NumberOfHosts {
    	get { return (~_mask+1)-2; } // -2 add
    }

    public UInt32 NetworkAddress {
        get { return _ip & _mask; }
    }

    public UInt32 BroadcastAddress {
        get { return NetworkAddress + ~_mask; }
    }

    public UInt32 startIP {
    	get { return (_ip & _mask) + 1; }
    }    
    
    public UInt32 endIP {
    	get { return ( _ip | (~_mask) ) - 1; }
    }     
    
    // IEnumerable
    public IEnumerable <UInt32> Hosts(){
        for (var host = NetworkAddress+1; host < BroadcastAddress; host++) {
            yield return  host;
        }
    }
    
    
}

	
public static class IpHelpers {
    public static string ToIpString(this UInt32 value) {
        var bitmask = 0xff000000;
        var parts = new string[4];
        for (var i = 0; i < 4; i++) {
            var masked = (value & bitmask) >> ((3-i)*8);
            bitmask >>= 8;
            parts[i] = masked.ToString(CultureInfo.InvariantCulture);
        }
        return String.Join(".", parts);
    }

    public static UInt32 ParseIp(this string ipAddress) {
        var splitted = ipAddress.Split('.');
        UInt32 ip = 0;
        for (var i = 0; i < 4; i++) {
            ip = (ip << 8) + UInt32.Parse(splitted[i]);
        }
        return ip;
    }

/*		
public static uint[] GetIpRange(string ip, IPAddress subnet)
{
    uint ip2 = Utils.IPv4ToUInt(ip);
    uint sub = Utils.IPv4ToUInt(subnet);

    uint first = ip2 & sub;
    uint last = first | (0xffffffff & ~sub);

    return new uint[] { first, last };
}  */  
		
		
}	
	
	
	
}
