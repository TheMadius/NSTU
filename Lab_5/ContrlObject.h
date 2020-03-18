#include <QObject>
#include <QGraphicsItem>
#include <QPainter>
#include <QGraphicsScene>
#include <QPixmap>
#include <QImage>

#include <windows.h>

#ifndef controller_H
#define controller_H

class ContrlObject: public QObject, public QGraphicsItem
{
public:
     explicit ContrlObject(QObject *parent = nullptr);
     virtual void setMovement(bool b_Movement) = 0;

protected:
    bool move;
    qreal m_step;
    int m_direction;

    unsigned short m_directionOfTravel;

    qreal m_stepNPS;
    qreal m_stepBack;

signals:

    public slots:
        virtual void contrlPlayer();
        virtual void contrlNPS();

};

#endif // controller_H
