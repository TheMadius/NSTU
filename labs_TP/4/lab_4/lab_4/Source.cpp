#include <iostream>
#include "Btree.h"
using namespace std;

int main()
{
	setlocale(0, "ru");

	Btree<int>* fil = nullptr;

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
		{
			int orter;
			if (fil != nullptr)
				delete fil;
			cout << "������� ��� ����� : ";
			cin >> name_file;
			cout << "������� ������� ������ : ";
			cin >> orter;

			fil = new Btree<int>(name_file.c_str(),New_Tree, orter);
			break;
		}
		case 2:
			if (fil != nullptr)
				delete fil;

			cout << "������� ��� ����� : ";
			cin >> name_file;
			try
			{
				fil = new Btree<int>(name_file.c_str());
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