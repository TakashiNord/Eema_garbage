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
Длина строки (блока) обмена вместе с двумя контрольными суммами:
a. 16 для посылки запроса в прибор;
b. 72, 32 или 8 для ответа из прибора в зависимости от номера команды
*/

/*
6. Формат команды - 16 байт:
0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15
Сетевой адрес 4 байта Команда Данные 9 байт Кс1 Кс2

7. Формат ответа - 72 байта (длинные, для команд БД):
0 1 2 3 4 5 6 7 … 67 68 69 70 71
Сетевой адрес 4 байта команда Данные 65 байт Кс1 Кс2

или - 32 байта (короткие):
0 1 2 3 4 5 6 7 … 27 28 29 30 31

Сетевой адрес 4 байта команда Данные 25 байт Кс1 Кс2
или - 8 байт (команда 48)
0 1 2 3 4 5 6 7
Сетевой адрес 4 байта команда Данные 1 байт Кс1 Кс2
8. Длина ответа однозначно соответствует номеру посланной команды
*/

/*
Формирование 2-х байтной КС:
a. 1-й байт по исключающему ИЛИ всех байтов предшествующих КС1;
b. 2-й байт - сумма по модулю 256 всех байтов предшествующих КС1 (не КС2!
*/

/*
 функция         | длина | timeout
0-10,12-38,40-48 |  32   | 100 мс
11, 39, 49-63    |  32   | 300 мс
64-90            |  72   | 300 мс
91-100           |  72   | 800 мс
101-127          |  72   | 100 мс
*/

/*
функция = 0      - Запрос сетевого номера прибора.
1 4 int  Возвращаемые данные сетевой номер прибора (1-й … 4-й байт)
5 4 int  модель прибора
*/


/*
функция = 126
1 4 float Сумма кода расхода Gi основного потока
5 4 float Сумма значений теплового потока W 
9 4 float Сумма значений объемного расхода GV1
13 4 float Сумма значений массового расхода GM1
17 4 float Сумма значений объемного расхода GV2
21 4 float Сумма значений массового расхода GM2
25 4 float Сумма температуры t1
29 4 float Сумма температуры t2
33 4 float Сумма температуры t3 (холодной воды)
37 4 float Сумма температуры t3 ППС (холодной воды ППС)
41 4 float Сумма температуры t2 ППС
45 4 float Сумма кода расхода Gi в ППС
49 4 float Резерв
53 4 float Резерв
57 4 float Реальное время суммирования (секунд) измеренное в приборе (время удержания сигнала SS)
63 2 int Счетчик просуммированных величин
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
	
	// Создаём сокет
	if (INVALID_SOCKET == (s = socket (AF_INET, SOCK_STREAM, IPPROTO_TCP) ) )
	{
		// Error
		return -1;
	}


	// Получаем адрес
	if (NULL == ( hn = gethostbyname (SERVERADDR) ) )
	{
		// Error
		return -1;
	}

	// Заполняем  поля структуры adr для использование ее в connect
	adr.sin_family				= AF_INET;
	adr.sin_addr.S_un.S_addr	= *(DWORD* ) hn->h_addr_list[0];
	adr.sin_port				= htons(PORT);

	// Устанавливаем соединение с сервером
	if (SOCKET_ERROR == connect (s, (sockaddr* )&adr,  sizeof (adr) ) )
	{
		// Error
		int res = WSAGetLastError ();
		//return -1;
	}
	
	
	sprintf(request,"%s",'\n');
	reqbuff = &request[0] ;

	// Посылаем запрос серверу
	if (SOCKET_ERROR == send (s, reqbuff, sizeof (request), 0) )
	{
		// Error
		int res = WSAGetLastError ();
		return -1;
	}

	// Ждём ответа
	int len, res;
	
	fd_set	read_s;
	timeval	time_out;
	
	time_out.tv_sec = 0;time_out.tv_usec = 500000; // 0.5 sec.

	do
	{
		FD_ZERO (&read_s);	 // Обнуляем мнодество
		FD_SET (s, &read_s); // Заносим в него наш сокет 
		if (SOCKET_ERROR == (res = select (0, &read_s, NULL, NULL, &time_out) ) )
			return -1;

		if ((res!=0) && (FD_ISSET (s, &read_s)) ) // Я использую здесь FD_ISSET только для примера! :)
		{
			// Данные готовы к чтению...
			if (SOCKET_ERROR == (len = recv (s, (char *) &buff, MAX_PACKET_SIZE, 0) ) )
			{
				int res = WSAGetLastError ();
				return -1;
			}
			// Выводим их на экран.
			for (int i = 0; i<len; i++)
				printf ("%c", buff [i]);
		}

		// Делаем все необходимые нам действия...
		// Пример, конечно, условный! :)

	}
	while (len!=0);

	if (SOCKET_ERROR == closesocket (s) )	// закрываем соединенеие. *
	{
		// Error
		return -1;
	}
	
	return 0;
}


