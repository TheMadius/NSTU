#include "String1.h"
#include <string.h>

String::String()
{
    len = 0;
    string = new char[len + 1];
    string[0] = '\0';
}

String::String(const char* str)
{
    len = strlen(str);
    string = new char[len + 1];
    strcpy(string, str);
}

String::String(const String& str)
{
    len = str.len;
    string = new char[len + 1];
    strcpy(string, str.string);
}

String::String(String&& str)
{
    len = str.len;
    string = str.string;
    str.string = nullptr;
}

char* String::getString() const
{
    char* out = new char[len + 1];
    strcpy(out, string);
    return out;
}

size_t String::getLen() const
{
    return len;
}

String String::delSubString(const String &delStr) const
{
    char* p = strstr(string, delStr.string);

    String res;
    res.len = len - delStr.len;
    res.string = new char[res.len + 1];
    strcpy(res.string, "\0");
    strncat(res.string, string, p - string);
    strcat(res.string, p + delStr.len);
    return res;
}

String String::insSubString(const String &insStr, size_t pos) const
{
    if (pos > len)
        throw std::invalid_argument("Position bigger then string length");
    if (pos < 0)
        throw std::invalid_argument("Position less then 0");

    String res;
    res.len = len + insStr.len;
    res.string = new char[res.len + 1];
    strcpy(res.string, "\0");
    strncat(res.string, string, pos);
    strcat(res.string, insStr.string);
    strcat(res.string, string + pos);
    return res;
}

String String::delSubStringLen(size_t pos, int length) const
{
    if (length < 0)
        throw std::invalid_argument("Length less then 0");
    if (pos < 0)
        throw std::invalid_argument("Position less then 0");
    if (length + pos > len)
        throw std::invalid_argument("Out of the string");

    String res;
    res.len = len - length;
    res.string = new char[res.len + 1];
    strcpy(res.string, "\0");
    strncat(res.string, string, pos);
    strcat(res.string, string + pos + length);
    return res;
}

String String::concat(const String &str) const
{
    String res;
    res.len = len + str.len;
    res.string = new char[res.len + 1];
    strcpy(res.string, string);
    strcat(res.string, str.string);
    return res;
}

bool String::compare(const String &str, compSign sign) const
{
    switch(sign)
    {
    case BIGGER:            return strcmp(string, str.string) > 0;
    case LESS:              return strcmp(string, str.string) < 0;
    case BIGGER_OR_EQUAL:   return strcmp(string, str.string) >= 0;
    case LESS_OR_EQUAL:     return strcmp(string, str.string) <= 0;
    case EQUAL:             return strcmp(string, str.string) == 0;
    case NOT_EQUAL:         return strcmp(string, str.string) != 0;
	default: return false;
    }
}

String& String::operator=(const String& other)
{
    delete[] string;
    len = other.len;
    string = new char[len + 1];
    if (other.string != nullptr)
        strcpy(string, other.string);
    return *this;
}

String& String::operator=(const char* other)
{
    if (other == nullptr)
        throw std::invalid_argument("Null pointer");
    delete[] string;
    len = strlen(other);
    string = new char[len + 1];
    strcpy(string, other);
    return *this;
}


String::~String()
{
    delete[] string;
}
