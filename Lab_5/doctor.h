#ifndef DOCTOR_H
#define DOCTOR_H

#include <QObject>
#include <QGraphicsItem>
#include <QPainter>
#include <QGraphicsScene>
#include <QPixmap>
#include <QImage>
#include <QTimer>
#include <typeinfo>
#include <human.h>
#include "disease.h"

class Doctor:public Human
{
public:
    Doctor();
    ~Doctor();
private:
    void initDoc();
signals:
    public slots:
        void Healing();
private:
        int eHeal;
        QTimer *timerHeal;
        QMetaObject::Connection connectHeal;
};

#endif // DOCTOR_H
