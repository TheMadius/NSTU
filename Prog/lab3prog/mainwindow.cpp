#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <QMessageBox>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    ui->label2->setVisible(true);
    ui->label2->setText("Введите строку:");
    ui->label3->setVisible(false);
    ui->lineEdit->setVisible(true);
    ui->lineEdit_2->setVisible(false);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::setStr(MyString *str)
{
    this->str = new MyString[5];
    for (int i = 0; i < 5;i++)
    {
        this->str[i] =  str[i];
    }
}
void MainWindow::on_pushButton_clicked()
{
    int value = ui->comboBox_3->currentIndex();
    switch (value) {
    case 0:
    {
        QString st = ui->lineEdit->text();
        auto d = st.toStdString().c_str();

        this->str[ui->comboBox->currentIndex()].set(d);
        ui->textBrowser->setText("Переменная инициализирована");
        break;
    }
    case 1:
    {
        MyString newStr;
        int in1;
        QString st = ui->lineEdit->text();
        auto d = st.toStdString().c_str();

        in1 = ui->comboBox->currentIndex();
        newStr = (this->str[in1]).strdelstr(d);

        newStr.show(ui->textBrowser);
         break;
    }
    case 2:
    {
        MyString newStr;
        int in1;
        int start;
        QString st = ui->lineEdit->text();
        auto d = st.toStdString().c_str();
        st = ui->lineEdit_2->text();

        start = st.toInt();
        in1 = ui->comboBox->currentIndex();
        if (start > this->str[in1].Lenth()+1)
        {
             ui->textBrowser->setText("Значение начало выходит за пределы слова");
             break;
        }
        newStr = (this->str[in1]).insertstr(d,start);
        newStr.show(ui->textBrowser);
         break;
    }
    case 3:
    {
        MyString newStr;
        int in1;
        int size,end;
        QString st = ui->lineEdit->text();

        size = st.toInt();
        st = ui->lineEdit_2->text();

        end = st.toInt();
        in1 = ui->comboBox->currentIndex();
        if (size > this->str[in1].Lenth()-end+1)
        {
             ui->textBrowser->setText("Невозможно удалить заданное количество символов");
             break;
        }
        newStr = (this->str[in1]).delstr(size,end);
        newStr.show(ui->textBrowser);
         break;
    }
    case 4:
    {
        MyString newStr;
        int in1,in2;
        in1 = ui->comboBox->currentIndex();
        in2 = ui->comboBox_2->currentIndex();

        if ((this->str[in1].get() == nullptr) || this->str[in2].get() == nullptr )
                    {
                        ui->textBrowser->setText("Переменные должны быть инициализированы");
                        break;
                    }
         newStr = (this->str[in1]).strcat(this->str[in2]);
         newStr.show(ui->textBrowser);
         break;
    }
    case 5:
    {
        MyString newStr;
        int in1,in2;
        QString st = ui->lineEdit->text();
        auto d = st.toStdString().c_str();
        in1 = ui->comboBox->currentIndex();
        in2 = ui->comboBox_2->currentIndex();

        auto s = this->str[in1].get();
        auto s1 = this->str[in2].get();
        if ((this->str[in1].get() == nullptr) || this->str[in2].get() == nullptr )
                    {
                        ui->textBrowser->setText("Переменные должны быть инициализированы");
                        break;
                    }

        if((this->str[in1]).strcm(this->str[in2],d[0]))
             ui->textBrowser->setText("Выражение истинно");
        else
             ui->textBrowser->setText("Выражение ложно");
         break;
        }
    case 6:
    {
        int in1;
        in1 = ui->comboBox->currentIndex();
         (this->str[in1]).show(ui->textBrowser);
         break;
    }
    }

}

void MainWindow::on_comboBox_3_currentIndexChanged(int index)
{
    if (index > 3)
        ui->comboBox_2->setEnabled(true);
     else
        ui->comboBox_2->setEnabled(false);

    switch (index) {
    case 0:
        ui->label2->setVisible(true);
        ui->label2->setText("Введите строку:");
        ui->label3->setVisible(false);
        ui->lineEdit->setVisible(true);
        ui->lineEdit_2->setVisible(false);
        ui->lineEdit_2->setText("");
        ui->lineEdit->setText("");
        break;
    case 1:
        ui->label2->setVisible(true);
         ui->label2->setText("Введите подстроку:");
        ui->label3->setVisible(false);
        ui->lineEdit->setVisible(true);
        ui->lineEdit_2->setVisible(false);
        ui->lineEdit_2->setText("");
        ui->lineEdit->setText("");
         break;
    case 2:
        ui->label2->setVisible(true);
         ui->label3->setText("Начиная с:");
        ui->label3->setVisible(true);
        ui->label2->setText("Введите подстроку:");
        ui->lineEdit->setVisible(true);
        ui->lineEdit_2->setVisible(true);
        ui->lineEdit_2->setText("");
        ui->lineEdit->setText("");
         break;
    case 3:
        ui->label2->setVisible(true);
        ui->label2->setText("Длина:");
        ui->label3->setVisible(true);
        ui->lineEdit->setVisible(true);
        ui->lineEdit_2->setVisible(true);
        ui->lineEdit_2->setText("");
        ui->lineEdit->setText("");
         break;
    case 4:
        ui->label2->setVisible(false);
        ui->label3->setVisible(false);
        ui->lineEdit->setVisible(false);
        ui->lineEdit_2->setVisible(false);
        ui->lineEdit_2->setText("");
        ui->lineEdit->setText("");
         break;
    case 5:
        ui->label2->setVisible(true);
        ui->label2->setText("Операция сравнения");
        ui->label3->setVisible(false);
        ui->lineEdit->setVisible(true);
        ui->lineEdit_2->setVisible(false);
        ui->lineEdit_2->setText("");
        ui->lineEdit->setText("");
         break;
    case 6:
        ui->label2->setVisible(false);
        ui->label3->setVisible(false);
        ui->lineEdit->setVisible(false);
        ui->lineEdit_2->setVisible(false);
        ui->lineEdit_2->setText("");
        ui->lineEdit->setText("");
        ui->comboBox_2->setEnabled(false);
         break;
    }
}
