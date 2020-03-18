#include <iostream>
#include "dlist.h"
#include "product.h"

using namespace std;

int main()
{
	dlist<product> newList;
	product prod("Lor",50,345617);
	product prod1("Tor",70,345627);
	product prod2("tnt",90,345477);
	product prod3("Toy",20,345657);
	
	newList.push_front(prod);
	newList.push_back(prod1);
	newList.push_back(prod2);
	newList.push_back(prod3);

	newList.pop_intex(1);

	newList.show();

	 

}