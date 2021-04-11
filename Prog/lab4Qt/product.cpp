#include "product.h"

product::product()
{
    this->code = 0;
    this->price = 0;
}

product::product(product& other)
{
    this->name = other.name;
    this->code = other.code;
    this->price = other.price;
}

product::product(const char* name, float price, int code)
{
    this->name.set(name);
    this->code = code;
    this->price = price;

}

const char* product::getName()
{
    return this->name.get();
}

void product::setName(const char* name)
{
    this->name.set(name);
}

float product::getPrice()
{
    return this->price;
}

float product::getDiscountPrice(float sale)
{
    return this->price*(1-sale);
}

void product::setPrice(float price)
{
    this->price = price;
}

void product::setCode(int code)
{
    this->code = code;
}

product& product::operator=(const product& other)
{
    this->name = other.name;
    this->code = other.code;
    this->price = other.price;

    return *this;
}

product::~product()
{
}

ostream& operator<<(ostream& out, const product& prod)
{
    MyString st;
    st = prod.name;

    out << st.get()<< "\t price: " << prod.price << "\t code: " << prod.code;

    return out;
}
