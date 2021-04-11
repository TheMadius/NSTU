#ifndef HUMAN_H
#define HUMAN_H

#define COUNT_IMG 7
#define COUNT_ANIMATION 4

#include <iostream>
#include <QObject>
#include <QGraphicsItem>
#include <QPainter>
#include <QGraphicsScene>
#include <QPixmap>
#include <QImage>
#include <QTimer>
#include <typeinfo>
#include "ContrlObject.h"
#include "disease.h"

#include <windows.h>

enum {Man = true,Female = false};

class Human: public ContrlObject
{
public:
    explicit Human(bool gender = Man,QObject *parent = nullptr);
    void setMovement(bool b_Movement);
    void setGender(bool gender);
    QRectF boundingRect() const;
    static QList<Human*> getListHuman();
    static void clearListHuman();
    void healHum(int eHeal);
    void damage(int uDamage);
    bool getStatus();
    QList<Disease> getlistDisease();
    void addDisease(Disease ill);
    void delDisease(Disease ill);
    ~Human();

signals:

public slots:
    void contrlNPS();
    void contrPlayer();
    void disbirth();
    void isHealthy();


protected:

    void paint(QPainter *painter, const QStyleOptionGraphicsItem *option, QWidget *widget);

    int hp;

    int m_countForSteps;
    int m_steps;

    QPixmap* m_animation[COUNT_ANIMATION][COUNT_IMG];

    qreal m_heightPNG;
    qreal m_widthPNG;

    bool m_gender;
    bool m_ability;
    bool m_alive;

    QTimer *m_gameTimer;
    QTimer *m_birthTimer;
    QTimer *m_InjectorTimer;

    QMetaObject::Connection m_myConnect;
    QMetaObject::Connection m_timerBorntConnect;
    QMetaObject::Connection m_ConnectHeal;

    QPainterPath shape() const;

    static QList<Human*> listNewPer;

private:
    QList<Disease> listDisease;
    void init();
    void initMan();
    void initFemale();
    void birth();
};

#endif // HUMAN_H
