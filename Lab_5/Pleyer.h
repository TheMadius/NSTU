#include <QObject>
#include <QGraphicsItem>
#include <QPainter>
#include <QGraphicsScene>
#include <QPixmap>
#include <QImage>


#ifndef PLEYER_H
#define PLEYER_H

class Pleyer:public QObject,public QGraphicsItem
{
    virtual void slotGameTimer() = 0;

};



#endif // PLEYER_H
