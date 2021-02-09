/*
 * Created by SharpDevelop.
 * User: tanuki
 * Date: 07.01.2021
 * Time: 22:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ArcConfig
{
	/// <summary>
	/// Description of .
	/// </summary>
public class ArcGinfo
{
    public ArcGinfo( )
    {
    	;
    }
    public int ID { get;  set; }
    public int ID_GTOPT { get;  set; }
    public int ID_TYPE { get;  set; }
    public int DEPTH { get;  set; }
    public int DEPTH_LOCAL { get;  set; }
    public int CACHE_SIZE { get;  set; }
    public int CACHE_TIMEOUT { get;  set; }
    public int FLUSH_INTERVAL { get;  set; }
    public int RESTORE_INTERVAL { get;  set; }
    public int STACK_INTERVAL { get;  set; }
    public int WRITE_MINMAX { get;  set; }
    public int RESTORE_TIME { get;  set; }
    public string NAME { get; set; }
    public int STATE { get;  set; }
    public int DEPTH_PARTITION { get;  set; }
    public int RESTORE_TIME_LOCAL { get;  set; }
}

public class ARCFTR
{
    public ARCFTR( )
    {
    	;
    }
    public int ID { get; set; }
    public string NAME { get; set; }
    public string DEFINE_ALIAS { get; set; }
    public int MASK { get; set; }
}

}
