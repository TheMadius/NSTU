#include "Check.h"
#include <ctime>

Check::Check()
{
    m_data = time(NULL);
    m_number = String();
    m_marketName = String();
    m_list = DList<Product>();
}

Check::Check(String marketName, String number)
{
    m_data = time(NULL);
    m_number = number;
    m_marketName = marketName;
    m_list = DList<Product>();
}

void Check::addProduct(const Product& product)
{
    m_list.addTail(product);
}

size_t Check::getSum()
{
    size_t sum = 0;
    size_t length = getAmountProducts();
    for (size_t i = 0; i < length; i++)
        sum += m_list[i].getCost();
    return sum;
}

String Check::getNumber()
{
    return m_number;
}

String Check::getMarketName()
{
    return m_marketName;
}

size_t Check::getAmountProducts()
{
    return m_list.amountElements();
}

Product Check::operator[](size_t index)
{
    return m_list[index];
}

void Check::deleteProduct(size_t productCode)
{
    size_t length = getAmountProducts();
    for (size_t i = 0; i < length; i++)
        if (m_list[i].getProductCode() == productCode)
        {
            m_list.del(i--);
            length--;
        }
}

void Check::setMarketName(String name)
{
    m_marketName = name;
}
void Check::setNumber(String number)
{
    m_number = number;
}
