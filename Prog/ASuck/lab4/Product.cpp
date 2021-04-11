#include "Product.h"

Product::Product()
{
	m_name = "Name";
	m_cost = 0;
	m_productCode = 0;
}

Product::Product(String name, size_t cost, size_t productCode)
{
	m_name = name;
	m_cost = cost;
	m_productCode = productCode;
}

size_t Product::getCost() const
{
	return m_cost;
}

size_t Product::getProductCode() const
{
    return m_productCode;
}

String Product::getName() const
{
    return m_name;
}

void Product::setCost(size_t cost)
{
	m_cost = cost;
}

void Product::setProductCode(size_t code)
{
	m_productCode = code;
}

void Product::setName(String name)
{
	m_name = name;
}
