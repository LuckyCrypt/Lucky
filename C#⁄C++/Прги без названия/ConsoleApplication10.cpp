// ConsoleApplication10.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//
#include "pch.h"
#include <iostream>
#include <cstdlib>
#include <locale>
#include <ctime>
using namespace std;
const int N = 10;
int main()
{
	setlocale(LC_ALL, "RU");
	int A[N];
	int mid, chislo, search_mid,left,right;
	bool found = false;
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
		for (int i = 0; i < N; i++) {
			cout << A[i] << endl;
		}
	}
	left = A[0];
	right = A[N - 1];
	cout << "Введите число которое хотите найти" << endl;
	cin >> chislo;
	while (chislo <= right && !found)
	{
		mid = (left + right) / 2;
		if (chislo == A[mid])
		{
			search_mid = mid;
			found = true;
			cout << "Индекс=" << search_mid << endl;
		}


		else
		{
			if (chislo < A[mid])
			{
				right = mid - 1;
			}
			else
			{
				chislo = mid + 1;
			}
		}
	}

	cout << chislo;
}


// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"

// Советы по началу работы 
//   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
//   2. В окне Team Explorer можно подключиться к системе управления версиями.
//   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
//   4. В окне "Список ошибок" можно просматривать ошибки.
//   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
//   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.
