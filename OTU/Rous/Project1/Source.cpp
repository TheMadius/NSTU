#include <iostream>
#include <locale>
#include <stdio.h>

using namespace std;

int main()
{
	setlocale(LC_ALL, "ru");
	int n;
	double* odds;
	double** tabl;
	cout << "Введите порядок ДУ:";
	cin >> n;

	odds = new double[n + 1];

	for (int i = 0; i < n + 1; ++i)
	{
		printf("Введите a%i :", i);
		cin >> odds[i];
	}

	tabl = new double*[n + 1];

	tabl[0] = new double[n];
	tabl[1] = new double[n];

	for (int i = 0; i < n ; ++i)
	{
		tabl[0][i] = (i * 2 < n+1) ? odds[2 * i] : 0;
		tabl[1][i] = (i * 2 + 1< n+1) ? odds[2 * i + 1] : 0;
	}

	for (int i = 2; i < n + 1; ++i)
	{
		double r = tabl[i - 2][0] / tabl[i - 1][0];
		tabl[i] = new double[n + 1 - i];
		for (int j = 0; j < n + 1 - i; j++)
		{
			tabl[i][j] = tabl[i - 2][j + 1] - r * tabl[i - 1][j + 1];
		}
	}

	int flag = 0;

	for (int i = 0; i < n + 1; ++i)
	{
		if (tabl[i][0] >= 0)
		{
			flag++;
		}
	}

	string str = ((flag == (n + 1)) || (flag == 0)) ? "Система устойчивая\n" : "Система неустойчивая\n";
	cout << str;
	system("pause");
}
