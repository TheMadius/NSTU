#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include "product.h"
#include "checks.h"

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    void showCheck();
    ~MainWindow();

private slots:
    void on_pushButton_clicked();

    void on_pushButton_4_clicked();

    void on_pushButton_3_clicked();

    void on_pushButton_2_clicked();

private:
    Ui::MainWindow *ui;
    check newChek;
    QString titalCkeck;
    QString endCkeck;
    int countCheck = 0;

};
#endif // MAINWINDOW_H
