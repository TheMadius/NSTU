#ifndef DELELEMENT_H
#define DELELEMENT_H

#include <QDialog>

namespace Ui {
class delElement;
}

class delElement : public QDialog
{
    Q_OBJECT

public:
    explicit delElement(QWidget *parent = nullptr);
    int getIndex();
    bool getSetIndex();
    ~delElement();

private slots:
    void on_pushButton_clicked();

private:
    Ui::delElement *ui;
    int Index;
    bool setIndex= false;
};

#endif // DELELEMENT_H
