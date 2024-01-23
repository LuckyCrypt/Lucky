// дз2.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <cstdlib>
#include <locale>
#include <ctime>
using namespace std;
void random(int A[], const int N)
{
	int n = -100;
	for (int i = 0; i < N; i++)
	{
		A[i] = rand() % 201 - 100;
		cout << A[i] << "  ";
	}
}
void max(int A[],const int N,int n=-100) 
{
	for (int i = 0;i < N;i++) 
	{
		if (A[i] > n)
		{
			n = A[i];
		}
	}
	cout <<"---"<< n;
}
int main()
{
	const int N = 10;
	setlocale(LC_ALL, "RU");
	srand(time(NULL));
	int A[N];
	int n;
	random(A, N);
	max(A, N);
}
