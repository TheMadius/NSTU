#include <iostream>
#include <fstream>
#include "virtual_memory.h"

using namespace std;

class createFile
{
	public:
		createFile(const char *name);
		void create_file_int();
		void create_file_char();
	private:
		const char* m_name;
};

createFile::createFile(const char* name)
{
	this->m_name = name;
}

void createFile::create_file_int()
{
	int r;

	srand(time(NULL));
	ofstream file (m_name, ios::binary | ios::out );

	for (size_t i = 0; i < 100; i++)
	{
		r = rand() % 64;

		file.write((char*)&r, sizeof(int));
	}
	file.close();
}

void createFile::create_file_char()
{
	char r = 'a';

	srand(time(NULL));
	ofstream file(m_name, ios::binary | ios::out);

	for (size_t i = 0; i < 100; i++)
	{
		r = r + rand() % 27;

		file.write((char*)& r, sizeof(char));

		r = 'a';
	}
	file.close();
}

int main()
{
	createFile test("D:\\temp.bin");

	test.create_file_int();

	virtual_memory<int> pages("D:\\temp.bin", 32, 4);

	pages.vput(100,1);
	pages.vput(99,2);

	for (int i = 1; i <= 100; i++)
	{
		cout <<i << ": " << pages[i] <<endl;
	}

	pages.clearBufer();
	   	   
    system("pause");
    return 0;
}


