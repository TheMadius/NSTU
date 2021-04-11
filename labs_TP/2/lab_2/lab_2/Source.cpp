#include <iostream>
#include <locale>

#include "Flist.h"

using namespace std;

int main()
{
	setlocale(0, "ru");

	Flist<int>* fil = nullptr;

	const char* Menu = "\n1 -\t������� ������ \n2 -\t������������ ������������\n3 -\t�������� �������\n4 -\t������� �������\n5 -\t����� ����\n6 -\t������� ����������\n7 -\t�����\n\n";

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

			fil = new Flist<int>(name_file.c_str());
			break;
		case 2:
			if (fil != nullptr)
				delete fil;
			cout << "������� ��� ����� : ";
			cin >> name_file;

			try
			{
				fil = new Flist<int>(name_file.c_str(), Access::Existing_List);
			}
			catch (std::exception* err)
			{
				cout << err->what() << endl;
			}
			break;
		case 3:
		{
			int num;
			cout << "������� ������� : ";
			cin >> num;
			fil->insert(num);

			break;
		}
		case 4:
		{
			int num;
			cout << "������� ������� : ";
			cin >> num;
			try
			{
				fil->remove(num);
			}
			catch (std::exception* err)
			{
				cout << err->what() << endl;
			}
			break;
		}
		case 5:
		{
			fil->pack();
			break;
		}
		case 6:
		{
			fil->show();
			break;
		}
		case 7:
			exit(1);
			break;
		default:
			break;
		}
	}
	return 0;
}