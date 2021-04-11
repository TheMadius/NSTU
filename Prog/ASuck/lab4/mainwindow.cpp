#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <QMessageBox>

MainWindow::MainWindow(QWidget *parent) : QMainWindow(parent) , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    ui->lineEdit->setText("");
    ui->lineEdit_2->setText("");
    ui->lineEdit_3->setText("");
    ui->lineEdit->setVisible(true);
    ui->lineEdit_2->setVisible(true);
    ui->lineEdit_3->setVisible(false);

    ui->label_2->setVisible(false);
    ui->label_3->setText("Название магазина:");
    ui->label_3->setVisible(true);
    ui->label_4->setText("Номер чека:");
    ui->label_4->setVisible(true);
    ui->label_5->setText("");
    ui->label_5->setVisible(false);

    ui->spinBox->setVisible(false);

    this->setWindowTitle("Check");
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_pushButton_clicked()
{
    int value = ui->comboBox->currentIndex();
    try
    {
        switch (value)
        {
        case 0: //name, number
        {
            check.setMarketName(String(ui->lineEdit->text().toStdString().c_str()));
            check.setNumber(String(ui->lineEdit_2->text().toStdString().c_str()));
            ui->textBrowser->setText("Название магазина и номер чека установлены.");
            break;
        }
        case 1: //add
        {
            Product product(String(ui->lineEdit->text().toStdString().c_str()), ui->lineEdit_2->text().toInt(), ui->lineEdit_3->text().toInt());
            size_t number = ui->spinBox->value();
            for(size_t i = 0; i < number; i++)
                check.addProduct(product);
            ui->textBrowser->setText("Добавлен продукт.");
            break;
        }
        case 2: //delete
        {
            check.deleteProduct(ui->lineEdit_3->text().toInt());
            ui->textBrowser->setText("Удален продукт.");
            break;
        }
        case 3: //info
        {
            QString buffer;
            char* str;
            buffer.append("Название магазина:");
            str = check.getMarketName().getString();
            buffer.append(str);
            buffer.append("\n");

            buffer.append("Номер:");
            str = check.getNumber().getString();
            buffer.append(str);
            buffer.append("\n");

            buffer.append("Количество продуктов:");
            buffer.append(QString::number(check.getAmountProducts()));
            buffer.append("\n");

            buffer.append("Список продуктов:\n");
            size_t number =check.getAmountProducts();
            for(size_t i = 0; i < number; i++)
            {
                str = check[i].getName().getString();
                buffer.append(str);
                delete str;
                buffer.append(" ");
                buffer.append(QString::number(check[i].getCost()));
                buffer.append(" ");
                buffer.append(QString::number(check[i].getProductCode()));
                buffer.append("\n");
            }

            buffer.append("Сумма:");
            buffer.append(QString::number(check.getSum()));
            ui->textBrowser->setText(buffer);
            break;
        }
        }
    } catch (invalid_argument arg)
    {
        ui->textBrowser->setText(tr("Ошибка"));
    }
}

void MainWindow::on_comboBox_currentIndexChanged(int index)
{
    switch (index)
    {
    case 0:
    {
        ui->lineEdit->setText("");
        ui->lineEdit_2->setText("");
        ui->lineEdit_3->setText("");
        ui->lineEdit->setVisible(true);
        ui->lineEdit_2->setVisible(true);
        ui->lineEdit_3->setVisible(false);

        ui->label_2->setVisible(false);
        ui->label_3->setText("Название магазина:");
        ui->label_3->setVisible(true);
        ui->label_4->setText("Номер чека:");
        ui->label_4->setVisible(true);
        ui->label_5->setText("");
        ui->label_5->setVisible(false);

        ui->spinBox->setVisible(false);
        break;
    }
    case 1:
    {
        ui->lineEdit->setText("");
        ui->lineEdit_2->setText("");
        ui->lineEdit_3->setText("");
        ui->lineEdit->setVisible(true);
        ui->lineEdit_2->setVisible(true);
        ui->lineEdit_3->setVisible(true);

        ui->label_2->setVisible(true);
        ui->label_3->setText("Название продукта:");
        ui->label_4->setText("Цена:");
        ui->label_5->setText("Код товара:");
        ui->label_3->setVisible(true);
        ui->label_4->setVisible(true);
        ui->label_5->setVisible(true);

        ui->spinBox->setVisible(true);
        break;
    }
    case 2:
    {
        ui->lineEdit->setText("");
        ui->lineEdit_2->setText("");
        ui->lineEdit_3->setText("");
        ui->lineEdit->setVisible(true);
        ui->lineEdit_2->setVisible(true);
        ui->lineEdit_3->setVisible(false);

        ui->label_2->setVisible(false);
        ui->label_3->setText("Название магазина:");
        ui->label_4->setText("Номер чека:");
        ui->label_5->setText("");
        ui->label_3->setVisible(true);
        ui->label_4->setVisible(true);
        ui->label_5->setVisible(false);

        ui->spinBox->setVisible(false);
        break;
    }
    case 3:
    {
        ui->lineEdit->setText("");
        ui->lineEdit_2->setText("");
        ui->lineEdit_3->setText("");
        ui->lineEdit->setVisible(false);
        ui->lineEdit_2->setVisible(false);
        ui->lineEdit_3->setVisible(false);

        ui->label_2->setVisible(false);
        ui->label_3->setText("");
        ui->label_4->setText("");
        ui->label_5->setText("");
        ui->label_3->setVisible(false);
        ui->label_4->setVisible(false);
        ui->label_5->setVisible(false);

        ui->spinBox->setVisible(false);
        break;
    }
    }
}
