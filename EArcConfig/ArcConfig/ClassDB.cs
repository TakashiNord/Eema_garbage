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


public class MEAS1
{
    public MEAS1(int _id, int _idn, int _idt, string _nm, string _al, int _ginfo )
    {
      ID = _id ;
      ID_NODE = _idn ;
      ID_TYPE = _idt ;
      NAME1 = _nm;
      NAME2 = _al ;
      ID_GINFO = _ginfo ;
    }
    public int ID { get; set; }
    public int ID_NODE { get; set; }
    public int ID_TYPE { get; set; }
    public string NAME1 { get; set; }
    public string NAME2 { get; set; }
    public int ID_GINFO { get; set; }
}

public class ARCSUM1
{
    public ARCSUM1(string _nm, string _ginfo , int _sum)
    {
      NAME1 = _nm;
      ID_GINFO = _ginfo ;
      SUM = _sum ;
    }
    public int SUM { get; set; }
    public string NAME1 { get; set; }
    public string ID_GINFO { get; set; }
}


public class EXT
{
    public EXT(int _cnt, int _min , int _max)
    {
      MAX = _max;
      MIN = _min ;
      CNT = _cnt ;
    }
    public int CNT { get; set; }
    public int MIN { get; set; }
    public int MAX { get; set; }
}


}
