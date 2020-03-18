#pragma once
#include "DualList.h"
#include "String1.h"
#include "Product.h"

class Check
{
private:
	String m_marketName;
    String m_number;
	time_t m_data;
	DList<Product> m_list;
public:
	Check();
    Check(String marketName, String number);
	void addProduct(const Product& product);
    void deleteProduct(size_t productCode);
    void setMarketName(String name);
    void setNumber(String number);

	size_t getSum();
    String getNumber();
	String getMarketName();
	size_t getAmountProducts();

    Product operator[](size_t index);
};

