// ConsoleApplication11.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include "pch.h"
#include <iostream>
#include <cstdlib>
#include <locale>
#include <ctime>
using namespace std;
const int N = 10;
const int G = 8;
const int F = N + G;
int main()
{
	setlocale(LC_ALL, "RU");
	int A[N];
	int B[G];
	int C[F];
    srand(time(NULL));
	cout << "Введите 10 чисел 1 массива";
	for (int i = 0; i < N; i++)
	{
		cin >> A[i];
	}
	cout << "Введите 10 чисел 2 массива";
	for (int i = 0; i < G; i++)
	{
		cin >> B[i];
	}
	for (int i = 0; i < N; i++) {
		C[i] = A[i];
	}
	for (int i = 0; i < G; i++) {
		C[i+N] = B[i];
	}
	cout << endl;

	for (int i = 0; i < F; i++)
	{
		for (int j = 0; j < F - i - 1; j++) {
			if (C[j] > C[j + 1]) {
				swap(C[j], C[j + 1]);
			}
		}
		
	}
	for (int i = 0; i < F; i++) {
		cout << C[i] << endl;
	}
}
/*
{
	setlocale(LC_ALL, "RU");
	int A[N];
	bool flag = true;
	srand(time(NULL));
	for (int i = 0; i < N; i++)
	{
		A[i] = rand() % 201 - 100;
		cout << A[i] << "   ";
	}
	cout << endl;
	for (int i = 0; i < N; i++)
	{
		for (int j = 0; j < N - i - 1; j++) {
			if (A[j] > A[j + 1]) {
				swap(A[j], A[j + 1]);
			}
		}

	}
	for (int i = 0; i < N; i++) {
		cout << A[i] << endl;
	}
	int i = 0;
while (flag && i < N - 1) {
	if (A[i + 1] < A[i]) {
		flag = false;
	}
	i++;
}
if (flag) { cout << "Верно"; }
else {
	cout << "Не верно";
}
}*/

// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"

// Советы по началу работы 
//   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
//   2. В окне Team Explorer можно подключиться к системе управления версиями.
//   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
//   4. В окне "Список ошибок" можно просматривать ошибки.
//   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
//   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.
