#ifndef ADDWINDOW_H
#define ADDWINDOW_H

#include <QDialog>
#include "product.h"

namespace Ui {
class addWindow;
}

class addWindow : public QDialog
{
    Q_OBJECT

public:
    explicit addWindow(QWidget *parent = nullptr);
    product& getProduct();
    bool checkProduct();
    ~addWindow();

private slots:
    void on_pushButton_clicked();

private:
    Ui::addWindow *ui;
    product *pord = nullptr;
    bool setProd = false;
};

#endif // ADDWINDOW_H
