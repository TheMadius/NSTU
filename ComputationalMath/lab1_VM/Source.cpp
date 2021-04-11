#include <iostream>
#include <math.h>

using namespace std;

double F(double x)
{
	return sin(x) - 1 / x;
}

double dF(double x0, double x1)
{
	return (F(x0) - F(x1)) / (x0 - x1);
}

double metNewton(double x0,double e)
	{
		double x1 = x0 + e;

		while (true)
		{
			double x2 = x0 - F(x0) / dF(x0, x1);

			if (abs(x2 - x1) < e*0.001)
			{
				return x2;
			}

			x0 = x1;
			x1 = x2;
		}	
	}


int main()
{
	setlocale(LC_ALL, "rus");

	double e = 0.01;

	cout << metNewton(6.28, e);
	
}