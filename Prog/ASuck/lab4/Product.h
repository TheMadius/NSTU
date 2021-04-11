#pragma once
#include "String1.h"

class Product
{
private:
	String m_name;
	size_t m_cost;
	size_t m_productCode;

public:
	Product();
	Product(String name, size_t cost, size_t productCode);
	size_t getCost() const;
    size_t getProductCode() const;
    String getName() const;

	void setCost(size_t cost);
	void setProductCode(size_t code);
	void setName(String name);
};

