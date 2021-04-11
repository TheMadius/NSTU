#include <QObject>
#include <QGraphicsItem>
#include <QPainter>
#include <QGraphicsScene>
#include <QPixmap>
#include <QImage>
#include <QTimer>
#include "ContrlObject.h"


#ifndef CAMERA_H
#define CAMERA_H

#include <windows.h>

class Camera: public ContrlObject
{
public:
    explicit Camera(QObject *parent = nullptr);
    ~Camera();
    void setMovement(bool b_Movement);
    QRectF boundingRect() const;

    signals:

    public slots:
        void contrPlayer();

protected:
        void paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget);
private:
     qreal step;
     QTimer *gameTimer;

     QMetaObject::Connection myConnect;
};

#endif // CAMERA_H
