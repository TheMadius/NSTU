#pragma once
#include <iostream>
#include <fstream>

using namespace std;

enum Access {New_List,Existing_List};

template <class T>
class Flist
{
public:

	Flist(const char * name_file, Access status  = New_List);
	void remove(T data);
	void insert(T data);
	void pack();
	void show();
	~Flist();

private:
	void _FILE_WC(Access status, const char* name_file);
	int backPtr(int pos,bool status = false);
	bool changeHead(int pos,int ptrDel);
	void _Load_data(Access status);
	void checkTit(int pos1, int pos2);
	void swap(int pos1,int pos2);
	void skipItem(int pos);
	void deletion(int pos);
	int countActiv();
	int countDel();
	void sortDel();
	void sortActiv();
	int nextPtr(int pos);
	void existingFile();
	int find(T data);
	void newFile();
	void updata();

	fstream* m_file;
	int m_count_app;
	int m_start_list;
	int m_ptr_last_app;
	int m_ptr_first_app;
	int m_ptr_last_del_app;
};

template<class T>
inline Flist<T>::Flist(const char* name_file, Access status)
{
	this->m_start_list = sizeof(m_count_app) + sizeof(m_ptr_first_app) + sizeof(m_ptr_last_del_app)+ sizeof(m_ptr_last_app);
	
	_FILE_WC(status, name_file);

	_Load_data(status);

}

template<class T>
inline void Flist<T>::pack()
{
	sortDel();
	sortActiv();
}

template<class T>
inline void Flist<T>::remove(T data)
{
	int pos = find(data), ptrNext;

	if (pos == -1)
	{
		throw new out_of_range("Not, Data");
	}

	if (pos == this->m_ptr_last_app && pos != this->m_ptr_first_app)
		ptrNext = backPtr(pos);
	else 
		ptrNext = nextPtr(pos);

	if(pos != this->m_ptr_first_app)
		skipItem(pos);

	deletion(pos);

	changeHead(pos,ptrNext);

	updata();

}

template<class T>
inline void Flist<T>::insert(T data)
{
		bool deleteBit = 0;
		int ptr = 0;
		
		if (m_ptr_last_del_app == 0)
		{
			this->m_file->seekg(0, ios::end);

			m_count_app++;
		}
		else
		{
			this->m_file->seekg(m_ptr_last_del_app, ios::beg);

			m_ptr_last_del_app = this->nextPtr(m_ptr_last_del_app);
		}

		int newEnd = m_file->tellg();

		if (this->m_ptr_first_app == 0)
		{
			this->m_ptr_first_app = newEnd;
		}
		
		this->m_file->write((char*)&deleteBit,sizeof(deleteBit));
		this->m_file->write((char*)&ptr,sizeof(ptr));
		this->m_file->write((char*)&data,sizeof(data));

		if (m_ptr_last_app != 0)
		{
			this->m_file->seekg(m_ptr_last_app + sizeof(deleteBit), ios::beg);
			this->m_file->write((char*)&newEnd, sizeof(ptr));
		}

		m_ptr_last_app = newEnd;

		updata();

}

template<class T>
inline void Flist<T>::show()
{
	int temp;
	bool bo;
	int temp2 = 1;
	T temp3;
	this->m_file->seekg(0, ios::beg);

	m_file->read((char*)&temp, sizeof(temp));
	cout << temp<< " ";

	m_file->read((char*)&temp, sizeof(temp));
	cout << temp << " ";

	m_file->read((char*)&temp, sizeof(temp));
	cout << temp << " ";

	m_file->read((char*)&temp, sizeof(temp));
	cout << temp << " ";
	cout << endl;

	temp = 0;
	while (this->m_count_app > temp)
	{
		this->m_file->read((char*)&bo, sizeof(bo));
		cout << bo << " ";
		this->m_file->read((char*)&temp2, sizeof(temp2));
		cout << temp2 << " ";
		this->m_file->read((char*)&temp3, sizeof(temp3));
		cout << temp3 << " ";
		cout << endl;
		temp++;
	}
	cout << endl;
}

template<class T>
inline Flist<T>::~Flist()
{
	m_file->close();
	delete m_file;
}

template<class T>
inline int Flist<T>::find(T data)
{
	T dat;
	bool flag;
	int ptrOld = 0, ptrNew = 0;

	this->m_file->seekg(this->m_ptr_first_app, ios::beg);

	while (true)
	{
		this->m_file->read((char*)&flag, sizeof(flag));
		this->m_file->read((char*)&ptrNew, sizeof(ptrNew));
		this->m_file->read((char*)&dat, sizeof(dat));

		if (dat == data)
		{
			if (ptrOld == 0)
			{
				ptrOld = this->m_ptr_first_app;
			}

			return ptrOld;
		}

		if (ptrNew == 0)
		{
			return -1;
		}
		ptrOld = ptrNew;

		this->m_file->seekg(ptrOld, ios::beg);

	}
	
}

template<class T>
inline bool Flist<T>::changeHead(int pos,int ptrDel)
{
	bool t = false;
	if (pos == this->m_ptr_first_app)
	{
		this->m_ptr_first_app = ptrDel;
		t = true;
	}

	if (pos == this->m_ptr_last_app)
	{
		this->m_ptr_last_app = ptrDel;
		t = true;
	}

	return t;
}

template<class T>
inline void Flist<T>::updata()
{
	m_file->seekp(0, ios::beg);

	m_file->write((char*)&m_count_app, sizeof(m_count_app));

	m_file->write((char*)&m_ptr_first_app, sizeof(m_ptr_first_app));

	m_file->write((char*)&m_ptr_last_app, sizeof(m_ptr_last_app));

	m_file->write((char*)&m_ptr_last_del_app, sizeof(m_ptr_last_del_app));

}

template<class T>
inline int Flist<T>::nextPtr(int pos)
{
	int nowPos = m_file->tellg(), ptrNext;

	this->m_file->seekg(pos + sizeof(bool), ios::beg);
	this->m_file->read((char*)&ptrNext, sizeof(ptrNext));

	this->m_file->seekg(nowPos, ios::beg);

	return ptrNext;
}

template<class T>
inline int Flist<T>::backPtr(int pos,bool status)
{
	int start, nowPos = m_file->tellg(), ptrOld;

	if (pos == this->m_ptr_first_app || pos == this->m_ptr_last_del_app)
	{
		return -1;
	}

	if (status)
		start = this->m_ptr_last_del_app;
	else
		start = this->m_ptr_first_app;

	ptrOld = start;

	this->m_file->seekg(start + sizeof(bool), ios::beg);

	while (true)
	{
		int ptrNew;
		this->m_file->read((char*)&ptrNew, sizeof(ptrNew));

		if (ptrNew == pos)
		{
			this->m_file->seekg(nowPos, ios::beg);
			return ptrOld;
		}

		ptrOld = ptrNew;

		this->m_file->seekg(ptrOld + sizeof(bool), ios::beg);
	}
}

template<class T>
inline void Flist<T>::skipItem(int pos)
{

	int ptrDel = nextPtr(pos);

	int ptrNew = backPtr(pos);

	this->m_file->seekg(ptrNew + sizeof(bool), ios::beg);
	this->m_file->write((char*)&ptrDel, sizeof(ptrDel));

}

template<class T>
inline void Flist<T>::deletion(int pos)
{
	bool del = true;

	this->m_file->seekg(pos, ios::beg);
	this->m_file->write((char*)&del, sizeof(del));
	this->m_file->write((char*)&m_ptr_last_del_app, sizeof(m_ptr_last_del_app));

	m_ptr_last_del_app = pos;
}

template<class T>
inline int Flist<T>::countActiv()
{
	return this->m_count_app - countDel();
}

template<class T>
inline int Flist<T>::countDel()
{
	int count = 0;
	int firstDel = this->m_ptr_last_del_app;

	while (firstDel != 0)
	{
		firstDel = nextPtr(firstDel);
		count++;
	}

	return count;
}

template<class T>
inline void Flist<T>::sortDel()
{
	int sizeData = sizeof(bool) + sizeof(int) + sizeof(T),
		countDel = this->countDel(),
		preOnDel = this->m_ptr_last_del_app;

	int TotalSizeFile = this->m_start_list + sizeData * this->m_count_app;

	while (countDel != 0)
	{
		int next = nextPtr(preOnDel);

		int posSwap = TotalSizeFile - ((countDel)*sizeData);

		if (posSwap != preOnDel)
			swap(posSwap, preOnDel);

		if (next != posSwap)
			preOnDel = next;

		countDel--;
	}

}

template<class T>
inline void Flist<T>::sortActiv()
{
	int sizeData = sizeof(bool) + sizeof(int) + sizeof(T),
		countApp = this->countActiv(),
		preOnApp = this->m_ptr_first_app,
		now = this->m_start_list;

	while (countApp != 0)
	{
		int next = nextPtr(preOnApp);

		if (now != preOnApp)
			swap(preOnApp, now);

		if (next != now)
			preOnApp = next;

		countApp--;

		now += sizeData;
	}
}

template<class T>
inline void Flist<T>::swap(int pos1, int pos2)
{
	int backPos1, backPos2;
	int ptr1, ptr2;
	bool flag1, flag2;
	T Data1, Data2;

	this->m_file->seekg(pos1, ios::beg);

	this->m_file->read((char*)&flag1, sizeof(flag1));
	this->m_file->read((char*)&ptr1, sizeof(ptr1));
	this->m_file->read((char*)&Data1, sizeof(Data1));

	this->m_file->seekg(pos2, ios::beg);

	this->m_file->read((char*)&flag2, sizeof(flag2));
	this->m_file->read((char*)&ptr2, sizeof(ptr2));
	this->m_file->read((char*)&Data2, sizeof(Data2));

	backPos1 = backPtr(pos1, flag1);
	backPos2 = backPtr(pos2, flag2);

	if (backPos2 == pos1)
		backPos2 = pos2;

	if (backPos1 == pos2)
		backPos1 = pos1;

	this->m_file->seekg(pos2, ios::beg);

	this->m_file->write((char*)&flag1, sizeof(flag1));
	this->m_file->write((char*)&ptr1, sizeof(ptr1));
	this->m_file->write((char*)&Data1, sizeof(Data1));

	this->m_file->seekg(pos1, ios::beg);

	this->m_file->write((char*)&flag2, sizeof(flag2));
	this->m_file->write((char*)&ptr2, sizeof(ptr2));
	this->m_file->write((char*)&Data2, sizeof(Data2));

	if(backPos1 != -1)
	{ 
		this->m_file->seekg(backPos1 + sizeof(bool), ios::beg);
		this->m_file->write((char*)&pos2, sizeof(ptr2));
	}
		
	if (backPos2 != -1)
	{
		this->m_file->seekg(backPos2 + sizeof(bool), ios::beg);
		this->m_file->write((char*)&pos1, sizeof(pos1));
	}
	
	checkTit(pos1,pos2);

	updata();

}

template<class T>
inline void Flist<T>::_Load_data(Access status)
{
	switch (status)
	{
	case New_List:
		{
			newFile();
			break;
		}
	case Existing_List:
		{
			existingFile();
			break;
		}
	}
}

template<class T>
inline void Flist<T>::_FILE_WC(Access status,const char* name_file)
{
	switch (status)
	{
	case New_List:
		{
			std::ofstream oFile(name_file);
			oFile.close();
			break;
		}
	default:break;
	}

	m_file = new fstream(name_file, ios::binary | ios::in | ios::out);
	m_file->seekp(0, ios::beg);

	if (!m_file->is_open())
	{
		throw new std::exception("Error, file cann't be opened");
	}

}

template<class T>
inline void Flist<T>::newFile()
{
	m_count_app = 0;

	m_ptr_first_app = m_start_list;

	m_ptr_last_del_app = 0;

	m_ptr_last_app = 0;

	m_file->write((char*)&m_count_app, sizeof(m_count_app));

	m_file->write((char*)&m_ptr_first_app, sizeof(m_ptr_first_app));

	m_file->write((char*)&m_ptr_last_app, sizeof(m_ptr_last_app));

	m_file->write((char*)&m_ptr_last_del_app, sizeof(m_ptr_last_del_app));

}

template<class T>
inline void Flist<T>::existingFile()
{
	
	m_file->read((char*)&m_count_app, sizeof(m_count_app));

	m_file->read((char*)&m_ptr_first_app, sizeof(m_ptr_first_app));

	m_file->read((char*)&m_ptr_last_app, sizeof(m_ptr_last_app));

	m_file->read((char*)&m_ptr_last_del_app, sizeof(m_ptr_last_del_app));
}

template<class T>
inline void Flist<T>::checkTit(int pos1, int pos2)
{
	if (pos1 == this->m_ptr_first_app)
		this->m_ptr_first_app = pos2;
	else if (pos2 == this->m_ptr_first_app)
		this->m_ptr_first_app = pos1;

	if (pos1 == this->m_ptr_last_app)
		this->m_ptr_last_app = pos2;
	else if (pos2 == this->m_ptr_last_app)
		this->m_ptr_last_app = pos1;

	if (pos1 == this->m_ptr_last_del_app)
		this->m_ptr_last_del_app = pos2;
	else if (pos2 == this->m_ptr_last_del_app)
		this->m_ptr_last_del_app = pos1;
}
