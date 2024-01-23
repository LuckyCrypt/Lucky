// ConsoleApplication2.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include "pch.h"
#include <iostream>
#include <cstdlib>
#include <locale>
#include <ctime>
using namespace std;
const int N = 10;
int main()
/*{
	setlocale(LC_ALL, "RU");
	int A[N];
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
}*/




/*{
	int min, k;
	int A[N];
	srand(time(NULL));
	for (int i = 0; i < N; i++)
	{
		A[i] = rand() % 201 - 100;
		cout << A[i] << "   ";
	}
	cout << endl;
	for (int i = 0; i < N; i++)
	{
		min = A[i];
		k = i;
		for (int j = i + 1; j < N; j++)
		{
			if (A[j] < min) {
				min = A[j];
				k = j;
			}
		}
		if (k != i) {
			swap(A[k], A[i]);
		}
		cout << endl;
		for (int i = 0; i < N; i++)
		{
			cout << A[i] << "      ";
			cout << endl;
		}
	}
}
*/
/*{
	int A[N];

	srand(time(NULL));

	for (int i = 0; i < N; i++)
	{
		A[i] = rand() % 201 - 100;
		cout << A[i] << " ";
	}
	cout << endl;

	int key = 0;
	int i = 0;

	for (int j = 1; j < N; j++) {
		key = A[j];
		i = j - 1;
		while (i >= 0 && A[i] > key) {
			A[i + 1] = A[i];
			i = i - 1;
			A[i + 1] = key;
		}

	}
	for (int l = 0; l < N; l++)
	{
		cout << A[l] << " ";

	}
	cout << endl;
}*/
{

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
