#include <iostream>

#include <stdio.h>
#include <string.h>

#include "multiListF.h"

using namespace std;


class Str
{
	private:
		char* string;
	public:
		Str()
		{
			string = new char[20];
		}
		Str(const char* str)
		{
			string = new char[20];
			strcpy(this->string, str);
		}

		Str& operator=(const Str& other)
		{
			strcpy(this->string, other.string);
			
			return *this;
		}

		friend ostream& operator<<(ostream& out, const Str& other)
		{
			out << other.string;
			return out;
		}

		bool operator==(const Str& other)
		{
			return (strcmp(this->string, other.string) == 0);
		}

};

int main()
{
	setlocale(0, "ru");

	multiListF<Str>* fil = nullptr;

	char temp[20];
	char temp2[20];

	const char* Menu = "\n1 -\t������� ������ \n2 -\t������������ ������������\n3 -\t�������� �������\n4 -\t�������� ������������\n5 -\t������� �������\n6 -\t����� ����\n7 -\t������� ����������\n8 -\t�����\n\n";

	while (true)
	{
		int choose;
		string name_file;
		cout << Menu;
		cin >> choose;
		switch (choose)
		{
		case 1:
			if (fil != nullptr)
				delete fil;

			cout << "������� ��� ����� : ";
			cin >> name_file;

			try
			{
				fil = new multiListF<Str>(name_file.c_str());
			}
			catch (std::exception* err)
			{
				cout << err->what() << endl;
			}
			break;
		case 2:
			if (fil != nullptr)
				delete fil;

			cout << "������� ��� ����� : ";
			cin >> name_file;

			try
			{
				fil = new multiListF<Str>(name_file.c_str(), Access::Existing_List);
			}
			catch (std::exception* err)
			{
				cout << err->what() << endl;
			}
			break;
		case 3:
		{
			int num;
			cout << "������� �������: ";
			cin >> temp;

			fil->insert(Str(temp));

			break;
		}
		case 4:
		{
			int num, conf,k;
			cout << "������� ������� : ";
			cin >> temp;
			cout << "������� ������������ : ";
			cin >> temp2;
			cout << "������� ��������� : ";
			cin >> k;
			try
			{
				fil->link(Str(temp), Str(temp2), k);
			}
			catch ( std::exception* err)
			{
				cout << err->what() << endl;
			}
			break;
		}
		case 5:
		{
			int num;
			cout << "������� ������� : ";
			cin >> temp;
			try
			{
				fil->remove(Str(temp));
			}
			catch (std::exception* err)
			{
				cout << err->what() << endl;
			}
			break;
		}
		case 6:
		{
			fil->pack();
			break;
		}
		case 7:
		{
			int num;
			cout << "������� ������� : ";
			cin >> temp;

			try
			{
				fil->print(Str(temp));
			}
			catch (std::exception* err)
			{
				cout << err->what() << endl;
			}

			break;
		}
		case 8:
			exit(1);
			break;
		default:
			fil->show();
			fil->showConf();
			break;
		}

	}
}