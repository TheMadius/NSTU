#include "addwindow.h"
#include "ui_addwindow.h"

addWindow::addWindow(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::addWindow)
{
    ui->setupUi(this);
    this->setWindowTitle("Enter new product");
}

addWindow::~addWindow()
{
    if(pord != nullptr)
     delete this->pord;
    delete ui;
}
product& addWindow::getProduct()
{
    return *pord;
}

bool addWindow::checkProduct()
{
    return this->setProd;
}

void addWindow::on_pushButton_clicked()
{
        const char* name = (ui->lineEdit->text()).toStdString().data();
        float price = (ui->lineEdit_2->text()).toFloat();
        int code = (ui->lineEdit_2->text()).toInt();

        this->pord = new product(name,price,code);

        setProd=true;

        this->close();
}

