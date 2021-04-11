#include <iostream>
#include "Btree.h"
using namespace std;

int main()
{
	setlocale(0, "ru");

	Btree<int>* fil = nullptr;

	const char* Menu = "\n1 -\tСоздать дерево \n2 -\tИспользовать существующий\n3 -\tДобавить элемент\n4 -\tУдалить элемент\n5 -\tСжать файл\n6 -\tВывести информацию\n7 -\tВыход\n\n";

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
			cout << "Введите имя файла : ";
			cin >> name_file;
			cout << "Введите поряток дерева : ";
			cin >> orter;

			fil = new Btree<int>(name_file.c_str(),New_Tree, orter);
			break;
		}
		case 2:
			if (fil != nullptr)
				delete fil;

			cout << "Введите имя файла : ";
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
			cout << "Введите элемент : ";
			cin >> num;
			fil->insert(num);
			break;
		}
		case 4:
		{
			int num;
			cout << "Введите элемент : ";
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