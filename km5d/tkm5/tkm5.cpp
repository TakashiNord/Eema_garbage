// tkm5.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "winsock2.h"

#include <windows.h>
#include <time.h>

#pragma comment(lib,"ws2_32.lib")

#define WIN32 1

#ifdef WIN32
typedef signed char   int8_t;
typedef short int   int16_t;
typedef int       int32_t;
typedef long int  int64_t;

typedef unsigned char     uint8_t;
typedef unsigned short int    uint16_t;
typedef unsigned int      uint32_t;
typedef unsigned long int uint64_t;
#endif // WIN32

//
#define MAX_PACKET_SIZE		65535


#define PORT 4001
#define SERVERADDR "10.100.22.66"

/*
����� ������ (�����) ������ ������ � ����� ������������ �������:
a. 16 ��� ������� ������� � ������;
b. 72, 32 ��� 8 ��� ������ �� ������� � ����������� �� ������ �������
*/

/*
6. ������ ������� - 16 ����:
0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15
������� ����� 4 ����� ������� ������ 9 ���� ��1 ��2

7. ������ ������ - 72 ����� (�������, ��� ������ ��):
0 1 2 3 4 5 6 7 � 67 68 69 70 71
������� ����� 4 ����� ������� ������ 65 ���� ��1 ��2

��� - 32 ����� (��������):
0 1 2 3 4 5 6 7 � 27 28 29 30 31

������� ����� 4 ����� ������� ������ 25 ���� ��1 ��2
��� - 8 ���� (������� 48)
0 1 2 3 4 5 6 7
������� ����� 4 ����� ������� ������ 1 ���� ��1 ��2
8. ����� ������ ���������� ������������� ������ ��������� �������
*/

/*
������������ 2-� ������� ��:
a. 1-� ���� �� ������������ ��� ���� ������ �������������� ��1;
b. 2-� ���� - ����� �� ������ 256 ���� ������ �������������� ��1 (�� ��2!
*/

/*
 �������         | ����� | timeout
0-10,12-38,40-48 |  32   | 100 ��
11, 39, 49-63    |  32   | 300 ��
64-90            |  72   | 300 ��
91-100           |  72   | 800 ��
101-127          |  72   | 100 ��
*/

/*
������� = 0      - ������ �������� ������ �������.
1 4 int  ������������ ������ ������� ����� ������� (1-� � 4-� ����)
5 4 int  ������ �������
*/


/*
������� = 126
1 4 float ����� ���� ������� Gi ��������� ������
5 4 float ����� �������� ��������� ������ W 
9 4 float ����� �������� ��������� ������� GV1
13 4 float ����� �������� ��������� ������� GM1
17 4 float ����� �������� ��������� ������� GV2
21 4 float ����� �������� ��������� ������� GM2
25 4 float ����� ����������� t1
29 4 float ����� ����������� t2
33 4 float ����� ����������� t3 (�������� ����)
37 4 float ����� ����������� t3 ��� (�������� ���� ���)
41 4 float ����� ����������� t2 ���
45 4 float ����� ���� ������� Gi � ���
49 4 float ������
53 4 float ������
57 4 float �������� ����� ������������ (������) ���������� � ������� (����� ��������� ������� SS)
63 2 int ������� ���������������� �������
*/

int main(int argc, char* argv[])
{
	WSADATA		ws;
	SOCKET		s;
	sockaddr_in	adr;
	hostent*	hn;
	char		buff [MAX_PACKET_SIZE];
	char        request [32] ;
	char  *     reqbuff ;

	// Init 
	if (WSAStartup (0x0101, &ws) != 0)
	{
		// Error
		return -1;
	}
	
	// ������ �����
	if (INVALID_SOCKET == (s = socket (AF_INET, SOCK_STREAM, IPPROTO_TCP) ) )
	{
		// Error
		return -1;
	}


	// �������� �����
	if (NULL == ( hn = gethostbyname (SERVERADDR) ) )
	{
		// Error
		return -1;
	}

	// ���������  ���� ��������� adr ��� ������������� �� � connect
	adr.sin_family				= AF_INET;
	adr.sin_addr.S_un.S_addr	= *(DWORD* ) hn->h_addr_list[0];
	adr.sin_port				= htons(PORT);

	// ������������� ���������� � ��������
	if (SOCKET_ERROR == connect (s, (sockaddr* )&adr,  sizeof (adr) ) )
	{
		// Error
		int res = WSAGetLastError ();
		//return -1;
	}
	
	
	sprintf(request,"%s",'\n');
	reqbuff = &request[0] ;

	// �������� ������ �������
	if (SOCKET_ERROR == send (s, reqbuff, sizeof (request), 0) )
	{
		// Error
		int res = WSAGetLastError ();
		return -1;
	}

	// ��� ������
	int len, res;
	
	fd_set	read_s;
	timeval	time_out;
	
	time_out.tv_sec = 0;time_out.tv_usec = 500000; // 0.5 sec.

	do
	{
		FD_ZERO (&read_s);	 // �������� ���������
		FD_SET (s, &read_s); // ������� � ���� ��� ����� 
		if (SOCKET_ERROR == (res = select (0, &read_s, NULL, NULL, &time_out) ) )
			return -1;

		if ((res!=0) && (FD_ISSET (s, &read_s)) ) // � ��������� ����� FD_ISSET ������ ��� �������! :)
		{
			// ������ ������ � ������...
			if (SOCKET_ERROR == (len = recv (s, (char *) &buff, MAX_PACKET_SIZE, 0) ) )
			{
				int res = WSAGetLastError ();
				return -1;
			}
			// ������� �� �� �����.
			for (int i = 0; i<len; i++)
				printf ("%c", buff [i]);
		}

		// ������ ��� ����������� ��� ��������...
		// ������, �������, ��������! :)

	}
	while (len!=0);

	if (SOCKET_ERROR == closesocket (s) )	// ��������� �����������. *
	{
		// Error
		return -1;
	}
	
	return 0;
}


