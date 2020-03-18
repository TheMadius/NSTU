#include "disease.h"

Disease::Disease(QString name,qreal effectOnSpeed,int radiusOfAction,int damage,qreal probabilitySick,qreal probabilityCure)
{
     this->name = name;
     this->effectOnSpeed = effectOnSpeed;
     this->radiusOfAction = radiusOfAction;
     this->damage = damage;
     this->probabilitySick = probabilitySick;
     this->probabilityCure = probabilityCure;
}

Disease::Disease()
{
    this->name = "";
    this->effectOnSpeed = 0;
    this->radiusOfAction = 0;
    this->damage = 0;
    this->probabilitySick = 0;
    this->probabilityCure = 0;
}

bool Disease::operator==(const Disease& other)
{
    return (other.name == this->name);
}
