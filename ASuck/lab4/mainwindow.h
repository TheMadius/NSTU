#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include "String1.h"
#include "Check.h"

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:
    void on_pushButton_clicked();

    void on_comboBox_currentIndexChanged(int index);

private:
    Ui::MainWindow *ui;
    Check check;
};
#endif // MAINWINDOW_H
