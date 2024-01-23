#include <iostream>
#include <conio.h>
#include <stdio.h>

using std::cout;
using std::cin;
using std::endl;

const int n = 10;

//класс для подстчета количества повторов
class CFrequency {
private:
    int num;
    int count;  //переменная для подсчета количества повторяющихся значений
public:
    bool operator<<(CFrequency& aBox);
    int Iteration(int numbers[n]);
};

//перегрузка оператора << для класса
bool CFrequency::operator<<(CFrequency& aBox) {
    cout << count << endl;
    return 0;
}

//считаем количество повторяющихся значений
int CFrequency::Iteration(int numbers[n]) {
    cout << "Enter the number for count" << endl;
    cin >> num;
    count = 0;
    for (int i = 0; i < n; i++) {
        if (numbers[i] == num) count++;
    }
    cout << "Number of inerations is " << count << endl;
    return 0;
}

//считывание нач. данных с клавиатуры
class CFromKeyb :public CFrequency {
private:
    int numbers[n]; //поправить на задание размера вручную
public:
    int Enter(int numbers[n]);
};

//считывание нач. данных из файла
class CFromFile {
private:
    char ch;
public:
    int FileCheck(FILE* file);
};

//класс записи данных в файл
class CToFile :public CFromFile {
private:
    FILE* fp;
    int outnum; //для записи в файл (именно она будет записываться) 
public:
    void Writing();
};

//записываем данные в файл
void CToFile::Writing() {
    fp = fopen("test.txt", "w");
    if (FileCheck(fp) == 1) {
        cout << "Cannot open the file" << endl;
        return;
    }
    while (outnum != EOF) { //считываем значения, пока не будет нажато ctrl+Z
        cin >> outnum;
        fwrite(&outnum, sizeof(int), 1, fp);
    }
};

//проверка открытия файла
int CFromFile::FileCheck(FILE* file) {
    if ((file = fopen("test.txt", "wb")) == NULL) {
        cout << "Cennot open the file" << endl;
        return 1;
    }
    else return 0;
};

//заполнение массива с клавиатуры
int CFromKeyb::Enter(int numbers[n]) {
    for (int i = 0; i < n; i++) {
        cout << "Enter the number " << i + 1 << ":" << endl;
        cin >> numbers[i];
    }
    return 0;
}

int main() {
    cout << "Hello, user! I glad to see you ^_^" << endl
        << "Chose the problem: " << endl
        << "1.Frequency of iterations" << endl
        << "2.Search for max number" << endl
        << "3.Search for min number" << endl
        << "4.Consider the summ of numbers" << endl;
    int check;
    cin >> check;
    //проверяем ввод (ДОПИЛИТЬ ОБРАБОТКУ СИМВОЛОВ)
    if (check != 1 & check != 2 & check != 3 & check != 4) {
        cout << endl << "Wrong input" << endl << endl;
        main();
    }
    switch (check) {
    case 1: {
        cout << "Select a input method: " << endl
            << "1.From keyboard" << endl
            << "2.From file" << endl;
        int choise = 0;
        cin >> choise;
        switch (choise) {
        case 1: CFromKeyb.Enter();
        case 2: CToFile.Writing();
        }
        getch();
        return 0;
    }
    }
}