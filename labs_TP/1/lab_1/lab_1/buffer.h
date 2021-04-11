#pragma once
#include <iostream>
#include <fstream>
#include <stdexcept>

using namespace std;

#ifndef LOOK_BUFFER
#define LOOK_BUFFER

template <typename T>
class buffer
{
public:
	buffer();
	buffer(const char* str, long sizeBuffer);
	void init(const char* str, long sizeBuffer);
	void setBuff(long index,T element);
	void set(long index,T element);
	T getBuff(long index);
	long getSizeBuffer();
	void swap(int page);
	T get(long index);
	int getSize();
	long getPage();
	void unswap();
	bool empty();
	~buffer();
private:
	void cleanBuf();
	void fileSize();

	const char* name_File;

	long m_sizeBuffer;
	char* m_buffer;

	bool m_empty;

	int sizeFile;

	int m_bufnum;
};

template<typename T>
inline buffer<T>::buffer()
{
	m_sizeBuffer = 0;
	name_File = nullptr;
	m_empty = true;
	this->m_buffer = nullptr;
	m_bufnum = -1;
}

template <typename T>
buffer<T>::buffer(const char* str,long sizeBuffer)
{
	init(str, sizeBuffer);

	m_empty = true;
	
	m_bufnum = -1;

}

template <typename T>
buffer<T>::~buffer()
{
	if (!m_empty)
		unswap();
}

template<typename T>
inline void buffer<T>::init(const char* str, long sizeBuffer)
{
	name_File = str;

	m_sizeBuffer = sizeBuffer;

	this->m_buffer = new char[sizeBuffer];

	fileSize();
	
}

template<typename T>
void buffer<T>::swap(int page)
{
	if (!m_empty)
		unswap();

	if (this->m_bufnum == page)
		return;

	this->m_bufnum = page;
		
	fstream m_file(name_File, ios::binary | ios::out | ios::in);

	m_file.seekg(0, ios::beg);

	if(m_buffer == nullptr)
		this->m_buffer = new char[m_sizeBuffer];
	
	m_file.seekg(page * m_sizeBuffer, ios::beg);
	
	m_file.read(this->m_buffer, m_sizeBuffer);

	m_empty = false;

	m_file.close();

}

template<typename T>
inline void buffer<T>::unswap()
{
	if (m_empty)
		return;

	fstream m_file(name_File, ios::binary | ios::out | ios::in);

	m_file.seekg(0, ios::beg);

	m_file.seekg(m_bufnum * m_sizeBuffer, ios::beg);

	m_file.write(m_buffer, m_sizeBuffer);

	m_file.close();

	cleanBuf();
	   
}

template<typename T>
inline bool buffer<T>::empty()
{
	return this->m_empty;
}

template<typename T>
inline T buffer<T>::getBuff(long index)
{	
	if (m_empty)
		throw new logic_error("The buffer is empty");

	if (index < 1 || index >(m_sizeBuffer / sizeof(T)))
		throw new out_of_range("The index of the element goes beyond the bounds of the array");

	int len = sizeof(T);

	char* inBof = new char[len];

	for (int i = 0; i < len; i++)
	{
		inBof[i] = m_buffer[(index - 1)* len + i];
	}

	T* rez = (T*)inBof;

	return *rez;
}

template<typename T>
inline T buffer<T>::get(long index)
{
	if (index < 1 || index >(sizeFile / sizeof(T)))
		throw new out_of_range("The index of the element goes beyond the bounds of the array");

	int page = (index-1) / (m_sizeBuffer / sizeof(T));

	int newIn = (index-1) % (m_sizeBuffer / sizeof(T));
	
	this->swap(page);
	
	return getBuff(newIn + 1);
}

template<typename T>
inline int buffer<T>::getSize()
{
	return this->sizeFile;
}

template<typename T>
inline long buffer<T>::getPage()
{
	return m_bufnum;
}

template<typename T>
inline void buffer<T>::setBuff(long index, T element)
{
	if (m_empty)
		throw new logic_error("The buffer is empty");
	
	if (index < 1 || index >(m_sizeBuffer / sizeof(T)))
		throw new out_of_range("The index of the element goes beyond the bounds of the array");
	
	char* inBof = (char*)&element;

	int len = sizeof(T);

	for (int i = 0; i < len; i++)
	{
		m_buffer[(index - 1)*len + i ] = inBof[i];
	}
 }

template<typename T>
inline void buffer<T>::set(long index, T element)
{
	if (index < 1 || index > (sizeFile/ sizeof(T)))
		throw new out_of_range("The index of the element goes beyond the bounds of the array");

	int page = (index-1) / (m_sizeBuffer/ sizeof(T));
	
	int newIn = (index-1) % (m_sizeBuffer / sizeof(T));

	this->swap(page);

	setBuff(newIn + 1, element);
}

template<typename T>
inline long buffer<T>::getSizeBuffer()
{
	return m_sizeBuffer;
}

template<typename T>
inline void buffer<T>::cleanBuf()
{
	if (m_buffer == nullptr)
		return;

	delete[] this->m_buffer;

	m_buffer = nullptr;

	m_empty = true;

}

template<typename T>
inline void buffer<T>::fileSize()
{
	fstream m_file(name_File, ios::binary | ios::out | ios::in);

	m_file.seekg(0, ios::end);

	sizeFile = m_file.tellg();

	m_file.close();

}

#endif // !LOOK_BUFFER
