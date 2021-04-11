#pragma once
#include <iostream>
#include <fstream>
#include <QMainWindow>
#include "product.h"
#include "dlist.h"
using namespace std;

class check
{
public:
	void add(product pro);
	void delItem(int index);
	void clear();
    float getAllPrice();
    int size();
    product operator[](int index);
    void creatFile(int numer,string start,string end);
	~check();

private:
	dlist<product> productList;
    float allPrice = 0;
};

