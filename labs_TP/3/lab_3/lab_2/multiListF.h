#pragma once
#include <iostream>
#include <fstream>
#pragma warning(disable:4996)

using namespace std;

enum Access {New_List,Existing_List};

template <class T>
class multiListF
{
public:
	multiListF(const char * name_file, Access status  = New_List);
	void remove(T data);
	void insert(T data);
	void pack();
	void show();
	void link(T data1,T data2, int multi);
	void print(T data);
	void showConf();
	~multiListF();
private:
	template <class T>
	struct Data
	{
		bool flag;
		int ptr;
		T data;
		int prtConf;

		Data(T data = T())
		{
			flag = false;
			ptr = 0;
			this->data = data;
			prtConf = 0;
		}
	};

	struct DataConf
	{
		bool flag;
		int ptr;
		int ptrProduct;
		int multi;
		DataConf()
		{
			flag = false;
			 ptr = 0;
			 ptrProduct = 0;
			 multi = 0;
		}
	};

	struct BeginFile
	{
		int m_count_app;
		int m_ptr_last_app;
		int m_ptr_first_app;
		int m_ptr_last_del_app;
		char name_conf[16];
		BeginFile()
		{
			 m_count_app = 0;
			 m_ptr_last_app = 0;
			 m_ptr_first_app = 0;
			 m_ptr_last_del_app = 0;
		}
	};

	struct BeginConfig
	{
		int m_count_app;
		int m_ptr_first_app;
		int m_ptr_last_del_app;
		BeginConfig()
		{
			m_count_app = 0;
			m_ptr_first_app = 0;
			m_ptr_last_del_app = 0;
		}
	};

	int find(T data);

	int _link(int start,int ptr,int multi);

	void _FILE_WC(Access status, const char* name_file);
	int backPtr(int pos,bool status = false);
	void changeConfig(int pos1, int pos2);
	void checkTit(int pos1, int pos2);
	bool changeHead(int pos,int ptrDel);
	void _Load_data(Access status);
	void swap(int pos1,int pos2);
	int firstOccurrence(int prt);
	int nextPtrConf(int pos);
	void skipItem(int pos);
	int endList(int start);
	void deletion(int pos);
	int nextPtr(int pos);
	void existingFile();
	int countActiv();
	void sortActiv();
	int countDel();
	void sortDel();
	void newFile();
	void updata();
	void updataConf();

	fstream* m_file;
	fstream* m_file_config;

	int m_start_list;

	BeginFile Start;
	BeginConfig ConfStart;
	
};

template<class T>
inline multiListF<T>::multiListF(const char* name_file, Access status)
{
	this->m_start_list = sizeof(Start) ;
	
	_FILE_WC(status, name_file);

	_Load_data(status);

}

template<class T>
inline void multiListF<T>::pack()
{
	sortDel();
	sortActiv();
}

template<class T>
inline void multiListF<T>::remove(T data)
{
	int pos = find(data), ptrDel;
	Data<T> dat;

	if (pos == -1)
	{
		throw new out_of_range("Not, Data");
	}

	int posConf = firstOccurrence(pos);

	this->m_file->seekg(pos,ios::beg);
	this->m_file->read((char*)&dat, sizeof(dat));

	if (posConf != 0 || dat.prtConf != 0)
	{
		throw new std::logic_error("Error, there is a pointer in the configuration");
	}

	if (pos == this->Start.m_ptr_last_app && pos != this->Start.m_ptr_first_app)
		ptrDel = backPtr(pos);
	else 
		ptrDel = nextPtr(pos);

	if(pos != this->Start.m_ptr_first_app)
		skipItem(pos);

	deletion(pos);

	changeHead(pos,ptrDel);

	updata();

}

template<class T>
inline void multiListF<T>::insert(T data)
{
		Data<T> dat(data);
	
		if (Start.m_ptr_last_del_app == 0)
		{
			this->m_file->seekg(0, ios::end);

			Start.m_count_app++;
		}
		else
		{
			this->m_file->seekg(Start.m_ptr_last_del_app, ios::beg);

			Start.m_ptr_last_del_app = this->nextPtr(Start.m_ptr_last_del_app);
		}

		int newEnd = m_file->tellg();

		if (this->Start.m_ptr_first_app == 0)
		{
			this->Start.m_ptr_first_app = newEnd;
		}
		
		this->m_file->write((char*)&dat,sizeof(dat));
		
		if (Start.m_ptr_last_app != 0)
		{
			this->m_file->seekg(Start.m_ptr_last_app, ios::beg);
			this->m_file->read((char*)&dat, sizeof(dat));
			dat.ptr = newEnd;
			this->m_file->seekg(Start.m_ptr_last_app, ios::beg);
			this->m_file->write((char*)&dat, sizeof(dat));
		}

		Start.m_ptr_last_app = newEnd;

		updata();
}

template<class T>
inline void multiListF<T>::show()
{
	int temp;
	Data<T> dat;
	BeginFile Fil;
	this->m_file->seekg(0, ios::beg);
	m_file->read((char*)&Fil, sizeof(Fil));

	cout << Fil.m_count_app << " ";
	cout << Fil.m_ptr_last_app << " ";
	cout << Fil.m_ptr_first_app << " ";
	cout << Fil.m_ptr_last_del_app << " ";
	cout << Fil.name_conf << " ";
	cout << endl;

	temp = 0;
	while (this->Start.m_count_app > temp)
	{
		this->m_file->read((char*)&dat, sizeof(dat));
		
		cout << dat.flag << " ";
		cout << dat.ptr << " ";
		cout << dat.data << " ";
		cout << dat.prtConf << " ";
		cout << endl;
		temp++;
	}
	cout << endl;
}

template<class T>
inline void multiListF<T>::link(T data1, T data2, int multi)
{
	Data<T> dat;
	int pos = find(data1);
	int posConf = find(data2);

	if (pos == -1 || posConf == -1)
	{
		throw new std::exception("Error access");
	}

	this->m_file->seekg(pos,ios::beg);

	m_file->read((char*)&dat, sizeof(dat));

	dat.prtConf = _link(dat.prtConf, posConf, multi);

	this->m_file->seekg(pos, ios::beg);

	m_file->write((char*)&dat, sizeof(dat));
}

template<class T>
inline void multiListF<T>::print(T data)
{
	Data<T> dat;
	DataConf datCon;
	int pos = find(data);
	int start;
	if (pos == -1 )
	{
		throw new std::exception("Error access");
	}

	this->m_file->seekg(pos, ios::beg);
	m_file->read((char*)&dat, sizeof(dat));

	cout << "\nProduct " << dat.data << "\nConfiguration : \n";

	start = dat.prtConf;

	while (start != 0)
	{
		this->m_file_config->seekg(start, ios::beg);
		this->m_file_config->read((char*)&datCon, sizeof(datCon));

		this->m_file->seekg(datCon.ptrProduct, ios::beg);
		m_file->read((char*)&dat, sizeof(dat));

		cout << "Product " << dat.data << " Multiplicity = " << datCon.multi << "\n";

		start = nextPtrConf(start);
	}

}

template<class T>
inline void multiListF<T>::showConf()
{
	int temp;
	int nowPos = m_file->tellg();
	DataConf dat;
	BeginConfig Fil;
	this->m_file_config->seekg(0, ios::beg);
	this->m_file_config->seekp(0, ios::beg);
	int nowPos1 = m_file_config->tellg();
	int nowPos2 = m_file_config->tellp();
	m_file_config->read((char*)&Fil, sizeof(Fil));

	cout << Fil.m_count_app << " ";
	cout << Fil.m_ptr_first_app << " ";
	cout << Fil.m_ptr_last_del_app << " ";
	cout << endl;

	temp = 0;
	while (Fil.m_count_app > temp)
	{
		this->m_file_config->read((char*)&dat, sizeof(dat));

		cout << dat.flag << " ";
		cout << dat.ptr << " ";
		cout << dat.ptrProduct << " ";
		cout << dat.multi << " ";
		cout << endl;
		temp++;
	}
	cout << endl;

	this->m_file_config->seekg(nowPos, ios::beg);
	this->m_file_config->seekp(nowPos, ios::beg);
}

template<class T>
inline multiListF<T>::~multiListF()
{
	m_file->close();
	delete m_file;
	m_file_config->close();
	delete m_file_config;
}

template<class T>
inline int multiListF<T>::find(T data)
{
	Data<T> dat;
	int ptrOld = 0;

	this->m_file->seekg(this->Start.m_ptr_first_app, ios::beg);

	while (true)
	{
		this->m_file->read((char*)&dat, sizeof(dat));
		
		if (dat.data == data)
		{
			if (ptrOld == 0)
			{
				ptrOld = this->Start.m_ptr_first_app;
			}

			return ptrOld;
		}

		if (dat.ptr == 0)
		{
			return -1;
		}
		ptrOld = dat.ptr;

		this->m_file->seekg(ptrOld, ios::beg);

	}
	
}

template<class T>
inline int multiListF<T>::_link(int start,int ptr, int multi)
{
	DataConf dat;

	 dat.ptrProduct = ptr;
	 dat.multi = multi;

	if (ConfStart.m_ptr_last_del_app == 0)
	{
		this->m_file_config->seekg(0, ios::end);

		ConfStart.m_count_app++;
	}
	else
	{
		this->m_file_config->seekg(ConfStart.m_ptr_last_del_app, ios::beg);

		ConfStart.m_ptr_last_del_app = this->nextPtrConf(ConfStart.m_ptr_last_del_app);
	}

	int newEnd = m_file_config->tellg();


	this->m_file_config->write((char*)&dat, sizeof(dat));

	if (start != 0)
	{
		int end = endList(start);
		this->m_file_config->seekg(end, ios::beg);
		this->m_file_config->read((char*)&dat, sizeof(dat));
		dat.ptr = newEnd;
		this->m_file_config->seekg(end, ios::beg);
		this->m_file_config->write((char*)&dat, sizeof(dat));
	}

	updataConf();

	if (start == 0)
	{
		return newEnd;
	}
	return start;
}

template<class T>
inline bool multiListF<T>::changeHead(int pos,int ptrDel)
{
	bool t = false;
	if (pos == this->Start.m_ptr_first_app)
	{
		this->Start.m_ptr_first_app = ptrDel;
		t = true;
	}

	if (pos == this->Start.m_ptr_last_app)
	{
		this->Start.m_ptr_last_app = ptrDel;
		t = true;
	}

	return t;
}

template<class T>
inline void multiListF<T>::updata()
{
	m_file->seekp(0, ios::beg);

	m_file->write((char*)&Start, sizeof(Start));

}

template<class T>
inline void multiListF<T>::updataConf()
{
	m_file_config->seekp(0, ios::beg);

	m_file_config->write((char*)&ConfStart, sizeof(ConfStart));
}

template<class T>
inline int multiListF<T>::nextPtr(int pos)
{
	Data<T> dat;
	int nowPos = m_file->tellg(), ptrNext;

	this->m_file->seekg(pos, ios::beg);
	this->m_file->read((char*)&dat, sizeof(dat));

	this->m_file->seekg(nowPos, ios::beg);

	return dat.ptr;
}

template<class T>
inline int multiListF<T>::nextPtrConf(int pos)
{
	DataConf  dat;
	int nowPos = m_file_config->tellg(), ptrNext;

	this->m_file_config->seekg(pos, ios::beg);
	this->m_file_config->read((char*)&dat, sizeof(dat));

	this->m_file_config->seekg(nowPos, ios::beg);

	return dat.ptr;
}

template<class T>
inline int multiListF<T>::backPtr(int pos,bool status)
{
	Data<T> dat;
	int start, nowPos = m_file->tellg(), ptrOld;

	if (pos == this->Start.m_ptr_first_app || pos == this->Start.m_ptr_last_del_app)
	{
		return -1;
	}

	if (status)
		start = this->Start.m_ptr_last_del_app;
	else
		start = this->Start.m_ptr_first_app;

	ptrOld = start;

	this->m_file->seekg(start, ios::beg);

	while (true)
	{
		this->m_file->read((char*)&dat, sizeof(dat));

		if (dat.ptr == pos)
		{
			this->m_file->seekg(nowPos, ios::beg);
			return ptrOld;
		}

		ptrOld = dat.ptr;

		this->m_file->seekg(ptrOld, ios::beg);
	}
}

template<class T>
inline void multiListF<T>::changeConfig(int pos1, int pos2)
{
	int temp;
	int start;
	DataConf dat;
	BeginConfig Fil;
	this->m_file_config->seekg(0, ios::beg);
	m_file_config->read((char*)&Fil, sizeof(Fil));

	start = Fil.m_ptr_first_app;

	temp = 0;

	while (Fil.m_count_app > temp)
	{
		m_file_config->seekg(start, ios::beg);
		
		this->m_file_config->read((char*)&dat, sizeof(dat));

		if (dat.ptrProduct == pos1)
		{
			dat.ptrProduct = pos2;
			m_file_config->seekg(start, ios::beg);
			this->m_file_config->write((char*)&dat, sizeof(dat));
		}
		else
		{
			if (dat.ptrProduct == pos2)
			{
				dat.ptrProduct = pos1;
				m_file_config->seekg(start, ios::beg);
				this->m_file_config->write((char*)&dat, sizeof(dat));
			}

		}

		start += sizeof(DataConf);
		temp++;
	}

}

template<class T>
inline void multiListF<T>::skipItem(int pos)
{
	Data<T> dat;
	int ptrDel = nextPtr(pos);
		
	int ptrNew = backPtr(pos);

	this->m_file->seekg(ptrNew, ios::beg);
	this->m_file->read((char*)&dat, sizeof(dat));
	dat.ptr = ptrDel;
	this->m_file->seekg(ptrNew, ios::beg);
	this->m_file->write((char*)&dat, sizeof(dat));

}

template<class T>
inline void multiListF<T>::deletion(int pos)
{
	Data<T> dat;

	this->m_file->seekg(pos, ios::beg);
	this->m_file->read((char*)&dat, sizeof(dat));
	dat.ptr = Start.m_ptr_last_del_app;
	dat.flag = true;
	this->m_file->seekg(pos, ios::beg);
	this->m_file->write((char*)&dat, sizeof(dat));

	Start.m_ptr_last_del_app = pos;
}

template<class T>
inline int multiListF<T>::countActiv()
{
	return this->Start.m_count_app - countDel();
}

template<class T>
inline int multiListF<T>::endList(int start)
{
	DataConf dat;
	int ptrEnd = start;
	while(true)
	{
		this->m_file_config->seekg(ptrEnd,ios::beg);
		this->m_file_config->read((char*)&dat, sizeof(dat));

		if (dat.ptr == 0)
		{
			break;
		}
		ptrEnd = dat.ptr;
	}

	return ptrEnd;
}

template<class T>
inline int multiListF<T>::firstOccurrence(int prt)
{
	int temp;
	DataConf dat;
	BeginConfig Fil;
	this->m_file_config->seekg(0, ios::beg);
	m_file_config->read((char*)&Fil, sizeof(Fil));

	temp = 0;

	while (Fil.m_count_app > temp)
	{
		int nowPos = m_file_config->tellg();

		this->m_file_config->read((char*)&dat, sizeof(dat));

		if (dat.ptrProduct == prt)
			return nowPos;

		temp++;
	}

	return 0;
}

template<class T>
inline int multiListF<T>::countDel()
{
	int count = 0;
	int firstDel = this->Start.m_ptr_last_del_app;

	while (firstDel != 0)
	{
		firstDel = nextPtr(firstDel);
		count++;
	}

	return count;
}

template<class T>
inline void multiListF<T>::sortDel()
{
	int sizeData = sizeof(Data<T>),
		countDel = this->countDel(),
		preOnDel = this->Start.m_ptr_last_del_app;

	int TotalSizeFile = this->m_start_list + sizeData * this->Start.m_count_app;

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
inline void multiListF<T>::sortActiv()
{
	int sizeData = sizeof(Data<T>),
		countApp = this->countActiv(),
		preOnApp = this->Start.m_ptr_first_app,
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
inline void multiListF<T>::swap(int pos1, int pos2)
{

	Data<T> dat1, dat2, dat;

	int backPos1, backPos2;
	int ptr1, ptr2;
	bool flag1, flag2;
	T Data1, Data2;

	this->m_file->seekg(pos1, ios::beg);

	this->m_file->read((char*)&dat1, sizeof(dat1));
	
	this->m_file->seekg(pos2, ios::beg);

	this->m_file->read((char*)&dat2, sizeof(dat2));
	

	backPos1 = backPtr(pos1, dat1.flag);
	backPos2 = backPtr(pos2, dat2.flag);

	if (backPos2 == pos1)
		backPos2 = pos2;

	if (backPos1 == pos2)
		backPos1 = pos1;

	this->m_file->seekg(pos2, ios::beg);

	this->m_file->write((char*)&dat1, sizeof(dat1));
	
	this->m_file->seekg(pos1, ios::beg);

	this->m_file->write((char*)&dat2, sizeof(dat2));
	

	if(backPos1 != -1)
	{ 
		this->m_file->seekg(backPos1, ios::beg);
		this->m_file->read((char*)&dat, sizeof(dat));
		dat.ptr = pos2;
		this->m_file->seekg(backPos1, ios::beg);
		this->m_file->write((char*)&dat, sizeof(dat));

	}
		
	if (backPos2 != -1)
	{
		this->m_file->seekg(backPos2, ios::beg);
		this->m_file->read((char*)&dat, sizeof(dat));
		dat.ptr = pos1;
		this->m_file->seekg(backPos2, ios::beg);
		this->m_file->write((char*)&dat, sizeof(dat));
	}
	
	changeConfig(pos1, pos2);

	checkTit(pos1,pos2);

	updata();

}

template<class T>
inline void multiListF<T>::_Load_data(Access status)
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
inline void multiListF<T>::_FILE_WC(Access status,const char* name_file)
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
inline void multiListF<T>::newFile()
{
	Start.m_ptr_first_app = m_start_list;

	strcpy(Start.name_conf, "config.ion");

	m_file->write((char*)&Start, sizeof(Start));

	std::ofstream oFile(Start.name_conf);
	oFile.close();

	this->m_file_config = new fstream(Start.name_conf, ios::binary | ios::in | ios::out);

	if (!m_file_config->is_open())
	{
		throw new std::exception("Error, file cann't be opened");
	}
	m_file_config->seekp(0, ios::beg);

	ConfStart.m_ptr_first_app = sizeof(ConfStart);

	m_file_config->write((char*)&ConfStart, sizeof(ConfStart));

}

template<class T>
inline void multiListF<T>::existingFile()
{
	m_file->read((char*)&Start, sizeof(Start));

	this->m_file_config = new fstream(Start.name_conf, ios::binary | ios::in | ios::out);

	if (!m_file->is_open())
	{
		throw new std::exception("Error, file cann't be opened");
	}

	m_file->seekp(0, ios::beg);

	m_file->read((char*)&ConfStart, sizeof(ConfStart));
}

template<class T>
inline void multiListF<T>::checkTit(int pos1, int pos2)
{
	if (pos1 == this->Start.m_ptr_first_app)
		this->Start.m_ptr_first_app = pos2;
	else if (pos2 == this->Start.m_ptr_first_app)
		this->Start.m_ptr_first_app = pos1;

	if (pos1 == this->Start.m_ptr_last_app)
		this->Start.m_ptr_last_app = pos2;
	else if (pos2 == this->Start.m_ptr_last_app)
		this->Start.m_ptr_last_app = pos1;

	if (pos1 == this->Start.m_ptr_last_del_app)
		this->Start.m_ptr_last_del_app = pos2;
	else if (pos2 == this->Start.m_ptr_last_del_app)
		this->Start.m_ptr_last_del_app = pos1;
}
