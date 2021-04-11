
#include <iostream>
#include <QMainWindow>
#include <QWidget>
#include <QGraphicsScene>
#include <QShortcut>
#include <QTimer>

#ifndef DISEASE_H
#define DISEASE_H


class Disease
{
public:
    Disease(QString name,qreal effectOnSpeed,int radiusOfAction,int damage,qreal probabilitySick,qreal probabilityCure);
    Disease();
    qreal getEffectOnSpeed(){return effectOnSpeed;}
    int getRadiusOfAction(){return radiusOfAction;}
    int getDamage(){return damage;}
    qreal getProbabilitySick(){return probabilitySick;}
    qreal getprobabilityCure(){return probabilityCure;}
    bool operator==(const Disease& other);

private:
    QString name;
    qreal effectOnSpeed;
    int radiusOfAction;
    int damage;
    qreal probabilitySick;
    qreal probabilityCure;

};

#endif // DISEASE_H
