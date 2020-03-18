#pragma once
#include <iostream>
#include <string.h>
#include "MyString.h"
using namespace std;

class product
{
public:
	product();
	product(product& other);
	product(const char * name,float price,int code );

	const char* getName();
	void setName(const char* name);
	float getPrice();
	float getDiscountPrice(float sale);
	void setPrice(float price);
	product& operator=(const product& other);
	friend ostream& operator<< (ostream& out , const product &prod);


	~product();

private:
	MyString name;
	float price;
	int code;
};
