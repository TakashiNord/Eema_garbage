// ConsoleApplication1.cpp: ���������� ����� ����� ��� ����������� ����������.
//

#include "stdafx.h"

#include "windows.h"
#include "time.h"


int _tmain(int argc, _TCHAR* argv[])
{
	int v0=10;
	int CurTaskTime;
	int NextTaskTime=0;
	int TaskPeriod=0;
	int DCS_CYCLIC_SHIFT=5;
	
	while(1)
	{
	   Sleep(v0);

      /* ��������� ���� �� ��� �������� � ����������� �� ��������� ��������� */
      CurTaskTime = time(0);
      if(CurTaskTime < NextTaskTime)
            continue;
      if (0 == TaskPeriod)
         NextTaskTime = CurTaskTime;
      else
         NextTaskTime = CurTaskTime - CurTaskTime % TaskPeriod + TaskPeriod + DCS_CYCLIC_SHIFT; /* ������ ��������� ����� ������ */

	  printf("%d = %d\n",CurTaskTime,NextTaskTime);
	}

	
	return 0;
}

