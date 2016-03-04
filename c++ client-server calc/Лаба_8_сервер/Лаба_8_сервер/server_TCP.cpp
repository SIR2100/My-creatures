#include "server_TCP.h"
#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdlib.h>
#include <stdio.h>
# include <iostream>
# include <conio.h>
using namespace std;

int __cdecl main(void) 
{
	WSADATA wsaData;
	SOCKET ListenSocket,ClientSocket;  // впускающий сокет и сокет для клиентов
	sockaddr_in ServerAddr;  // это будет адрес сервера
	int err;  // код ошибки и размер буферов
	const int maxlen = 512;
	char* recvbuf=new char[maxlen];  // буфер приема
	char* test=new char[maxlen]; // тестовая переменная - копия буфера приема
	char* result_string=new char[maxlen];  // буфер отправки


	// Initialize Winsock
	WSAStartup(MAKEWORD(2,2), &wsaData);

	// Create a SOCKET for connecting to server
	ListenSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

	// Setup the TCP listening socket
	ServerAddr.sin_family=AF_INET;
	ServerAddr.sin_addr.s_addr=inet_addr("127.0.0.1");
	ServerAddr.sin_port=htons(12345);
	err = bind( ListenSocket, (sockaddr *) &ServerAddr, sizeof(ServerAddr));
	if (err == SOCKET_ERROR) {
		printf("bind failed: %d\n", WSAGetLastError());
		closesocket(ListenSocket);
		WSACleanup();
        system("pause");
		return 1;
	}

	err = listen(ListenSocket, 50);
	if (err == SOCKET_ERROR) {
		printf("listen failed: %d\n", WSAGetLastError());
		closesocket(ListenSocket);
		WSACleanup();
        system("pause");
		return 1;
	}
	while (true) {
		// Accept a client socket
		ClientSocket = accept(ListenSocket, NULL, NULL);
		err = recv(ClientSocket, recvbuf, maxlen, 0); //количество символов
		if (err > 0) {
			char *result_string = recvbuf; //_string
			
			char *x[maxlen] =  {0};
			char *p = strtok(result_string, "&");
			int i = 0;
			float result;
			while(p)
			{
				x[i++] = p;
				p = strtok(NULL, "&");
			}
			int a1 = atoi(x[0]);
			int a3 = atoi(x[2]);
			if (x[1][0] == '+') {  	
				result = a1 + a3;
			}
			else if (x[1][0] == '-') {
				result = a1 - a3;
			}
			else if (x[1][0] == '*') {
				result = a1 * a3;
			}
			else if (x[1][0] == '/') {
				result = a1 / a3;
			}
			else if (x[1][0] == '^') {
				result = pow(a1,a3);
			}
			
			//страшная реализация
	
	

	//		int result=atoi(result_string);
			recvbuf[err]=0;
			printf("Received query: %s\n", result_string);
			// вычисляем результат
			_snprintf_s(result_string,maxlen,maxlen,"OK %.2f",result);
			// отправляем результат клиенту
			send( ClientSocket,  result_string, strlen(result_string), 0 );
			printf("Sent answer: %s\n", result_string);
			result = 0;
			a1 = 0;
			a3 = 0;
			*x = 0;
			result_string = 0;
		}
		else if (err == 0)
			printf("Connection closing...\n");
		else  {
			printf("recv failed: %d\n", WSAGetLastError());
			closesocket(ClientSocket);
			WSACleanup();
            system("pause");
			return 1;
		}

		// закрываем соединение
		closesocket(ClientSocket);
	}
}