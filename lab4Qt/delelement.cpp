#include "delelement.h"
#include "ui_delelement.h"

delElement::delElement(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::delElement)
{
    ui->setupUi(this);
    this->Index = 0;
}

 int delElement::getIndex()
 {
     return this->Index;
 }

 bool delElement::getSetIndex()
 {
        return this->setIndex;
 }

delElement::~delElement()
{
    delete ui;
}

void delElement::on_pushButton_clicked()
{
    this->Index = (ui->lineEdit->text()).toInt()-1;
    this->setIndex = true;
    this->close();
}
