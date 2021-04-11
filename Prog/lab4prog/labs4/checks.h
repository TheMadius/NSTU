#pragma once
#include <iostream>
#include "product.h"
#include "dlist.h"
using namespace std;

class check
{
public:
	void add(product pro);
	void delItem(int index);
	void clear();
	~check();

private:
	dlist<product> productList;
};

