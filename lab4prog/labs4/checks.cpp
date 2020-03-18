#include "checks.h"

void check::add(product pro)
{
	this->productList.push_front(pro);
}

void check::delItem(int index)
{
	this->productList.pop_intex(index);
}

void check::clear()
{
	this->productList.clear();
}

check::~check()
{
	if (!(this->productList.empty()))
		this->productList.clear();
}
