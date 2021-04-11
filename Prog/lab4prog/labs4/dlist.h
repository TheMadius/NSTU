#pragma once
#include <iostream>
using namespace std;

template<typename T>
class dlist
{
public:
	dlist();
	~dlist();
	void push_front(T val);
	void pop_front();
	void push_back(T val);
	void pop_back();
	void pop_intex(int index);
	void insert(int index, T value);
	void erase(T it);
	int size();
	void clear();
	bool empty();
	void show();
	T operator[](int index);
private:
	template<typename TNode>
	class node
	{
	public:
		node(TNode val = TNode(), node* pNext = nullptr, node* pBack = nullptr)
		{
			this->data = val;
			this->pNext = pNext;
			this->pBack = pBack;

		};
		TNode data;
		node<TNode>* pNext;
		node<TNode>* pBack;

	};

	int lenth;

	node<T>* head;

	node<T>* tail;
};

template<typename T>
dlist<T>::dlist()
{
	this->head = nullptr;
	this->tail = nullptr;
	this->lenth = 0;
}
template<typename T>
dlist<T>::~dlist()
{
	this->clear();
}

template<typename T>
inline void dlist<T>::push_front(T val)
{
	if (this->head == nullptr)
	{
		head = new node<T>(val);
		tail = head;
	}
	else
	{

		node<T>* temp = new node<T>(val);

		this->head->pBack = temp;
		temp->pNext = this->head;
		this->head = temp;
	}

	this->lenth++;
}

template<typename T>
inline void dlist<T>::pop_front()
{
	if (head == nullptr)
	{
		tail = nullptr;
		return;
	}

	node<T>* temp = this->head;
	if (head->pBack != nullptr)
		head->pBack = nullptr;

	head = head->pNext;

	this->lenth--;

	delete temp;
}

template<typename T>
inline void dlist<T>::push_back(T val)
{
	if (this->head == nullptr)
	{
		head = new node<T>(val);
		tail = head;
	}
	else
	{
		node<T>* temp = new node<T>(val);

		tail->pNext = temp;
		tail->pNext->pBack = tail;
		tail = temp;
	}
	this->lenth++;

}

template<typename T>
inline void dlist<T>::pop_back()
{
	if (tail == nullptr)
	{
		head = nullptr;
		return;
	}
	node<T>* temp = this->tail;
	if (tail->pNext != nullptr)
		tail->pNext = nullptr;

	tail = tail->pBack;

	this->lenth--;

	delete temp;
}

template<typename T>
inline void dlist<T>::pop_intex(int index)
{
	if (index > this->lenth)
		return;

	node<T>* temp = this->head;
	for (int i = 0; i < index; i++)
		temp = temp->pNext;

	if (temp->pBack != nullptr)
		temp->pBack->pNext = temp->pNext;
	if (temp->pNext != nullptr)
		temp->pNext->pBack = temp->pBack;

	if (lenth == 1)
	{
		tail = nullptr;
		head = nullptr;
	}

	this->lenth--;
	delete temp;

}

template<typename T>
inline void dlist<T>::insert(int index, T value)
{
	node<T>* temp = this->head;
	for (int i = 0; i < index; i++)
		temp = temp->pNext;

	node<T>* newNode = new node<T>(value, temp->pNext, temp);

	temp->pNext->pBack = newNode;
	temp->pNext = newNode;
	this->lenth++;
}

template<typename T>
inline void dlist<T>::erase(T it)
{
	node<T>* temp = this->head;
	while (temp != nullptr)
	{
		if (temp->data == it)
			break;
		temp = temp->pNext;
	}

	if (temp == nullptr)
		return;

	if (temp->pBack != nullptr)
		temp->pBack->pNext = temp->pNext;
	if (temp->pNext != nullptr)
		temp->pNext->pBack = temp->pBack;

	if (lenth == 1)
	{
		tail = nullptr;
		head = nullptr;
	}

	this->lenth--;
	delete temp;
}

template<typename T>
inline int dlist<T>::size()
{
	return this->lenth;
}

template<typename T>
inline void dlist<T>::clear()
{
	while (head != nullptr)
		this->pop_back();
}

template<typename T>
inline bool dlist<T>::empty()
{
	return (head == nullptr);
}

template<typename T>
inline void dlist<T>::show()
{
	node<T>* temp = this->head;
	while (temp != nullptr)
	{
		cout << temp->data << endl;

		temp = temp->pNext;
	}
}

template<typename T>
inline T dlist<T>::operator[](int index)
{
	node<T>* temp = this->head;
	for (int i = 0; i < index; i++)
		temp = temp->pNext;
	return temp->data;
}



