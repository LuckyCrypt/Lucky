﻿// дз 3.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <cstring>
#include <locale>
using namespace std;

int main()
{
	setlocale(LC_ALL, "ru");

	char S[100] = "";
	cout << "Введите строку" << endl;
	cin.getline(S, 99);
	char g;
	int h = 0;
	cout << "Введите Букву" << endl;
	cin >> g;
	for (int j = 0; j < 100; j++) {
		if (S[j] != '\0') {
			h = h + 1;
		}
		if (h=0){
			cout << "Слов нет";
		}
	}
	for (int j = 0; j <= h;j++)
	{
		if (S[j] == g)
		{
			if (S[j + 1] != ' ' || S[j + 1] != '\0')
			{
				while (S[j] != ' ')
				{
					cout << S[j];
				}
			}
		}
	}
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
