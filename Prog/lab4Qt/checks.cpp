#include "checks.h"

void check::add(product pro)
{
    this->allPrice += pro.getPrice();
    this->productList.push_back(pro);
}

void check::delItem(int index)
{
    this->allPrice -= productList[index].getPrice();
    this->productList.pop_intex(index);
}

product check::operator[](int index)
{
    product newProd;
    newProd = this->productList[index];
    return newProd;
}

void check::clear()
{
    this->allPrice = 0;
	this->productList.clear();
}
int check::size()
{
    return this->productList.size();
}

check::~check()
{
	if (!(this->productList.empty()))
		this->productList.clear();
}
float check::getAllPrice()
{
     return this->allPrice;
 }


void check::creatFile(int numer,string start,string end)
{

    string name = "D://Check" + string(to_string(numer))+".txt";
    ofstream fout(name,ios::out);
    int size = this->productList.size();
    fout << start;

    for (int i = 0; i < size;i++)
     fout << i+1 <<')' << productList[i].getName() <<"\t\t" <<productList[i].getPrice() <<"\n";

    fout << end << this->allPrice;

    fout.close();
}















