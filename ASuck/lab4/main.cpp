#include "mainwindow.h"
#include "String1.h"

#include <QApplication>

int main(int argc, char **argv)
{
    QApplication a(argc, argv);
    MainWindow w;
    w.show();
    return a.exec();
}
