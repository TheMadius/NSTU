#include <iostream>
#include <fstream>
#include <vector>
#include <thread>
#include <chrono>
#include <string>
#include "pthread.h"
#include "semaphore.h"
#pragma comment(lib, "pthreadVCE2.lib")

using namespace std;

struct Pair
{
	int a;
	int b;
};

bool flag = true;
ifstream f("data.txt");

struct info
{
	pthread_mutex_t *mfile;
	pthread_mutex_t *m;
	sem_t *s;
	vector<Pair>* listData;
	int i;
};

int NOD(int a, int b)
{
	if (a < b)
		swap(a, b);
	while (a != 0 and b != 0)
	{
		a = a % b;
		swap(a, b);
	}

	return a;
}

void* read(void * args)
{
	info inf = *((info*)args);
	
	Pair p;

	while (true)
	{
		pthread_mutex_lock(inf.mfile);
		if (f.eof())
		{
			pthread_mutex_unlock(inf.mfile);
			break;
		}
		f >> p.a;
		f >> p.b;
		pthread_mutex_unlock(inf.mfile);

		pthread_mutex_lock(inf.m);
		inf.listData->push_back(p);
		sem_post(inf.s);
		pthread_mutex_unlock(inf.m);
	}

	pthread_mutex_lock(inf.mfile);
	flag = false;
	sem_post(inf.s);
	cout << "end read" << inf.i <<endl;
	pthread_mutex_unlock(inf.mfile);

	return nullptr;
}

void* write(void* args)
{
	info inf = *((info*)args);
	ofstream f(string("outfile") + (char)('0'+inf.i) +".txt");
	Pair p;
	int res;

	while (true)
	{
		pthread_mutex_lock(inf.m);
		if (!flag && inf.listData->empty())
		{
			pthread_mutex_unlock(inf.m);
			break;
		}
		if (inf.listData->empty())
		{
			pthread_mutex_unlock(inf.m);
			sem_wait(inf.s);
			continue;
		}
		p = inf.listData->back();
		inf.listData->pop_back();
		pthread_mutex_unlock(inf.m);

		res = NOD(p.a,p.b);

		f << "NOD("<<p.a <<","<< p.b <<") = "<< res << endl;
	}

	pthread_mutex_lock(inf.mfile);
	cout << "end write" << inf.i << endl;
	pthread_mutex_unlock(inf.mfile);

	f.close();
	return nullptr;
}

int main()
{
	sem_t *s;
	pthread_t **t;
	pthread_mutex_t m;
	pthread_mutex_t* m_list;
	pthread_mutex_init(&m, NULL);
	t = new pthread_t*[2];
	pthread_cond_t cond;
	int num;
	info *inf;
	cout << "Enter num = ";
	cin >> num;

	inf = new info[num];
	m_list = new pthread_mutex_t[num];
	s = new sem_t[num];
	vector<Pair>* listData = new vector<Pair>[num];


	for (int i = 0; i < 2; ++i)
	{
		t[i] = new pthread_t [num];
	}

	clock_t time = clock();

	for (int i = 0;i < num; ++i)
	{
		pthread_mutex_init(&m_list[i], NULL);
		sem_init(&s[i], 0, 0);
		inf[i].mfile = &m;
		inf[i].m = &m_list[i];
		inf[i].i = i;
		inf[i].listData = &listData[i];
		inf[i].s = &s[i];

		pthread_create(&t[0][i], NULL, write, &inf[i]);
		pthread_create(&t[1][i], NULL, read, &inf[i]);
	}

	for (int i = 0; i < num; ++i)
	{
		pthread_join(t[0][i], NULL);
		pthread_join(t[1][i], NULL);
	}
	time = clock() - time;

	cout << time << " ms" << " ";


	delete[] m_list;
	delete[] inf;
	delete[] listData;

	f.close();
	return 0;
}