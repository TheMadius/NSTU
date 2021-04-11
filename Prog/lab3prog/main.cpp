#include "mainwindow.h"
#include "MyString.h"

#include <QApplication>

int main(int argc, char *argv[])
{
    MyString* str = new MyString[5];
    MyString rez;
    QApplication a(argc, argv);
    MainWindow w;
    w.setStr(str);
    w.show();
    delete [] str;
    return a.exec();
}
