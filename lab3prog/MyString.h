#pragma once
#include <iostream>
#include <string.h>
#include <QMainWindow>
#include <QTextBrowser>
using namespace std;

class MyString
{
public:
	MyString();
	MyString(MyString& other);
	MyString(MyString&& other);
	MyString(const char * str);
    MyString& operator=(const MyString &other);
	MyString operator+(const MyString &other);
	bool operator<(const MyString &other);
	bool operator>(const MyString &other);
	bool operator==(const MyString &other);
	bool operator!=(const MyString &other);
	bool strcm(const MyString &other,char cmp);
	MyString strdelstr(const char* str);
	MyString insertstr(const char* str,int position);
	MyString delstr(int size,int position);
	MyString strcat(const MyString& other);
	char operator[](int index);
	~MyString();
    int Lenth();
    void show(QTextBrowser*& textBrowser);
	void set(const char* str);
    const char *get();
private:
	char* str;
	int len;
};
