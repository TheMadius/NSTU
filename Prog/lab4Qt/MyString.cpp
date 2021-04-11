#include "MyString.h"

MyString::MyString()
{
	this->str = nullptr;
	this->len = 0;
}

MyString::MyString(MyString& other)
{
	this->len = other.len;
	this->str = new char[this->len + 1];

	for (int i = 0; i < this->len; i++)
	{
		this->str[i] = other.str[i];
	}

	this->str[this->len] = '\0';
}

MyString::MyString(MyString&& other)
{
	this->len = other.len;
	this->str = other.str;
	other.str = nullptr;
}

MyString::MyString(const char* str)
{
	this->len = strlen(str);
	this->str = new char[this->len + 1];

	for (int i = 0; i < this->len; i++)
	{
		this->str[i] = str[i];
	}

	this->str[this->len] = '\0';
		 
}

MyString& MyString::operator=(const MyString& other)
{
	if (this->str != nullptr)
		delete[] this->str;

	this->len = other.len;

	this->str = new char[this->len + 1];

	for (int i = 0; i < this->len; i++) 
	{
		this->str[i] = other.str[i];
	}

	this->str[this->len] = '\0';

	return *this;
}

MyString MyString::operator+(const MyString& other)
{
	MyString rezStr;

	rezStr.len = this->len + other.len;

	rezStr.str = new char[rezStr.len + 1];

	for (int i = 0; i < this->len; i++)
		rezStr.str[i] = this->str[i];


	for (int i = this->len; i < rezStr.len; i++)
		rezStr.str[i] = other.str[i - this->len];

	rezStr.str[rezStr.len] = '\0';

	return rezStr;
}

bool MyString::operator<(const MyString& other)
{
	return strcmp(this->str, other.str) < 0;
}

bool MyString::operator>(const MyString& other)
{
	return strcmp(this->str, other.str) > 0;
}

bool MyString::operator==(const MyString& other)
{
	return strcmp(this->str, other.str) == 0;
}

bool MyString::operator!=(const MyString& other)
{
	return !(this->str == other.str);
}

bool MyString::strcm(const MyString& other, char cmp)
{
	switch (cmp)
	{
    case '<':	return (*this < other);
    case '>':	return (*this > other);
    case '=':	return (*this == other);
    case '!':	return (*this != other);
		default: throw;
	}
}

MyString MyString::strdelstr(const char* str)
{
	MyString rezStr;
	char* newStr;
	int len = strlen(str);
    int position;
	newStr = strstr(this->str, str);

	if (newStr != nullptr)
	{
		rezStr.len = this->len - len;
		position = newStr-this->str;
		rezStr.str = new char[this->len - len + 1];

		for (int i = 0; i < position;i++)
			rezStr.str[i] = this->str[i];

		for (int i = position; i < this->len - len; i++)
			rezStr.str[i] = this->str[i+ len];

		rezStr.str[this->len - len] = '\0';
	}
    else return *this;

	return rezStr;
}

MyString MyString::insertstr(const char* str,int position)
{
	MyString rezStr;
	int len = strlen(str);

	position--;

	rezStr.len = this->len + len;

	rezStr.str = new char[rezStr.len + 1];
	
	for (int i = 0; i < position; i++)
		rezStr.str[i] = this->str[i];

	for (int i = position; i < position + len; i++)
		rezStr.str[i] = str[i - position];

	for (int i = position + len; i < rezStr.len; i++)
		rezStr.str[i] = this->str[i-len];

	rezStr.str[rezStr.len] = '\0';

	return rezStr;
}

MyString MyString::delstr(int size, int position)
{
	MyString rezStr;
	int len = strlen(str);

	position--;

	rezStr.len = this->len - size;

	rezStr.str = new char[rezStr.len + 1];

	for (int i = 0; i < position; i++)
		rezStr.str[i] = this->str[i];


	for (int i = position ; i < rezStr.len; i++)
		rezStr.str[i] = this->str[i + size];

	rezStr.str[rezStr.len] = '\0';

	return rezStr;
}

MyString MyString::strcat(const MyString& other)
{
	MyString rezStr;

	rezStr.len = this->len + other.len;

	rezStr.str = new char[rezStr.len + 1];

	for (int i = 0; i < this->len; i++)
		rezStr.str[i] = this->str[i];


	for (int i = this->len; i < rezStr.len; i++)
		rezStr.str[i] = other.str[i - this->len];

	rezStr.str[rezStr.len] = '\0';

	return rezStr;
}

char MyString::operator[](int index)
{
	return this->str[index];
}

MyString::~MyString()
{
    if (this->str != nullptr)
    {

    delete[] this->str;
    }
}

int MyString::lenth()
{
	return this->len;
}

void MyString::set(const char* str)
{
    if (this->str != nullptr)
        delete[] this->str;

	this->len = strlen(str);
	this->str = new char[this->len + 1];

	for (int i = 0; i < this->len; i++)
	{
		this->str[i] = str[i];
	}

	this->str[this->len] = '\0';
}

const char* MyString::get()
{
	return this->str;
}
