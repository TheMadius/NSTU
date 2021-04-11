#pragma once
#include <fstream>
#include <iostream>
#pragma warning(disable:4996)
using namespace std;

enum Access { New_Tree, Existing_Tree };

struct BeginFile
{
	int m_size_page;
	int m_coutn_page;
	int m_order_tree;
	int m_ptr_root_page;
	int m_ptr_last_del_page;

	BeginFile()
	{
		m_size_page = 0;
		m_coutn_page = 0;
		m_order_tree = 0;
		m_ptr_root_page = 0;
		m_ptr_last_del_page = 0;
	}
};

template <typename T>
struct Data
	{
		int ptr;
		T data;
		Data()
		{
			ptr = 0;
			data = T();
		}
	};

template <typename T>
class Btree
{
public:
	Btree(const char* name_file, Access status = Existing_Tree,int order = 4);
	void insert(T data);
	void remove(T data);
	void pack();
	void show();

private:
	void delPage(int ptrPage);
	Data<T> tossing(int ptrDonor,int ptrRecipient, Data<T> data);
	void moves(int ptrDonor,int ptrRecipient);
	void movesAll(int ptrDonor,int ptrRecipient);
	void _FILE_WC(Access status, const char* name_file);
	int addDataPage(int prtPage, Data<T> data);
	void _Load_data(Access status, int order);
	int ptrPort(int left, int reight);
	void addStart(int ptr1, int ptr2);
	void replaceData(T data, T data2);
	bool isLeft(int ptr,int ptrOther);
	void movesDel(int add,int del);
	int findAncestor(int data);
	int countAdd(int ptrPage);
	int findAdjacent(int ptrNow);
	int newPage(int prt = 0);
	void newFile(int order);
	int findReplace(T data);
	void deleteItem(T data);
	bool isDelete(int prt);
	T removeBeg(int pos);
	int backPtr(int pos);
	int findPage(T data);
	int nextPtr(int pos);
	void _Existing_File();
	int find(T data);
	int countDel();
	void updata();

	fstream* m_file;
	BeginFile Start;
	int m_start_tree;
};

template<typename T>
inline Btree<T>::Btree(const char* name_file, Access status, int order)
{
	this->m_start_tree = sizeof(Start);

	_FILE_WC(status, name_file);

	_Load_data(status, order);

}

template<typename T>
inline void Btree<T>::insert(T data)
{
	Data<T> res;
	res.data = data;
	
	if (Start.m_ptr_root_page == 0)
	{
		Start.m_ptr_root_page = newPage();
		addDataPage(Start.m_ptr_root_page, res);
		updata();
		return;
	}

	if (find(data) != -1)
	{
		return;
	}

	int pageInsert, pageAncestor;
	pageInsert = this->findPage(res.data);

	while (true)
	{
		if (this->countAdd(pageInsert) == Start.m_size_page)
		{
			pageAncestor = this->findAncestor(pageInsert);
			int ptrNew = newPage();
			res = tossing(pageInsert, ptrNew, res);
			addStart(res.ptr, ptrNew);
			res.ptr = ptrNew;

			if (pageAncestor == 0)
			{
				int ptrNew2 = newPage(pageInsert);
				Start.m_ptr_root_page = ptrNew2;
				addDataPage(ptrNew2, res);
				break;
			}
			pageInsert = pageAncestor;
		}
		else
		{
			addDataPage(pageInsert, res);
			break;
		}	
	}
	updata();
}

template<typename T>
inline void Btree<T>::remove(T data)
{
	int posDel = findReplace(data), posAbj = 0;
	int size;

	if (posDel != 0)
	{
		T dataRe = removeBeg(posDel);
		replaceData(data, dataRe);
	}
	else
	{
		posDel = find(data);
		deleteItem(data);
	}

	while (true)
	{
		size = countAdd(posDel);

		if (posDel == Start.m_ptr_root_page)
		{
			if (countAdd(posDel) == 0)
			{
				delPage(posDel);
				Start.m_ptr_root_page = posAbj;
				break;
			}
			else
				break;
		}

		if (size < Start.m_order_tree)
		{
			posAbj = findAdjacent(posDel);

			if (countAdd(posAbj) > Start.m_order_tree)
			{
				moves(posAbj, posDel);
			}
			else
			{
				if (!isLeft(posAbj, posDel))
					swap(posAbj, posDel);

				movesAll(posDel, posAbj);
				delPage(posDel);
			}
		}

		int newtPotr = findAncestor(posAbj);

		if (countAdd(newtPotr) >= Start.m_order_tree)
			break;

		posDel = newtPotr;
	}
	updata();
}

template<typename T>
inline void Btree<T>::pack()
{
	int ptr, count, ptrNow, delNext;
	Data<T> dat;
	this->m_file->seekg(0, ios::end);
	int totolSize = this->m_file->tellg();
	int sizePage = sizeof(int) + sizeof(int) + Start.m_size_page * sizeof(Data<T>);
	int countDl = countDel();

	int first = Start.m_ptr_last_del_page;

	for (int i = 0; i < countDl; ++i)
	{
		int nowStat = totolSize - sizePage * (countDl - i);

		int back = backPtr(first);

		if (!isDelete(nowStat))
		{

			int posRe = findAncestor(nowStat);

			if (posRe == 0)
			{
				Start.m_ptr_root_page = first;
			}
			else
			{
				this->m_file->seekg(posRe, ios::beg);
				this->m_file->read((char*)&ptr, sizeof(ptr));
				this->m_file->read((char*)&count, sizeof(count));

				if (nowStat == ptr)
				{
					dat.ptr = first;
					this->m_file->seekg(posRe, ios::beg);
					this->m_file->read((char*)&ptr, sizeof(ptr));
					count = 0;
				}

				for (int i = 0; i < count; ++i)
				{
					ptrNow = m_file->tellg();
					this->m_file->read((char*)&dat, sizeof(dat));
					if (dat.ptr == nowStat)
					{
						dat.ptr = first;
						this->m_file->seekg(ptrNow, ios::beg);
						this->m_file->write((char*)&dat, sizeof(dat));
						break;
					}
				}
			}
		}
		else
		{
			int back2 = backPtr(nowStat);

			if (nextPtr(first) == nowStat)
			{
				addStart(first, first);
			}

			addStart(first, back2);
		}

		if (first != nowStat)
			movesDel(nowStat, first);


		if (i == 0)
		{
			Start.m_ptr_last_del_page = nowStat;
		}
		else
		{
			addStart(nowStat, back);
		}
		first = nextPtr(nowStat);
	}
	updata();

}

template<typename T>
inline T Btree<T>::removeBeg(int pos)
{
	int prtInsert, ptr, count, ptrDel;
	Data<T> dat, Del;

	this->m_file->seekg(pos, ios::beg);
	this->m_file->read((char*)&ptr, sizeof(ptr));
	this->m_file->read((char*)&count, sizeof(count));

	prtInsert = this->m_file->tellg();

	this->m_file->read((char*)&Del, sizeof(Del));

	ptrDel = this->m_file->tellg();

	for (int i = 0; i < count-1; ++i)
	{
		this->m_file->seekg(ptrDel, ios::beg);
		this->m_file->read((char*)&dat, sizeof(dat));
		ptrDel = this->m_file->tellg();;
		this->m_file->seekg(prtInsert, ios::beg);
		this->m_file->write((char*)&dat, sizeof(dat));
		prtInsert = this->m_file->tellg();;
	}

	count--;

	this->m_file->seekg(pos, ios::beg);
	this->m_file->write((char*)&ptr, sizeof(ptr));
	this->m_file->write((char*)&count, sizeof(count));

	return Del.data;
}

template<typename T>
inline void Btree<T>::replaceData(T data, T data2)
{
	int ptrPage, prtInsert, ptr, count,ptrNow;
	Data<T> dat;

	ptrPage = Start.m_ptr_root_page;

	while (true)
	{
		this->m_file->seekg(ptrPage, ios::beg);
		this->m_file->read((char*)&ptr, sizeof(ptr));
		this->m_file->read((char*)&count, sizeof(count));

		prtInsert = ptr;

		for (int i = 0; i < count; ++i)
		{
			ptrNow = this->m_file->tellg();
			this->m_file->read((char*)&dat, sizeof(dat));

			if (data == dat.data)
			{
				dat.data = data2;
				this->m_file->seekg(ptrNow, ios::beg);
				this->m_file->write((char*)&dat, sizeof(dat));
				return;
			}

			if (data <= dat.data)
				break;

			prtInsert = dat.ptr;
		}
		ptrPage = prtInsert;
	}

}

template<typename T>
inline int Btree<T>::findReplace(T data)
{
	int ptrPage, ptr, count;
	int pos = find(data);
	Data<T> dat;

	if (pos == -1)
	{
		throw new std::exception("Error, no data");
	}

	this->m_file->seekg(pos, ios::beg);
	this->m_file->read((char*)&ptr, sizeof(ptr));
	this->m_file->read((char*)&count, sizeof(count));

	for (int i = 0; i < count; ++i)
	{
		this->m_file->read((char*)&dat, sizeof(dat));
		ptrPage = dat.ptr;
		if (data == dat.data)
			break;
	}

	while (ptrPage != 0)
	{
		this->m_file->seekg(ptrPage, ios::beg);
		this->m_file->read((char*)&ptr, sizeof(ptr));

		if (ptr == 0)
			break;

		ptrPage = ptr;
	}

	return ptrPage;
}

template<typename T>
inline bool Btree<T>::isLeft(int ptr,int ptrOther)
{
	int prePtr = findAncestor(ptr), pre1, pre2, count;
	Data<T> dat;

	this->m_file->seekg(prePtr, ios::beg);
	this->m_file->read((char*)&pre1, sizeof(pre1));
	this->m_file->read((char*)&count, sizeof(count));

	for (int i = 0; i < count; ++i)
	{
		this->m_file->read((char*)&dat, sizeof(dat));
		pre2 = dat.ptr;
		if (ptr == pre2 && ptrOther == pre1)
			return false ;
		if (ptr == pre1 && ptrOther == pre2)
			return true;

		pre1 = pre2;
	}
}

template<typename T>
inline int Btree<T>::countDel()
{
	int count = 0;

	int first = Start.m_ptr_last_del_page;

	while (first != 0)
	{
		this->m_file->seekg(first, ios::beg);
		this->m_file->read((char*)&first,sizeof(first));
		count++;
	}
	return count;
}

template<typename T>
inline int Btree<T>::find(T data)
{
	int ptrPage, prtInsert, ptr, count;
	Data<T> dat;

	ptrPage = Start.m_ptr_root_page;

	while (true)
	{
		this->m_file->seekg(ptrPage, ios::beg);
		this->m_file->read((char*)&ptr, sizeof(ptr));
		this->m_file->read((char*)&count, sizeof(count));

		prtInsert = ptr;

		for (int i = 0; i < count; ++i)
		{
			this->m_file->read((char*)&dat, sizeof(dat));

			if (data == dat.data)
				return ptrPage;

			if (data <= dat.data)
				break;

			prtInsert = dat.ptr;
		}

		if (prtInsert == 0)
		{
			return -1;
		}

		ptrPage = prtInsert;
	}
}

template<typename T>
inline int Btree<T>::findAdjacent(int ptrNow)
{
	int prePtr = findAncestor(ptrNow),pre1, pre2, count;
	Data<T> dat;

	this->m_file->seekg(prePtr, ios::beg);
	this->m_file->read((char*)&pre1, sizeof(pre1));
	this->m_file->read((char*)&count, sizeof(count));

	for (int i = 0; i < count; ++i)
	{
		this->m_file->read((char*)&dat, sizeof(dat));
		pre2 = dat.ptr;
		if (ptrNow == pre2)
			return pre1;
		if (ptrNow == pre1)
			return pre2;

		pre1 = pre2;
	}
}

template<typename T>
inline int Btree<T>::newPage(int prt)
{
	int ptr = prt,count = 0,ptrPage;
	Data<T> dat;

	if (this->Start.m_ptr_last_del_page == 0)
	{
		this->Start.m_coutn_page++;
		this->m_file->seekg(0, ios::end);
	}
	else
	{
		this->m_file->seekg(this->Start.m_ptr_last_del_page, ios::beg);
		this->m_file->read((char*)&ptrPage, sizeof(ptrPage));
		this->m_file->seekg(this->Start.m_ptr_last_del_page, ios::beg);
		Start.m_ptr_last_del_page = ptrPage;
	}

	ptrPage = this->m_file->tellg();

	this->m_file->write((char*)&ptr,sizeof(ptr));
	this->m_file->write((char*)&count, sizeof(count));
	for (int i = 0; i < Start.m_size_page; ++i)
	{
		this->m_file->write((char*)&dat, sizeof(dat));
	}

	this->m_file->seekg(0, ios::beg);

	updata();

	return ptrPage;
}

template<typename T>
inline void Btree<T>::show()
{
	int ptr, sizepage, count, temp, ptrNow;
	BeginFile Fil;
	Data<T> dat;

	this->m_file->seekg(0, ios::beg);

	m_file->read((char*)&Fil, sizeof(Fil));

	sizepage = sizeof(int) + sizeof(int) + sizeof(dat) * Fil.m_size_page;

	cout << Fil.m_order_tree << " ";
	cout << Fil.m_coutn_page << " ";
	cout << Fil.m_ptr_last_del_page << " ";
	cout << Fil.m_ptr_root_page << " ";
	cout << Fil.m_size_page << " ";
	cout << endl;

	temp = 0;

	ptrNow = this->m_file->tellg();

	while (this->Start.m_coutn_page > temp)
	{
		cout << ptrNow << "  ::  ";

		this->m_file->seekg(ptrNow,ios::beg);

		this->m_file->read((char*)&ptr, sizeof(ptr));
		this->m_file->read((char*)&count, sizeof(count));

		cout << ptr << " ";
		cout << count << " : ";

		for (int i = 0; i < count; ++i)
		{
			this->m_file->read((char*)&dat, sizeof(dat));

			cout << "d="<<dat.data << " ";
			cout << "p="<<dat.ptr << " /";
		}
		temp++;
		ptrNow += sizepage;
		cout << endl;
	}
		cout << endl;
}

template<typename T>
inline void Btree<T>::_Load_data(Access status, int order)
{
	switch (status)
	{
		case New_Tree:
		{
			newFile(order);
			break;
		}
		case Existing_Tree:
		{
			_Existing_File();
			break;
		}
	}
}

template<typename T>
inline int Btree<T>::ptrPort(int left, int reight)
{
	int prePtr = findAncestor(left), pre1, pre2, count,ptrElem;
	Data<T> dat;

	this->m_file->seekg(prePtr, ios::beg);
	this->m_file->read((char*)&pre1, sizeof(pre1));
	this->m_file->read((char*)&count, sizeof(count));

	for (int i = 0; i < count; ++i)
	{
		ptrElem = m_file->tellg();
		this->m_file->read((char*)&dat, sizeof(dat));
		pre2 = dat.ptr;

		if (left == pre2 && reight == pre1 || left == pre1 && reight == pre2)
			return ptrElem;

		pre1 = pre2;
	}
}

template<typename T>
inline void Btree<T>::_Existing_File()
{
	m_file->read((char*)&Start, sizeof(Start));
}

template<typename T>
inline int Btree<T>::findPage(T data)
{
	int ptrPage, prtInsert,ptr, count;
	Data<T> dat;
	ptrPage = Start.m_ptr_root_page;

	while (true)
	{
		this->m_file->seekg(ptrPage,ios::beg);
		this->m_file->read((char*)&ptr, sizeof(ptr));
		this->m_file->read((char*)&count, sizeof(count));

		prtInsert = ptr;

		for (int i = 0; i < count; ++i)
		{
			this->m_file->read((char*)&dat, sizeof(dat));
			if (data < dat.data)
			{
				break;
			}
			prtInsert = dat.ptr;
		}
		if (prtInsert == 0)
		{
			break;
		}
		ptrPage = prtInsert;
	}
	return ptrPage;
}

template<typename T>
inline void Btree<T>::delPage(int ptrPage)
{
	this->m_file->seekg(ptrPage,ios::beg);
	this->m_file->write((char*)&Start.m_ptr_last_del_page, sizeof(Start.m_ptr_last_del_page));

	Start.m_ptr_last_del_page = ptrPage;

	updata();
}

template<typename T>
inline Data<T> Btree<T>::tossing(int ptrDonor, int ptrRecipient, Data<T> data)
{
	int ptr1, count1, ptr2, count2, prtInsert, prtInsert2;
	Data<T> dat, dataNew,average;

	dataNew = data;

	this->m_file->seekg(ptrDonor, ios::beg);

	this->m_file->read((char*)&ptr1, sizeof(ptr1));
	this->m_file->read((char*)&count1, sizeof(count1));

	prtInsert = m_file->tellg();

	this->m_file->seekg(ptrRecipient, ios::beg);

	this->m_file->read((char*)&ptr2, sizeof(ptr2));
	this->m_file->read((char*)&count2, sizeof(count2));

	prtInsert2 = m_file->tellg();

	for (int i = 0; i < count1/2 + 1; ++i)
	{
		this->m_file->seekg(prtInsert, ios::beg);
		this->m_file->read((char*)&dat, sizeof(dat));

		if (dataNew.data < dat.data)
		{
			this->m_file->seekg(prtInsert, ios::beg);
			this->m_file->write((char*)&dataNew, sizeof(dataNew));
			average = dataNew;
			dataNew = dat;
		}
		else
		{
			average = dat;
		}
		prtInsert = m_file->tellg();
	}

	for (int i = 0; i < count1 / 2 - 1; ++i)
	{
		this->m_file->seekg(prtInsert, ios::beg);
		this->m_file->read((char*)&dat, sizeof(dat));
		prtInsert = m_file->tellg();

		if (dataNew.data < dat.data)
		{
			this->m_file->seekg(prtInsert2, ios::beg);
			this->m_file->write((char*)&dataNew, sizeof(dataNew));
			dataNew = dat;
		}
		else
		{
			this->m_file->seekg(prtInsert2, ios::beg);
			this->m_file->write((char*)&dat, sizeof(dat));
		}
		prtInsert2 = m_file->tellg();
	}

	this->m_file->seekg(prtInsert2, ios::beg);
	this->m_file->write((char*)&dataNew, sizeof(dataNew));

	count1 /= 2;

	this->m_file->seekg(ptrDonor, ios::beg);
	this->m_file->write((char*)&ptr1, sizeof(ptr1));
	this->m_file->write((char*)&count1, sizeof(count1));

	this->m_file->seekg(ptrRecipient, ios::beg);
	this->m_file->write((char*)&ptr2, sizeof(ptr2));
	this->m_file->write((char*)&count1, sizeof(count1));

	return average;
}

template<typename T>
inline void Btree<T>::moves(int ptrDonor, int ptrRecipient)
{
	int ptr,count,posNext;
	int count1 = countAdd(ptrDonor);
	int count2 = countAdd(ptrRecipient);
	int ptrPor = ptrPort(ptrDonor, ptrRecipient);
	int move = (count1 + count2 + 1) / 2 - count2;
	Data<T> dat, dat2;

	for (int i = 0; i < move; ++i)
	{
		m_file->seekg(ptrPor,ios::beg);
		this->m_file->read((char*)&dat, sizeof(dat));

		m_file->seekg(ptrRecipient, ios::beg);
		this->m_file->read((char*)&ptr, sizeof(ptr));

		dat.ptr = ptr;
		addDataPage(ptrRecipient, dat);

		count2++;
		count1--;

		posNext = ptrDonor + sizeof(int) + sizeof(int) + count1 * sizeof(dat);
		m_file->seekg(posNext, ios::beg);
		this->m_file->read((char*)&dat2, sizeof(dat2));

		m_file->seekg(ptrPor, ios::beg);
		this->m_file->read((char*)&dat, sizeof(dat));

		dat.data = dat2.data;

		m_file->seekg(ptrPor, ios::beg);
		this->m_file->write((char*)&dat, sizeof(dat));

		m_file->seekg(ptrRecipient, ios::beg);
		this->m_file->write((char*)&dat2.ptr, sizeof(dat2.ptr));
	}

	m_file->seekg(ptrDonor+ sizeof(int), ios::beg);
	this->m_file->write((char*)&count1, sizeof(count2));

	m_file->seekg(ptrRecipient+sizeof(int), ios::beg);
	this->m_file->write((char*)&count2, sizeof(count2));
}

template<typename T>
inline void Btree<T>::movesAll(int ptrDonor, int ptrRecipient)
{
	int ptr, count, posNext;
	int count1 = countAdd(ptrDonor);
	int count2 = countAdd(ptrRecipient);
	int ptrPor = ptrPort(ptrDonor, ptrRecipient);
	int move = count1;
	Data<T> dat, dat2;

	m_file->seekg(ptrPor, ios::beg);
	this->m_file->read((char*)&dat, sizeof(dat));

	m_file->seekg(ptrDonor, ios::beg);
	this->m_file->read((char*)&ptr, sizeof(ptr));

	dat.ptr = ptr;

	for (int i = 0; i < move; ++i)
	{
		count1--;

		posNext = ptrDonor + sizeof(int) + sizeof(int) + count1 * sizeof(dat);
		m_file->seekg(posNext, ios::beg);
		this->m_file->read((char*)&dat2, sizeof(dat2));

		addDataPage(ptrRecipient, dat2);
		
		count2++;
	}

	addDataPage(ptrRecipient, dat);

	count2++;

	deleteItem(dat.data);

	m_file->seekg(ptrDonor + sizeof(int), ios::beg);
	this->m_file->write((char*)&count1, sizeof(count2));

	m_file->seekg(ptrRecipient + sizeof(int), ios::beg);
	this->m_file->write((char*)&count2, sizeof(count2));
}

template<typename T>
inline int Btree<T>::findAncestor(int data)
{
	int ptrPage, prtInsert, ptr, count, ptrAncestor;
	Data<T> dat,datF;

	ptrPage = Start.m_ptr_root_page;
	ptrAncestor = 0;

	this->m_file->seekg(data, ios::beg);
	this->m_file->read((char*)&ptr, sizeof(ptr));
	this->m_file->read((char*)&count, sizeof(count));

	this->m_file->read((char*)&datF, sizeof(datF));

	while (true)
	{
		this->m_file->seekg(ptrPage, ios::beg);
		this->m_file->read((char*)&ptr, sizeof(ptr));
		this->m_file->read((char*)&count, sizeof(count));

		prtInsert = ptr;

		for (int i = 0; i < count; ++i)
		{
			this->m_file->read((char*)&dat, sizeof(dat));

			if(datF.data == dat.data)
				return ptrAncestor;

			if (datF.data <= dat.data)
				break;

			prtInsert = dat.ptr;
		}
		ptrAncestor = ptrPage;
		ptrPage = prtInsert;
	}
}

template<typename T>
inline void Btree<T>::newFile(int order)
{
	Start.m_size_page = order*2;
	Start.m_order_tree = order;

	m_file->write((char*)&Start, sizeof(Start));
}

template<typename T>
inline int Btree<T>::addDataPage(int prtPage, Data<T> data)
{
	int ptr, count,prtInsert;
	Data<T> dat,dataNew;

	dataNew = data;

	this->m_file->seekg(prtPage, ios::beg);

	this->m_file->read((char*)&ptr, sizeof(ptr));
	this->m_file->read((char*)&count, sizeof(count));

	prtInsert = m_file->tellg();

	for (int i = 0; i < count; ++i)
	{
		this->m_file->seekg(prtInsert, ios::beg);
		this->m_file->read((char*)&dat, sizeof(dat));

		if (dataNew.data < dat.data)
		{
			this->m_file->seekg(prtInsert, ios::beg);
			this->m_file->write((char*)&dataNew, sizeof(dataNew));
			dataNew = dat;
		}
		prtInsert = m_file->tellg();
	}

	count++;

	this->m_file->seekg(prtInsert, ios::beg);
	this->m_file->write((char*)&dataNew, sizeof(dataNew));

	this->m_file->seekg(prtPage, ios::beg);
	this->m_file->write((char*)&ptr, sizeof(ptr));
	this->m_file->write((char*)&count, sizeof(count));

	return 1;
}

template<typename T>
inline int Btree<T>::countAdd(int ptrPage)
{
	int ptr, count;
	this->m_file->seekg(ptrPage, ios::beg);
	this->m_file->read((char*)&ptr, sizeof(ptr));
	this->m_file->read((char*)&count, sizeof(count));
	this->m_file->seekg(0, ios::beg);

	return count;
}

template<typename T>
inline void Btree<T>::updata()
{
	m_file->seekp(0, ios::beg);

	m_file->write((char*)&Start, sizeof(Start));
}

template<typename T>
inline bool Btree<T>::isDelete(int prt)
{
	int first = Start.m_ptr_last_del_page;

	while (first != 0)
	{
		if (prt == first)
			return true;
		this->m_file->seekg(first, ios::beg);
		this->m_file->read((char*)&first, sizeof(first));
	}
	return false;
}

template<typename T>
inline int Btree<T>::backPtr(int pos)
{
	int first = Start.m_ptr_last_del_page;

	if (pos == first)
	{
		return 0;
	}

	while (first != 0)
	{
		if (pos == nextPtr(first))
			return first;
		
		first = nextPtr(first);
	}
}

template<typename T>
inline int Btree<T>::nextPtr(int pos)
{
	this->m_file->seekg(pos, ios::beg);
	this->m_file->read((char*)&pos, sizeof(pos));

	return pos;
}

template<typename T>
inline void Btree<T>::movesDel(int add, int del)
{
	int ptr1, count1, posNext1;
	int ptr2, count2, posNext2;

	 count1 = countAdd(add);
	 count2 = countAdd(del);
	Data<T> dat, dat2;

	m_file->seekg(add, ios::beg);
	this->m_file->read((char*)&ptr1, sizeof(ptr1));
	this->m_file->read((char*)&count1, sizeof(count1));

	posNext1 = this->m_file->tellg();

	m_file->seekg(del, ios::beg);
	this->m_file->read((char*)&ptr2, sizeof(ptr2));
	this->m_file->read((char*)&count2, sizeof(count2));

	posNext2 = this->m_file->tellg();

	for (int i = 0; i < count1; ++i)
	{
		m_file->seekg(posNext1, ios::beg);
		this->m_file->read((char*)&dat2, sizeof(dat2));
		posNext1 = this->m_file->tellg();

		m_file->seekg(posNext2, ios::beg);
		this->m_file->write((char*)&dat2, sizeof(dat2));
		posNext2 = this->m_file->tellg();
	}

	m_file->seekg(del, ios::beg);
	this->m_file->write((char*)&ptr1, sizeof(ptr1));
	this->m_file->write((char*)&count1, sizeof(count1));

	m_file->seekg(add, ios::beg);
	this->m_file->write((char*)&ptr2, sizeof(ptr2));
	this->m_file->write((char*)&count2, sizeof(count2));
}

template<typename T>
inline void Btree<T>::deleteItem(T data)
{
	int prtInsert, ptr, count, ptrDel ,i;
	int ptrD = find(data);
	Data<T> dat,dat2;

	this->m_file->seekg(ptrD, ios::beg);
	this->m_file->read((char*)&ptr, sizeof(ptr));
	this->m_file->read((char*)&count, sizeof(count));

	for ( i = 0; i < count - 1; ++i)
	{
		prtInsert = this->m_file->tellg();
		this->m_file->read((char*)&dat, sizeof(dat));
		ptrDel = this->m_file->tellg();	
		if (dat.data >= data)
		{
			this->m_file->read((char*)&dat2, sizeof(dat2));
			this->m_file->seekg(prtInsert,ios::beg);
			this->m_file->write((char*)&dat2, sizeof(dat2));
			this->m_file->seekg(ptrDel,ios::beg);
		}
	}

	count--;

	this->m_file->seekg(ptrD, ios::beg);
	this->m_file->write((char*)&ptr, sizeof(ptr));
	this->m_file->write((char*)&count, sizeof(count));

}

template<typename T>
inline void Btree<T>::addStart(int ptr1, int ptr2)
{
	this->m_file->seekg(ptr2, ios::beg);
	this->m_file->write((char*)&ptr1, sizeof(ptr1));

}

template<typename T>
inline void Btree<T>::_FILE_WC(Access status, const char* name_file)
{
	if (status == New_Tree)
	{
		std::ofstream oFile(name_file);
		oFile.close();
	}
		
	m_file = new fstream(name_file, ios::binary | ios::in | ios::out);

	m_file->seekp(0, ios::beg);

	if (!m_file->is_open())
	{
		throw new std::exception("Error, file cann't be opened");
	}
}
