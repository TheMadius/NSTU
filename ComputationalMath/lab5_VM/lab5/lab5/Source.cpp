#include <iostream>
#include <Math.h>
#include <locale>

using namespace std;
int iteration;

double Fx(double x)
{
	double y;
	y = pow(x + 1, 2) / (pow(x, 3) * sqrt(2 + x));

	return y;
}
double Fx2(double x)
{
	return 0;
}


double mediumRectangles(double (*F)(double), double a, double b, double e)
{
	double h = 2, step, S1, S2, sum, x1 , x2;
	iteration = 0;
	S1 = (b - a) * F((a + b) / 2);

	while (true)
	{
		iteration++;
		sum = 0;
		x1 = a;
		step = (b - a) / h;

		for (int i = 0;i < h;i++)
		{
			x2 = x1 + step;
			sum += F((x1 + x2) / 2);
			x1 = x2;
		}

		S2 = step * sum;

		if (abs(S2 - S1) < e)
		{
			cout << "Шаг : " << step << " кол. интервалов : " << h << endl;
			return S2;
		}

		S1 = S2;
		h = h * 2;
	}
}

double trapezoid(double (*F)(double), double a, double b, double e)
{
	double h = 2, step, S1, S2, sum, x1;
	iteration = 0;

	S1 = ((b - a)/2) *(F(a) + F(b));

	while (true)
	{
		iteration++;
		sum = 0;
		x1 = a;
		step = (b - a) / h;

		for (int i = 0;i < h - 1; ++i)
		{
			x1 += step;
			sum += F(x1);
		}

		S2 = (step / 2) * (F(a) + 2 * sum + F(b));

		if (abs(S2 - S1) < e)
		{
			cout << "Шаг : " << step <<" кол. интервалов : " << h << endl;
			return S2;
		}

		S1 = S2;
		h = h * 2;
	}
}

double Simpson(double (*F)(double), double a, double b, double e)
{
	double h = 3, step, S1, S2, sum, x1;
	double C0 = 3. / 8.;
	iteration = 0;

	step = (b - a) / h;
	sum = (F(a) + 3 * F(a + step) + 3 * F(a + step + step) + F(b));
	S1 = C0 * step * sum;

	h = h * 2;

	while (true)
	{
		iteration++;
		x1 = a;
		sum = 0;

		step = (b - a) / h;

		for (int i = 0; i < h / 3; ++i)
		{
			sum += (F(x1) + 3 * F(x1 + step) + 3 * F(x1 + step * 2) + F(x1 + step * 3));
			x1 = x1 + step * 3;
		}

		S2 = C0 *step *sum;

		if (abs(S2 - S1) < e)
		{
			cout << "Шаг : " << step << " кол. интервалов : " << h << endl;
			return S2;
		}

		S1 = S2;
		h = h * 2;
	}
}

int main()
{
	setlocale(LC_ALL, "ru");

	double e = 0.001;
	double st = 10;

	cout << "Трапеций : " << trapezoid(Fx, 1.4, st, e) << endl;
	cout << "Количество итераций : " << iteration << endl << endl;

	cout << "Средних прямоугольников : " << mediumRectangles(Fx, 1.4, st, e) << endl;
	cout << "Количество итераций : " << iteration << endl << endl;

	cout << "Ньютона-Котеса 3-го порядка : " << Simpson(Fx, 1.4, st, e) << endl;
	cout << "Количество итераций : " << iteration << endl << endl;

	cout << endl;
}