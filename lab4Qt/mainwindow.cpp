#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "addwindow.h"
#include "delelement.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    this->titalCkeck = "\tNSTUStore\t\n                    Добро пожаловать!\n                         Кассовый чек\n";
    this->endCkeck =  "\nИтого:\t\t";
    this->setWindowTitle("Check");
    ui->textEdit->clear();
    ui->textEdit->append(titalCkeck);
    ui->textEdit->append(endCkeck + QString::number(this->newChek.getAllPrice()));
}

void MainWindow::showCheck()
{
    ui->textEdit->clear();
    ui->textEdit->append(titalCkeck);
    for(int i = 0; i < this->newChek.size();i++)
    {
       ui->textEdit->append(QString::number(i+1)+")"+QString(this->newChek[i].getName()) + "\t\t" + QString::number(this->newChek[i].getPrice()));
    }
    ui->textEdit->append(endCkeck + QString::number(this->newChek.getAllPrice()));
}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_pushButton_clicked()
{
    addWindow newWindow;
    newWindow.setModal(true);
    newWindow.exec();

    if(newWindow.checkProduct())
    {
        this->newChek.add(newWindow.getProduct());
    }

    this->showCheck();
    return ;
}

void MainWindow::on_pushButton_4_clicked()
{
    if(this->newChek.size() != 0)
        countCheck++;
    this->newChek.clear();

     this->showCheck();
}

void MainWindow::on_pushButton_3_clicked()
{
    if (this->newChek.size() == 0)
        return;
    delElement newWindow;
    newWindow.setModal(true);
    newWindow.exec();

    if(newWindow.getSetIndex())
    {

        this->newChek.delItem(newWindow.getIndex());
    }
    this->showCheck();
    return ;
}

void MainWindow::on_pushButton_2_clicked()
{
    this->newChek.creatFile(countCheck,this->titalCkeck.toStdString(),this->endCkeck.toStdString());
}
