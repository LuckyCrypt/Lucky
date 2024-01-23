// Enykeev.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <omp.h>
#include <ctime>
#include <iomanip>
#include <chrono>
double ploshad(double a, double b, double h)
{
	return ((b - a) * 0.5) * h;
}
double f(double x)
{
	return 2 * x;
}

//void transpose(int **matrix)
//{
//	auto starttime = omp_get_wtime();
//	//auto start_time = std::chrono::steady_clock::now();
//	int t;
//	for (int i = 0; i < 10000; ++i)
//	{
//		for (int j = i; j < 10000; ++j)
//		{
//			t = matrix[i][j];
//			matrix[i][j] = matrix[j][i];
//			matrix[j][i] = t;
//		}
//	}
//	auto endtime = omp_get_wtime();
//	//auto end_time = std::chrono::steady_clock::now();
//	std::cout << "timeposled              " << endtime - starttime << std::endl;
//}
//
//
//
//void transparalelstatic(int** matrix)
//{
//	auto starttime = omp_get_wtime();
//	//auto start_time = std::chrono::steady_clock::now();
//	int t;
//#pragma omp parallel for schedule(static, 2)
//	for (int i = 0; i < 10000; ++i)
//	{
//		for (int j = i; j < 10000; ++j)
//		{
//			t = matrix[i][j];
//			matrix[i][j] = matrix[j][i];
//			matrix[j][i] = t;
//		}
//	}
//	auto endtime = omp_get_wtime();
//	//auto end_time = std::chrono::steady_clock::now();
//	std::cout << "time paralel static 2   " << endtime - starttime << std::endl;
//}
//
//void transparalelstatic10(int** matrix)
//{
//	auto starttime = omp_get_wtime();
//	//auto start_time = std::chrono::steady_clock::now();
//	int t;
//#pragma omp parallel for schedule(static, 10)
//	for (int i = 0; i < 10000; ++i)
//	{
//		for (int j = i; j < 10000; ++j)
//		{
//			t = matrix[i][j];
//			matrix[i][j] = matrix[j][i];
//			matrix[j][i] = t;
//		}
//	}
//	auto endtime = omp_get_wtime();
//	//auto end_time = std::chrono::steady_clock::now();
//	std::cout << "time paralel static 10  " << endtime - starttime << std::endl;
//}
//
//void transparalelguided(int** matrix)
//{
//	auto starttime = omp_get_wtime();
//	//auto start_time = std::chrono::steady_clock::now();
//	int t;
//#pragma omp parallel for schedule(guided, 2)
//	for (int i = 0; i < 10000; ++i)
//	{
//		for (int j = i; j < 10000; ++j)
//		{
//			t = matrix[i][j];
//			matrix[i][j] = matrix[j][i];
//			matrix[j][i] = t;
//		}
//	}
//	auto endtime = omp_get_wtime();
//	//auto end_time = std::chrono::steady_clock::now();
//	std::cout << "time guidied 2          " << endtime - starttime << std::endl;
//}
//
//void transparalelguided10(int** matrix)
//{
//	auto starttime = omp_get_wtime();
//	//auto start_time = std::chrono::steady_clock::now();
//	int t;
//#pragma omp parallel for schedule(guided, 10)
//	for (int i = 0; i < 10000; ++i)
//	{
//		for (int j = i; j < 10000; ++j)
//		{
//			t = matrix[i][j];
//			matrix[i][j] = matrix[j][i];
//			matrix[j][i] = t;
//		}
//	}
//	auto endtime = omp_get_wtime();
//	//auto end_time = std::chrono::steady_clock::now();
//	std::cout << "time guidied 10         " << endtime - starttime << std::endl;
//}
//
//void transparalel(int** matrix)
//{
//	auto starttime = omp_get_wtime();
//	//auto start_time = std::chrono::steady_clock::now();
//	int t;
//#pragma omp parallel for
//	for (int i = 0; i < 10000; ++i)
//	{
//		for (int j = i; j < 10000; ++j)
//		{
//			t = matrix[i][j];
//			matrix[i][j] = matrix[j][i];
//			matrix[j][i] = t;
//		}
//	}
//	auto endtime = omp_get_wtime();
//	//auto end_time = std::chrono::steady_clock::now();
//	std::cout << "time chisto             " << endtime - starttime << std::endl;
//}
/*double trapezoidalIntegral(double a, double b, double n)
{
	const double width = (b - a) / n;

	double trapezoidal_integral = 0;
	for (int step = 0; step < n; step++)
	{
		const double x1 = a + step * width;
		const double x2 = a + (step + 1) * width;
		trapezoidal_integral += (x2 - x1) * 0.5 * (f(x1) + f(x2));
	}

	return trapezoidal_integral;

}*/
////////////////////////////////////////////////////Вычисление интеграла методом трапеции.
double trapezoidalIntegralparalel(double a, double b, double n, int shag,int nomershag)
{
	const double width = (b - a) / n;
	double trapezoidal_integral = 0;
	for (int step = nomershag; step < n; step += shag)
	{
		const double x1 = a + step * width;
		const double x2 = a + (step + 1) * width;
		trapezoidal_integral += (x2 - x1) * 0.5 * (f(x1) + f(x2));
	}
	return trapezoidal_integral;
}
/*void ymnojenyematriz()
{
	srand(10);
	const int Razmer1 = 5;
	const int Razmer2 = 5;
	int Matr1[Razmer1][Razmer2] = {};
	int Matr2[Razmer1][Razmer2] = {};
	int Matr3[Razmer1][Razmer2] = {};
	int MAX_VALUE = 10;
	int	MIN_VALUE = 1;
	for (int i = 0; i < Razmer1; ++i) { 
		for (int j = 0; j < Razmer2; ++j)
			Matr1[i][j] = rand() % (MAX_VALUE - MIN_VALUE + 1);
	}
	for (int i = 0; i < Razmer1; ++i) {
		for (int j = 0; j < Razmer2; ++j)
			Matr2[i][j] = rand() % (MAX_VALUE - MIN_VALUE + 1);
	}
	for (int i = 0; i < Razmer1; ++i) {  // Выводим на экран строку i
		for (int j = 0; j < Razmer2; ++j)
			std::cout << Matr1[i][j] << " ";
		std::cout << std::endl; // Строка завершается символом перехода на новую строку
	}
	std::cout << std::endl;
	for (int i = 0; i < Razmer1; ++i) {  // Выводим на экран строку i
		for (int j = 0; j < Razmer2; ++j)
			std::cout << Matr2[i][j] << " ";
		std::cout << std::endl; // Строка завершается символом перехода на новую строку
	}
	unsigned int start_time = clock();
	std::cout << std::endl;
	int MatrOtvet[Razmer1][Razmer2]= {};
	for (int i = 0; i < Razmer1; i++)
	{
		for (int j = 0; j < Razmer2; j++)
		{
			for (int k = 0; k < Razmer2; k++)
				MatrOtvet[i][j] += Matr1[i][k] * Matr2[k][j];
		}
	}
	unsigned int end_time = clock();
	for (int i = 0; i < Razmer1; ++i) {  // Выводим на экран строку i
		for (int j = 0; j < Razmer2; ++j)
			std::cout << MatrOtvet[i][j] << " ";
		std::cout << std::endl; // Строка завершается символом перехода на новую строку
	}
	unsigned int search_time = end_time - start_time;
	std::cout << "otvet" << search_time;
	std::cout << std::endl;
}*/
/////////////////////////////////////////////////////////////////////////////////////////////////////////
/*void ymnojenyematrizparalel()
{

	srand(10);
	const int Razmer1 = 5;
	const int Razmer2 = 5;
	int Matr1[Razmer1][Razmer2] = {};
	int Matr2[Razmer1][Razmer2] = {};
	int Matr3[Razmer1][Razmer2] = {};
	int MAX_VALUE = 10;
	int	MIN_VALUE = 1;
	for (int i = 0; i < Razmer1; ++i) {
		for (int j = 0; j < Razmer2; ++j)
			Matr1[i][j] = rand() % (MAX_VALUE - MIN_VALUE + 1);
	}
	for (int i = 0; i < Razmer1; ++i) {
		for (int j = 0; j < Razmer2; ++j)
			Matr2[i][j] = rand() % (MAX_VALUE - MIN_VALUE + 1);
	}
	for (int i = 0; i < Razmer1; ++i) {  // Выводим на экран строку i
		for (int j = 0; j < Razmer2; ++j)
			std::cout << Matr1[i][j] << " ";
		std::cout << std::endl; // Строка завершается символом перехода на новую строку
	}
	std::cout << std::endl;
	for (int i = 0; i < Razmer1; ++i) {  // Выводим на экран строку i
		for (int j = 0; j < Razmer2; ++j)
			std::cout << Matr2[i][j] << " ";
		std::cout << std::endl; // Строка завершается символом перехода на новую строку
	}
	std::cout << std::endl;
	unsigned int start_time = clock();
	int MatrOtvet[Razmer1][Razmer2] = {};
	for (int i = 0; i < Razmer1; i++)
	{
		for (int j = 0; j < Razmer2; j++)
		{
			int timever = 0;
			#pragma omp parallel for reduction(+:timever) num_threads(2)
			for (int k = 0; k < Razmer2; k++)
				timever = Matr1[i][k] * Matr2[k][j];
			MatrOtvet[i][j] = timever;
			
		}
	}
	unsigned int end_time = clock();
	for (int i = 0; i < Razmer1; ++i) {  // Выводим на экран строку i
		for (int j = 0; j < Razmer2; ++j)
			std::cout << MatrOtvet[i][j] << " ";
		std::cout << std::endl; // Строка завершается символом перехода на новую строку
	}
	unsigned int search_time = end_time - start_time;
	std::cout << "otvet" << search_time;
	std::cout << std::endl;
}
*/

void speedtime(int** matrix)
{
	auto starttime = omp_get_wtime();
	for (int i = 0; i < 10000; ++i)
	{
		for (int j = i; j < 10000; ++j)
		{
			matrix[j][i] ++;
		}
	}
	auto endtime = omp_get_wtime();
	std::cout << "time paralel static 2   " << endtime - starttime << std::endl;
}
void speedtime2(int** matrix)
{
	auto starttime = omp_get_wtime();
	for (int i = 0; i < 10000; ++i)
	{
		for (int j = i; j < 10000; ++j)
		{
			matrix[i][j] ++;
		}
	}
	auto endtime = omp_get_wtime();
	std::cout << "time paralel static 2   " << endtime - starttime << std::endl;
}
int main()
{
	double dlnA = 10;//длина А
	double dlnB = 20;//длина B
	double dlnH = dlnB-dlnA;//длина высоты
	const int p = 2;
	double otvet1;
	int colichpotocov = 6;
	//otvet1 = trapezoidalIntegral(dlnA, dlnB, dlnH);

	setlocale(0, "");
	srand(unsigned(time(NULL)));/*
	int N=10000, M= 10000;

	int** A = new int* [N];
	for (int i = 0; i < N; i++)
		A[i] = new int[M];

	for (int i = 0; i < N; i++)
		for (int j = 0; j < M; j++)
			A[i][j] = ((rand() % 10));*/

		#pragma omp parallel num_threads(colichpotocov)
		{
			otvet1 = trapezoidalIntegralparalel(dlnA, dlnB, dlnH, p, 0);
			std::cout << otvet1 << std::endl;

		}
	

	/*transpose(A);
	transparalelstatic(A);
	transparalelstatic10(A);
	transparalelguided(A);
	transparalelguided10(A);
	transparalel(A);*/
	///*speedtime(A);*/
	//speedtime2(A);
	//std::cout << std::endl;
	////for (int i = 0; i < N; i++)
	////	delete[] A[i];
	////delete[] A;
}

