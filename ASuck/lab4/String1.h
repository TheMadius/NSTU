#pragma once
#pragma warning(disable : 4996)

#include <iostream>
#include <locale>

using namespace std;

class String
{
private:
    size_t len;
	char* string;

public:
    enum compSign { LESS, BIGGER, LESS_OR_EQUAL, BIGGER_OR_EQUAL, EQUAL, NOT_EQUAL };

    String();
	String(const char* str);
    String(const String& str);
    String(String&& str);

    char* getString() const;
    size_t getLen() const;

    String delSubString(const String &str) const;
    String delSubStringLen(size_t pos, int length) const;

    String insSubString(const String &str, size_t pos) const;
    String concat(const String &str) const;
    bool compare(const String &str, compSign sign) const;

    String& operator= (const String& other);
    String& operator= (const char* other);

	~String();
};
