#pragma once
#include <iostream>
#include <fstream>
#include <ctime>
#include "buffer.h"

using namespace std;

#ifndef LOOK_VIRTUAL_MEMORY
#define LOOK_VIRTUAL_MEMORY

template<typename T>
class virtual_memory
{
public:
	virtual_memory(const char* str, long sizeBuffer,int nPage);
	~virtual_memory();
	void vput(long index, T value);
	void clearBufer();
	T operator[](long index);
	T vget(long index);

private:
	unsigned int nPage;
	buffer<T>* buffers;
	int* number;
	int* status;
};

template<typename T>
virtual_memory<T>::virtual_memory(const char* str, long sizeBuffer, int nPage)
{
	this->nPage = nPage;
	buffers = new buffer<T>[nPage];
	status = new int[nPage];
	number = new int[nPage];

	for (int i = 0; i < nPage; i++)
	{
		status[i] = 0;
		number[i] = 0;
		buffers[i].init(str, sizeBuffer);
	}
}

template<typename T>
virtual_memory<T>::~virtual_memory()
{
	delete[] buffers;
	delete[] status;
	delete[] number;
}

template<typename T>
inline void virtual_memory<T>::vput(long index, T value)
{
	
	for (int i = 0; i < nPage; i++)
	{
		int page = (index - 1) / (buffers[i].getSizeBuffer() / sizeof(T)) + 1;
		if (number[i] == page)
		{
			int temp = 1 + (index - 1) % (buffers[i].getSizeBuffer() / sizeof(T));
			buffers[i].setBuff(temp, value);
			return;
		}
	}
	for (int i = 0; i < nPage; i++)
	{
		if (status[i] == 0)
		{
			status[i] = 1;
			number[i] = (index - 1) / (buffers[i].getSizeBuffer() / sizeof(T)) + 1;
			buffers[i].set(index, value);
			return;
		}
	}

	srand(time(NULL));

	int r = rand() % nPage;

	number[r] = (index - 1) / (buffers[r].getSizeBuffer() / sizeof(T)) + 1;

	buffers[r].set(index, value);

	return;

}

template<typename T>
inline void virtual_memory<T>::clearBufer()
{
	for (int i = 0; i < nPage; i++)
	{
		if (!buffers[i].empty())
			buffers[i].unswap();
	}

}

template<typename T>
inline T virtual_memory<T>::operator[](long index)
{
	return vget(index);
}

template<typename T>
inline T virtual_memory<T>::vget(long index)
{
	for (int i = 0; i < nPage; i++)
	{
		int page = (index-1) / (buffers[i].getSizeBuffer() / sizeof(T)) + 1;

		if (number[i] == page)
		{
			int temp = 1 + (index-1) % (buffers[i].getSizeBuffer() / sizeof(T));
			return buffers[i].getBuff(temp);
		}
	}

	for (int i = 0; i < nPage; i++)
	{
		if (status[i] == 0)
		{
			number[i] = (index - 1) / (buffers[i].getSizeBuffer() / sizeof(T)) + 1;
			status[i] = 1;
			return buffers[i].get(index);
		}
	}
	srand(time(NULL));

	int r = rand() % nPage;
	
	number[r] = (index - 1) / (buffers[r].getSizeBuffer() / sizeof(T)) + 1;

	return buffers[r].get(index);
}

#endif // !LOOK_VIRTUAL_MEMORY
